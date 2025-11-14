using Microsoft.AspNetCore.Mvc;
using backend.Services.Animals;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("animal")]
    public class AnimalController : ControllerBase
    {
        private readonly IAnimalService _service;

        public AnimalController(IAnimalService service)
        {
            _service = service;
        }

        // GET /animal
        [HttpGet]
        public IActionResult GetAnimals()
        {
            var animals = _service.GetAll();
            return Ok(animals);
        }

        // GET /animal/{id}
        [HttpGet("{id}")]
        public IActionResult GetAnimal(int id)
        {
            var animal = _service.GetById(id);
            return animal == null ? NotFound() : Ok(animal);
        }

        // POST /animal
        [HttpPost]
        public IActionResult AddAnimal([FromBody] Animal newAnimal)
        {
            var animal = _service.Add(newAnimal);
            return CreatedAtAction(nameof(GetAnimal), new { id = animal.Id }, animal);
        }

        // PUT /animal/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateAnimal(int id, [FromBody] Animal updatedAnimal)
        {
            return _service.Update(id, updatedAnimal) ? NoContent() : NotFound();
        }

        // DELETE /animal/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteAnimal(int id)
        {
            return _service.Delete(id) ? NoContent() : NotFound();
        }
    }
}