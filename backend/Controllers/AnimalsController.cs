
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("animal")]
    public class AnimalController : ControllerBase
    {
        private static readonly List<string> Animals = new()
        {
            "Lion",
            "Bear",
            "Eagle",
            "Turtle",
            "Deer",
            "Seal",
            "Penguin"
        };

        [HttpGet]
        public IEnumerable<string> GetAnimals()
        {
            return Animals;
        }
    }
}