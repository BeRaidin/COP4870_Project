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

        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        private Person _person;
        public Person Person
        {
            get { return _person; }
            set { _person = value; }
        }
        public ObservableCollection<string> PersonTypes { get; set; }
        public ObservableCollection<string> StudentLevels { get; set; }
        public string SelectedType { get; set; }
        public string SelectedClass { get; set; }
        public string Name {
            get { return SelectedPerson.Name; }
            set { SelectedPerson.Name = value; } 
        }

        public PersonViewModel()
        {
            personService = PersonService.Current;
            PersonTypes = new ObservableCollection<string>
            { "Student", "Instructor", "Teaching Assistant" };
            StudentLevels = new ObservableCollection<string>
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
            else Person = new Student();
            Person.Name = Name;

            var student = Person as Student;
            if (student != null)
            {
                if (SelectedClass == "Freshman")
                {
                    student.Classification = Student.Classes.Freshman;
                }
                else if (SelectedClass == "Sophmore")
                {
                    student.Classification = Student.Classes.Sophmore;
                }
                else if (SelectedClass == "Junior")
                {
                    student.Classification = Student.Classes.Junior;
                }
                else if (SelectedClass == "Senior")
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
            SelectedPerson.Name = Name;
        }
    }
}
