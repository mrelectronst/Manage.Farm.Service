using Manage.Farm.Service.API.Application;
using Manage.Farm.Service.API.Facade;
using Manage.Farm.Service.API.Facade.Interface;
using Manage.Farm.Service.Domain.Animal;
using Manage.Farm.Service.Infrastructure;
using Manage.Farm.Service.Infrastructure.Dapper;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace Manage.Farm.Service.API;

public class Startup
{
    public Startup(IConfiguration configuration) => Configuration = configuration;
    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure<InfrastructureConfig>(Configuration);
        services.AddControllers(options =>
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true);
        services.AddHealthChecks();
        AddApiVersioning(services);
        AddSwagger(services);
        AddServices(services);
    }

    private static void AddApiVersioning(IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
        });
        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });
    }

    private static void AddSwagger(IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "1.0",
                Title = "Logistic Order Service V1",
            });
            swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description =
                    "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
            });
            swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }

    private static void AddServices(IServiceCollection services)
    {
        services.AddTransient<IAnimalFacade, AnimalFacade>();

        services.AddScoped<IAnimalService, AnimalService>();

        services.AddAutoMapper(typeof(Startup));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger(c => { c.RouteTemplate = "/farm/swagger/{documentname}/swagger.json"; });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/farm/swagger/v1/swagger.json", "Manage.Farm.Service.API v1");
                c.RoutePrefix = "farm/swagger";
            });
        }

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin();
            builder.AllowAnyHeader();
            builder.AllowAnyMethod();
        });
        app.UseAuthorization();
        app.UseAuthentication();
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/healthcheck", new HealthCheckOptions
            {
                AllowCachingResponses = false
            });
        });
    }
}

