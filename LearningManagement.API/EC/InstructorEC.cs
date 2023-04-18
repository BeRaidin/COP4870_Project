using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.DTO;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class InstructorEC
    {
        public List<InstructorDTO> GetInstructors()
        {
            var returnList = new List<InstructorDTO>();
            foreach(var person in FakeDataBase.Instructors)
            {
                returnList.Add(new InstructorDTO(person));
            }
            return returnList;
        }

        public List<TeachingAssistantDTO> GetAssistants()
        {
            var returnList = new List<TeachingAssistantDTO>();
            foreach (var person in FakeDataBase.Assistants)
            {
                returnList.Add(new TeachingAssistantDTO(person));
            }
            return returnList;
        }

        public InstructorDTO AddorUpdateInstructor(InstructorDTO i)
        {
            if (int.Parse(i.Id) <= 0)
            {
                var lastId = FakeDataBase.People.Select(p => int.Parse(p.Id)).Max();
                i.Id = lastId++.ToString();
            }

            FakeDataBase.People.Add(new Instructor(i));
            return i;
        }
        public TeachingAssistantDTO AddorUpdateTeachingAssistant(TeachingAssistantDTO t)
        {
            if (int.Parse(t.Id) <= 0)
            {
                var lastId = FakeDataBase.People.Select(p => int.Parse(p.Id)).Max();
                t.Id = lastId++.ToString();
            }
       
            FakeDataBase.People.Add(new TeachingAssistant(t));
            return t;
        }
    }
}
