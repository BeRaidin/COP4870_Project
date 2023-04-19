using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class StudentEC
    {
        public List<Student> GetStudents()
        {
            return FakeDataBase.Students;
        }

        public Student AddorUpdateStudent(Student s)
        {
            bool isNew = false;
            if (FakeDataBase.People.Count == 0)
            {
                s.Id = "0";
                isNew = true;
            }
            else if (int.Parse(s.Id) < 0)
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
                var editedStudent = FakeDataBase.People.FirstOrDefault(p => p.Id == s.Id) as Student;
                if(editedStudent != null)
                {
                    editedStudent.FirstName = s.FirstName;
                    editedStudent.LastName = s.LastName;
                    editedStudent.Classification = s.Classification;
                }
            }
            return s;
        }

        public void Delete(Person p)
        {

            var deletedPerson = FakeDataBase.People.FirstOrDefault(d => d.Id == p.Id);
            if (deletedPerson != null)
            {
                FakeDataBase.People.Remove(deletedPerson);
            }
        }
    }
}
