using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class AssignmentEC
    {
        public List<Assignment> GetAssignments()
        {
            return FakeDataBase.Assignments;
        }

        public Assignment AddOrUpdateAssignment(Assignment a)
        {
            bool isNew = false;
            if (FakeDataBase.Assignments.Count == 0)
            {
                a.Id = 0;
                isNew = true;
            }
            else if (a.Id < 0)
            {
                var lastId = FakeDataBase.Assignments.Select(p => p.Id).Max();
                lastId++;
                a.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.Assignments.Add(a);
            }
            else
            {
                var editedAssignment = FakeDataBase.Assignments.FirstOrDefault(i => i.Id == a.Id);
                if (editedAssignment != null)
                {
                    editedAssignment.Name = a.Name;
                    editedAssignment.Description = a.Description;
                    editedAssignment.TotalAvailablePoints = a.TotalAvailablePoints;
                    return editedAssignment;
                }
            }
            return a;
        }

        public void Delete(Assignment a)
        {

            var deletedAssignment = FakeDataBase.Assignments.FirstOrDefault(d => d.Id == a.Id);
            if (deletedAssignment != null)
            {
                FakeDataBase.Assignments.Remove(deletedAssignment);
            }
        }
    }
}
