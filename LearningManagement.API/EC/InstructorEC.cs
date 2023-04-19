using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class InstructorEC
    {
        public List<Instructor> GetInstructors()
        {
            return FakeDataBase.Instructors;
        }

        public List<TeachingAssistant> GetAssistants()
        {
            return FakeDataBase.Assistants;
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
                foreach(var person in FakeDataBase.People)
                {
                    if(person.Id == p.Id)
                    {
                        person.FirstName = p.FirstName;
                        person.LastName = p.LastName;
                        break;
                    }
                }
            }
            return p;
        }
    }
}
