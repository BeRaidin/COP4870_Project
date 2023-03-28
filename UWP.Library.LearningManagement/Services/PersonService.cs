using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class PersonService
    {
        private List<Person> personList;
        public List<Person> PersonList
        {
            get
            {
                return personList;
            }
            set
            {
                personList = value;
            }
        }
        private Person _currentPerson;
        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set { _currentPerson = value; }
        }
        public static PersonService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new PersonService();
                }

                return instance;
            }
        }
        private static PersonService instance;

        public PersonService()
        {
            PersonList = new List<Person>();
        }
           
        public void Add(Person person)
        {
            PersonList.Add(person);
        }

        public void Remove()
        {
            foreach (var course in CurrentPerson.Courses)
            {
                course.Roster.Remove(CurrentPerson);
            }
            PersonList.Remove(CurrentPerson);
        }

        public int Size()
        {
            return PersonList.Count;
        }

    }
}
