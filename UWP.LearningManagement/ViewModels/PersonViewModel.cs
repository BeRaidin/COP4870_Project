using Library.LearningManagement.Models;
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
        private Person person { get; set; }
        private List<Person> people;
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

        public PersonViewModel(List<Person> people) 
        { 
            person = new Person();
            person.Id = people.Count();
            this.people = people;
        }

        public void Add()
        {
            people.Add(person);
        }

        public void RemovePerson()
        {
            people.Remove(person);
        }
    }
}
