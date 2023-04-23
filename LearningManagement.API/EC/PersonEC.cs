﻿using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class PersonEC
    {
        public List<Person> GetPeople()
        {
            return FakeDataBase.People;
        }
        public List<Instructor> GetInstructors()
        {
            return FakeDataBase.Instructors;
        }

        public List<TeachingAssistant> GetAssistants()
        {
            return FakeDataBase.Assistants;
        }

        public List<Student> GetStudents()
        {
            return FakeDataBase.Students;
        }

        public Student AddorUpdateStudent(Student s)
        {
            bool isNew = false;
            if (FakeDataBase.People.Count == 0)
            {
                s.Id = 0;
                isNew = true;
            }
            else if (s.Id < 0)
            {
                var lastId = FakeDataBase.People.Select(p => p.Id).Max();
                lastId++;
                s.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.People.Add(s);
            }
            else
            {
                var editedStudent = FakeDataBase.People.FirstOrDefault(p => p.Id == s.Id) as Student;
                if (editedStudent != null)
                {
                    editedStudent.FirstName = s.FirstName;
                    editedStudent.LastName = s.LastName;
                    editedStudent.Classification = s.Classification;
                }
            }
            return s;
        }

        public Person AddorUpdateAdmin(Person i)
        {
            bool isNew = false;
            if(FakeDataBase.People.Count == 0)        
            {
                i.Id = 0;
                isNew = true;
            }
            else if (i.Id < 0)
            {
                var lastId = FakeDataBase.People.Select(p =>p.Id).Max();
                lastId++;
                i.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                if (i is Instructor instructor)
                {
                    FakeDataBase.People.Add(instructor);
                }
                else if (i is TeachingAssistant assistant)
                {
                    FakeDataBase.People.Add(assistant);
                }
            }
            else
            {
                var editedInstructor = FakeDataBase.People.FirstOrDefault(p => p.Id == i.Id);
                if(editedInstructor != null)
                {
                    editedInstructor.FirstName = i.FirstName;
                    editedInstructor.LastName = i.LastName;
                }
            }
            return i;
        }

        public Person UpdateCourses(Person p)
        {
            var editedPerson = FakeDataBase.People.FirstOrDefault(i => i.Id == p.Id);
            if (editedPerson != null) 
            { 
                editedPerson.Courses = p.Courses;
            }
            return p;
        }

        public void Delete(Person p)
        {

            var deletedPerson = FakeDataBase.People.FirstOrDefault(d => d.Id == p.Id);
            if(deletedPerson != null)
            {
                FakeDataBase.People.Remove(deletedPerson);
            }
        }
    }
}
