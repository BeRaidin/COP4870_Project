using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class PersonViewModel
    {
        private CourseService courseService;
        private PersonService personService;
        private Person person { get; set; }
        public string Name
        {
            get
            {
                return person.Name;
            }
            set
            {
                person.Name = value;
            }
        }
        public Person Person
        {
            get { return person; }
            set { person = value; }
        }
        public List<string> PersonTypes { get; set; }
        public string SelectedType { get; set; }

        public PersonViewModel()
        {
            courseService = CourseService.Current;
            personService = PersonService.Current;
            person = new Person();
            person.Id = personService.Size();
            PersonTypes = new List<string>
            { "Student", "Instructor", "Teaching Assistant" };
        }

        public void Add()
        {
            personService.Add(person);
        }


        public void Edit()
        {
            personService.CurrentPerson.Name = person.Name;
        }
    }
}
