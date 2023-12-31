﻿namespace Manage.Farm.Service.Domain.Animal;

public interface IAnimalService
{
    Task<List<Animal>> GetAll();
    Task<Animal> GetByName(string name);
    Task<int> Add(Animal obj);
    Task<int> Delete(Guid id);
    Task<Animal?> Get(Guid id);
}

public class Animal
{
    public Animal()
    {
    }

    public Animal(Guid id,
        string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
}