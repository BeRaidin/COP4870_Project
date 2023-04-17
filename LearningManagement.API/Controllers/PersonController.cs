using Library.LearningManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController
    {
        private readonly ILogger<PersonController> _logger;
        private List<Person> _people;

        public PersonController(ILogger<PersonController> logger)
        {
            _logger = logger;
            _people = new List<Person> { new Student {Id = "0", FirstName="Brayden", LastName="Lewis", Classification=Student.Classes.Sophmore} };
        }

        [HttpGet]
        public List<Person> Get()
        {
            return _people;
        }
    }
}
