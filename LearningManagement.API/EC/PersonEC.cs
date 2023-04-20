using System;
using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class PersonEC
    {
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

        public Person AddorUpdateAdmin(Person p)
        {
            bool isNew = false;
            if(FakeDataBase.People.Count == 0)        
            {
                p.Id = "0";
                isNew = true;
            }
            else if (int.Parse(p.Id) <= 0)
            {
                var lastId = FakeDataBase.People.Select(p => int.Parse(p.Id)).Max();
                lastId++;
                p.Id = lastId.ToString();
                isNew = true;
            }

            if (isNew)
            {
                if (p is Instructor instructor)
                {
                    FakeDataBase.People.Add(instructor);
                }
                else if (p is TeachingAssistant assistant)
                {
                    FakeDataBase.People.Add(assistant);
                }
            }
            else
            {
                var editedInstructor = FakeDataBase.People.FirstOrDefault(p => p.Id == p.Id);
                if(editedInstructor != null)
                {
                    editedInstructor.FirstName = p.FirstName;
                    editedInstructor.LastName = p.LastName;
                }
            }
            return p;
        }

        public Student AddorUpdateStudent(Student s)
        {
            bool isNew = false;
            if (FakeDataBase.People.Count == 0)
            {
                s.Id = "0";
                isNew = true;
            }
            else if (int.Parse(s.Id) <= 0)
            {
                var lastId = FakeDataBase.People.Select(p => int.Parse(p.Id)).Max();
                lastId++;
                s.Id = lastId.ToString();
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.People.Add(s);
            }
            else
            {
                var editedStudent = FakeDataBase.Students.FirstOrDefault(p => p.Id == s.Id);
                if (editedStudent != null)
                {
                    editedStudent.FirstName = s.FirstName;
                    editedStudent.LastName = s.LastName;
                    editedStudent.Classification = s.Classification;
                }
            }
            return s;
        }

        public void Delete(Person person)
        {
            var deletedPerson = FakeDataBase.People.FirstOrDefault(p => p.Id == person.Id);
            if (deletedPerson != null)
            {
                FakeDataBase.People.Remove(deletedPerson);
            }
        }
    }
}
