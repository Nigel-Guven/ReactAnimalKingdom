using backend.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace backend.Services.Animals
{
    public class AnimalService : IAnimalService
    {
        private readonly string _filePath = Path.Combine(AppContext.BaseDirectory, "Data", "animals.json");

        private List<Animal> ReadAnimals()
        {
            if (!File.Exists(_filePath))
                return new List<Animal>();

            var json = File.ReadAllText(_filePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() }
            };
            return JsonSerializer.Deserialize<List<Animal>>(json, options) ?? new List<Animal>();
        }

        private void WriteAnimals(List<Animal> animals)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Converters = { new JsonStringEnumConverter() }
            };
            var json = JsonSerializer.Serialize(animals, options);
            File.WriteAllText(_filePath, json);
        }

        public List<Animal> GetAll()
        {
            return ReadAnimals();
        }

        public Animal? GetById(int id)
        {
            return ReadAnimals().FirstOrDefault(a => a.Id == id);
        }

        public Animal Add(Animal newAnimal)
        {
            var animals = ReadAnimals();
            newAnimal.Id = animals.Any() ? animals.Max(a => a.Id) + 1 : 1;
            animals.Add(newAnimal);
            WriteAnimals(animals);
            return newAnimal;
        }

        public bool Update(int id, Animal updatedAnimal)
        {
            var animals = ReadAnimals();
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return false;

            animal.Name = updatedAnimal.Name;
            animal.LatinName = updatedAnimal.LatinName;
            animal.Type = updatedAnimal.Type;
            animal.Location = updatedAnimal.Location;
            animal.Status = updatedAnimal.Status;

            WriteAnimals(animals);
            return true;
        }

        public bool Delete(int id)
        {
            var animals = ReadAnimals();
            var animal = animals.FirstOrDefault(a => a.Id == id);
            if (animal == null) return false;

            animals.Remove(animal);
            WriteAnimals(animals);
            return true;
        }
    }
}