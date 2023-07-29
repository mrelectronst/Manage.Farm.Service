using Manage.Farm.Service.Domain.Animal;

namespace Manage.Farm.Service.API.Facade.Interface
{
    public interface IAnimalFacade
    {
        Task<IEnumerable<Animal>> List();
        Task<int> Add(Animal animal);
        Task<int> Delete(Guid id);
    }
}
