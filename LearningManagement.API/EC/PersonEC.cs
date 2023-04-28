using UWP.Library.LearningManagement.Database;
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
                var lastId = FakeDataBase.People.Select(x => x.Id).Max();
                lastId++;
                s.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.People.Add(s);
                FakeDataBase.CurrentSemester[0].People.Add(s);
            }
            else
            {
                if (FakeDataBase.People.FirstOrDefault(x => x.Id == s.Id) is Student editedStudent)
                {
                    editedStudent.FirstName = s.FirstName;
                    editedStudent.LastName = s.LastName;
                    editedStudent.Classification = s.Classification;

                    if (FakeDataBase.CurrentSemester[0].People.FirstOrDefault(x => x.Id == s.Id) is Student semesterEditedStudent)
                    {
                        semesterEditedStudent.FirstName = s.FirstName;
                        semesterEditedStudent.LastName = s.LastName;
                        semesterEditedStudent.Classification = s.Classification;
                    }
                    return editedStudent;
                }
            }
            return s;
        }

        public Person AddorUpdateAdmin(Person i)
        {
            bool isNew = false;
            if (FakeDataBase.People.Count == 0)
            {
                i.Id = 0;
                isNew = true;
            }
            else if (i.Id < 0)
            {
                var lastId = FakeDataBase.People.Select(x => x.Id).Max();
                lastId++;
                i.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                if (i is Instructor instructor)
                {
                    FakeDataBase.People.Add(instructor);
                    FakeDataBase.CurrentSemester[0].People.Add(instructor);
                }
                else if (i is TeachingAssistant assistant)
                {
                    FakeDataBase.People.Add(assistant);
                    FakeDataBase.CurrentSemester[0].People.Add(assistant);
                }
            }
            else
            {
                var editedInstructor = FakeDataBase.People.FirstOrDefault(x => x.Id == i.Id);
                if (editedInstructor != null)
                {
                    editedInstructor.FirstName = i.FirstName;
                    editedInstructor.LastName = i.LastName;

                    var semesterEditedInstructor = FakeDataBase.CurrentSemester[0].People.FirstOrDefault(x => x.Id == i.Id);
                    if (semesterEditedInstructor != null)
                    {
                        semesterEditedInstructor.FirstName = i.FirstName;
                        semesterEditedInstructor.LastName = i.LastName;
                    }
                    return editedInstructor;
                }
            }
            return i;
        }

        public Person UpdateCourses(Person p)
        {
            var editedPerson = FakeDataBase.People.FirstOrDefault(x => x.Id == p.Id);
            if (editedPerson != null)
            {
                editedPerson.Courses = p.Courses;

                var semesterEditedPerson = FakeDataBase.CurrentSemester[0].People.FirstOrDefault(x => x.Id == p.Id);
                if (semesterEditedPerson != null)
                {
                    semesterEditedPerson.Courses = p.Courses;
                }
                return editedPerson;
            }
            return p;
        }

        public Student UpdateStudentCourses(Student s)
        {
            var editedStudent = FakeDataBase.Students.FirstOrDefault(x => x.Id == s.Id);
            if (editedStudent != null)
            {
                editedStudent.Courses = s.Courses;
                editedStudent.FinalGrades = s.FinalGrades;
                editedStudent.Grades = s.Grades;

                if (FakeDataBase.CurrentSemester[0].People.FirstOrDefault(x => x.Id == s.Id) is Student semesterEditedStudent)
                {
                    semesterEditedStudent.Courses = s.Courses;
                    semesterEditedStudent.FinalGrades = s.FinalGrades;
                    semesterEditedStudent.Grades = s.Grades;
                }
                return editedStudent;
            }
            return s;
        }

        public Student UpdateFinalGrades(Student s)
        {
            var editedStudent = FakeDataBase.Students.FirstOrDefault(x => x.Id == s.Id);
            if (editedStudent != null)
            {
                editedStudent.FinalGrades = s.FinalGrades;

                if (FakeDataBase.CurrentSemester[0].People.FirstOrDefault(x => x.Id == s.Id) is Student semesterEditedStudent)
                {
                    semesterEditedStudent.FinalGrades = s.FinalGrades;
                }
                return editedStudent;
            }
            return s;
        }

        public Student UpdateGPA(Student s)
        {
            var editedStudent = FakeDataBase.Students.FirstOrDefault(x => x.Id == s.Id);
            if (editedStudent != null)
            {
                editedStudent.GradePointAverage = s.GradePointAverage;

                if (FakeDataBase.CurrentSemester[0].People.FirstOrDefault(x => x.Id == s.Id) is Student semesterEditedStudent)
                {
                    semesterEditedStudent.GradePointAverage = s.GradePointAverage;
                }
                return editedStudent;
            }
            return s;
        }

        public Student SetTrue(Student s)
        {
            var editedStudent = FakeDataBase.Students.FirstOrDefault(x => x.Id == s.Id);
            if (editedStudent != null)
            {
                editedStudent.IsSelected = true;

                if (FakeDataBase.CurrentSemester[0].People.FirstOrDefault(x => x.Id == s.Id) is Student semesterEditedStudent)
                {
                    semesterEditedStudent.IsSelected = true;
                }
                return editedStudent;
            }
            return s;
        }
        public Student SetFalse(Student s)
        {
            var editedStudent = FakeDataBase.Students.FirstOrDefault(x => x.Id == s.Id);
            if (editedStudent != null)
            {
                editedStudent.IsSelected = false;

                if (FakeDataBase.CurrentSemester[0].People.FirstOrDefault(x => x.Id == s.Id) is Student semesterEditedStudent)
                {
                    semesterEditedStudent.IsSelected = false;
                }
                return editedStudent;
            }
            return s;
        }

        public void Delete(Person p)
        {

            var deletedPerson = FakeDataBase.People.FirstOrDefault(x => x.Id == p.Id);
            if (deletedPerson != null)
            {
                FakeDataBase.People.Remove(deletedPerson);

                var semesterDeletedPerson = FakeDataBase.CurrentSemester[0].People.FirstOrDefault(x => x.Id == p.Id);
                if(semesterDeletedPerson != null) 
                {
                    FakeDataBase.CurrentSemester[0].People.Remove(semesterDeletedPerson);
                }
            }
        }
    }
}
