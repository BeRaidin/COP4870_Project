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
        private readonly PersonService personService;
        private Person _person;
        public Person Person
        {
            get { return _person; }
            set { _person = value; }
        }
        public List<string> PersonTypes { get; set; }
        public List<string> StudentLevels { get; set; }
        public string SelectedType { get; set; }
        public string SelectedLevel { get; set; }
        public string Name {
            get { return personService.CurrentPerson.Name; }
            set { personService.CurrentPerson.Name = value; } 
        }

        public PersonViewModel()
        {
            personService = PersonService.Current;
            PersonTypes = new List<string>
            { "Student", "Instructor", "Teaching Assistant" };
            StudentLevels = new List<string>
            { "Freshman", "Sophmore", "Junior", "Senior" };
        }

        public void Add()
            {
            if (SelectedType == "Student")
            {
                Person = new Student();
            }
            else if (SelectedType == "Instructor")
            {
                Person = new Instructor();
            }
            else if (SelectedType == "Teaching Assistant")
            {
                Person = new TeachingAssistant();
            }
            else Person = new Person();
            Person.Name = Name;
            Person.Id = personService.Size();

            var student = Person as Student;
            if (student != null)
            {
                if (SelectedLevel == "Freshman")
                {
                    student.Classification = Student.Classes.Freshman;
                }
                else if (SelectedLevel == "Sophmore")
                {
                    student.Classification = Student.Classes.Sophmore;
                }
                else if (SelectedLevel == "Junior")
                {
                    student.Classification = Student.Classes.Junior;
                }
                else if (SelectedLevel == "Senior")
                {
                    student.Classification = Student.Classes.Senior;
                }
                else student.Classification = Student.Classes.Freshman;
                personService.Add(student);
            }
            else
            {
                personService.Add(Person);
            }
        }

        public void Edit()
        {
            personService.CurrentPerson.Name = Name;
        }
    }
}
