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
                var lastId = FakeDataBase.Assignments.Select(x => x.Id).Max();
                lastId++;
                a.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.Assignments.Add(a);
                FakeDataBase.CurrentSemester[0].Assignments.Add(a);
            }
            else
            {
                var editedAssignment = FakeDataBase.Assignments.FirstOrDefault(x => x.Id == a.Id);
                if (editedAssignment != null)
                {
                    editedAssignment.Name = a.Name;
                    editedAssignment.Description = a.Description;
                    editedAssignment.TotalAvailablePoints = a.TotalAvailablePoints;
                    editedAssignment.DueDate = a.DueDate;

                    var semesterEditedAssignment = FakeDataBase.Assignments.FirstOrDefault(x => x.Id == a.Id);
                    if(semesterEditedAssignment != null)
                    {
                        semesterEditedAssignment.Name = a.Name;
                        semesterEditedAssignment.Description = a.Description;
                        semesterEditedAssignment.TotalAvailablePoints = a.TotalAvailablePoints;
                        semesterEditedAssignment.DueDate = a.DueDate;
                    }
                    return editedAssignment;
                }
            }
            return a;
        }

        public Assignment UpdateIsSelected(Assignment a)
        {
            var editedAssignment = FakeDataBase.Assignments.FirstOrDefault(x => x.Id == a.Id);
            if(editedAssignment != null)
            {
                editedAssignment.IsSubmitted = a.IsSubmitted;
                var semesterEditedAssignment = FakeDataBase.Assignments.FirstOrDefault(x => x.Id == a.Id);
                if (semesterEditedAssignment != null)
                {
                    semesterEditedAssignment.IsSubmitted = a.IsSubmitted;
                }
                return editedAssignment;
            }
            return a;
        }

        public Assignment UpdateIsGraded(Assignment a)
        {
            var editedAssignment = FakeDataBase.Assignments.FirstOrDefault(x => x.Id == a.Id);
            if (editedAssignment != null)
            {
                editedAssignment.IsGraded = a.IsGraded;
                var semesterEditedAssignment = FakeDataBase.Assignments.FirstOrDefault(x => x.Id == a.Id);
                if (semesterEditedAssignment != null)
                {
                    semesterEditedAssignment.IsGraded = a.IsGraded;
                }
                return editedAssignment;
            }
            return a;
        }

        public Assignment UpdateAssignGroup(Assignment a)
        {
            var editedAssignment = FakeDataBase.Assignments.FirstOrDefault(x => x.Id == a.Id);
            if (editedAssignment != null)
            {
                editedAssignment.AssignmentGroup = a.AssignmentGroup;
                var semesterEditedAssignment = FakeDataBase.Assignments.FirstOrDefault(x => x.Id == a.Id);
                if (semesterEditedAssignment != null)
                {
                    semesterEditedAssignment.AssignmentGroup = a.AssignmentGroup;
                }
                return editedAssignment;
            }
            return a;
        }

        public void Delete(Assignment a)
        {
            var deletedAssignment = FakeDataBase.Assignments.FirstOrDefault(x => x.Id == a.Id);
            if (deletedAssignment != null)
            {
                FakeDataBase.Assignments.Remove(deletedAssignment);
                var semesterDeletedAssignment = FakeDataBase.CurrentSemester[0].Assignments.FirstOrDefault(x => x.Id == a.Id);
                if (semesterDeletedAssignment != null)
                {
                    FakeDataBase.CurrentSemester[0].Assignments.Remove(semesterDeletedAssignment);
                }
            }
        }
    }
}
