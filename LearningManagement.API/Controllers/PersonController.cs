<<<<<<< HEAD
﻿using UWP.Library.LearningManagement.Models;
=======
﻿using Library.LearningManagement.Models;
>>>>>>> 6f2539601f20f679f9783d6d981f49a0344142e6
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
