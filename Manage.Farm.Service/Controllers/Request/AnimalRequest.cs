using Manage.Farm.Service.Domain.Animal;

namespace Manage.Farm.Service.API.Controllers.Request;

public class AnimalRequest
{
    public AnimalRequest()
    {
    }

    public string Name { get; set; }

    public Animal ToEntity() => new(Guid.NewGuid(), Name);
}