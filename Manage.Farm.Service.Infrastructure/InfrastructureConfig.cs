namespace Manage.Farm.Service.Infrastructure;

public class InfrastructureConfig
{
    public ConnectionStrings ConnectionStrings { get; set; }
}

public class ConnectionStrings
{
    public string FarmDb { get; set; }
}