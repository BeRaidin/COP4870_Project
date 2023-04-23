using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using System;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController
    {
        [HttpGet]
        public List<Student> Get()
        {
            return new PersonEC().GetStudents();
        }

        [HttpPost("Delete")]
        public void DeleteInstructor([FromBody] Person person)
        {
            new PersonEC().Delete(person);
        }
    }
}
