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
        private static PersonService instance;
        private Person currentPerson;
        public Person CurrentPerson
        {
            get { return currentPerson; }
            set { currentPerson = value; }
        }


        public PersonService()
        {
            personList = new List<Person>();
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
           
        public void Add(Person person)
        {
            personList.Add(person);
        }

        public List<Person> People
        {
            get
            {
                return personList;
            }
        }

        public int Size()
        {
            return personList.Count;
        }

        public void Remove()
        {
            foreach (var course in CurrentPerson.Courses)
            {
                course.Roster.Remove(CurrentPerson);
            }
            personList.Remove(CurrentPerson);
        }
    }
}
