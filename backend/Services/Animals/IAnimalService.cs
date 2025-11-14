using backend.Models;

namespace backend.Services.Animals
{
    public interface IAnimalService
    {
        List<Animal> GetAll();
        Animal? GetById(int id);
        Animal Add(Animal newAnimal);
        bool Update(int id, Animal updatedAnimal);
        bool Delete(int id);
    }
}