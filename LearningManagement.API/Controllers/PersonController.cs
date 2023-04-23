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
        [HttpGet("GetPeople")]
        public List<Person> GetPeople()
        {
            return new PersonEC().GetPeople();
        }

        [HttpGet("GetStudents")]
        public List<Student> GetStudents()
        {
            return new PersonEC().GetStudents();
        }

        [HttpGet("GetInstructors")]
        public List<Instructor> GetInstructors()
        {
            return new PersonEC().GetInstructors();
        }

        [HttpGet("GetAssistants")]
        public List<TeachingAssistant> Get()
        {
            return new PersonEC().GetAssistants();
        }

        [HttpPost("AddOrUpdateStudent")]
        public Student AddorUpdateStudent([FromBody] Student student)
        {
            return new PersonEC().AddorUpdateStudent(student);
        }

        [HttpPost("AddOrUpdateInstructor")]
        public Person AddorUpdate([FromBody] Instructor instructor)
        {
            return new PersonEC().AddorUpdateAdmin(instructor);
        }

        [HttpPost("AddOrUpdateAssistant")]
        public Person AddorUpdate([FromBody] TeachingAssistant assistant)
        {
            return new PersonEC().AddorUpdateAdmin(assistant);
        }

        [HttpPost("Delete")]
        public void DeleteInstructor([FromBody] Person person)
        {
            new PersonEC().Delete(person);
        }
    }
}
