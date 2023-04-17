using UWP.Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.Library.LearningManagement.Database;

namespace Library.LearningManagement.Services
{
    public class PersonService
    {
        public List<Person> People
        {
            get { return FakeDataBase.People; }
        }

        public List<Student> Students
        { 
            get { return FakeDataBase.Students; } 
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
            CurrentPerson = new Person();
            totalCount = 0;
        }

        public void Add(Person person)
        {
            person.Id = totalCount.ToString();
            totalCount++;
            FakeDataBase.People.Add(person);
        }

        public void Remove()
        {
            foreach(Student student in Students)
            {
                if(CurrentPerson.Id == student.Id)
                {
                    FakeDataBase.Students.Remove(student);
                    break;
                }
            }
        }
    }
}
