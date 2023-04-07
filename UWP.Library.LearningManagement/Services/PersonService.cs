using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
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
        private Assignment _currentAssigment;
        public Assignment CurrentAssignment
        {
            get { return _currentAssigment; }
            set { _currentAssigment = value; }
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

        private int totalCount;

        public PersonService()
        {
            PersonList = new List<Person>();
            CurrentPerson = new Person();
            totalCount = 0;
        }
           
        public void Add(Person person)
        {
            person.Id = totalCount.ToString();
            totalCount++;
            PersonList.Add(person);
        }

        public void Remove()
        {
            foreach(Person person in PersonList)
            {
                if(CurrentPerson.Id == person.Id)
                {
                    PersonList.Remove(person);
                    break;
                }
            }
        }
    }
}
