using Manage.Farm.Service.API.Application;
using Manage.Farm.Service.API.Facade.Interface;
using Manage.Farm.Service.Domain.Animal;

namespace Manage.Farm.Service.API.Facade;

public class AnimalFacade : IAnimalFacade
{
    private readonly IAnimalService _animalService;

    public AnimalFacade(IAnimalService animalService)
    {
        _animalService = animalService;
    }

    public async Task<IEnumerable<Animal>> List() => await _animalService.GetAll();

    public async Task<int> Add(Animal animal)
    {
        var foundedAnimal = await _animalService.Get(animal.Name).ConfigureAwait(false);
        if (foundedAnimal != null)
            throw new SameAnimalNameFoundException();
        return await _animalService.Add(animal);
    }

    public async Task<int> Delete(Guid id) => await _animalService.Delete(id);
}