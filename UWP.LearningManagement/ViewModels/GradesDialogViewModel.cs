using Library.LearningManagement.Services;
using UWP.Library.LearningManagement.Models;
using System.Collections.Generic;
using System.Linq;
using UWP.LearningManagement.API.Util;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class GradesDialogViewModel
    {
        public Assignment Assignment { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public string Score { get; set; }
        public GradesDialogViewModel() { }
        public GradesDialogViewModel(int assignmentId, int personId, int courseId)
        {
            AssignmentViewModel assignmentView = new AssignmentViewModel(assignmentId);
            StudentViewModel studentView = new StudentViewModel(personId);
            CourseViewModel courseView = new CourseViewModel(-1, courseId);
            Assignment = assignmentView.Assignment;
            Student = studentView.Student;
            Course = courseView.Course;
        }

        public async Task AddGrade()
        {
            if (double.TryParse(Score, out var value))
            {
                var grade = Student.GetGradeDict(Assignment);

                if (value > Assignment.TotalAvailablePoints)
                {
                    grade.Grade = Assignment.TotalAvailablePoints;
                }
                else if (value < 0)
                {
                    grade.Grade = 0;
                }
                else grade.Grade = value;
                await GetFinalGrade();
                await UpdateGPA();
            }
        }

        public async Task GetFinalGrade()
        {
            double rawGrade = 0;
            Course.GetMaxGrade();
            foreach (GradesDictionary grade in Student.Grades)
            {
                if (Course.Assignments.Any(x => x.Id == grade.Assignment.Id))
                {
                    double percentage = grade.Assignment.AssignmentGroup.Weight / (double)100;

                    rawGrade += ((double)grade.Grade * percentage);
                }
            }
            double totalGrade = (double)(rawGrade / Course.MaxGrade) * 100;
            Student.UpdateFinalGrade(totalGrade, Course.Id);
            await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateFinalGrades", Student);
        }

        public async Task UpdateGPA()
        {
            var totalHours = 0;
            double totalHonorPoints = 0;
            foreach (var grade in Student.FinalGrades)
            {
                var courseHonorPoints = (double)0.0;
                totalHours += grade.Key.CreditHours;
                if (grade.Value < 70)
                {
                    courseHonorPoints = 0.0;
                }
                else if (grade.Value < 80)
                {
                    courseHonorPoints = 2.0;
                }
                else if (grade.Value < 90)
                {
                    courseHonorPoints = 3.0;
                }
                else
                {
                    courseHonorPoints = 4.0;
                }
                totalHonorPoints += (double)courseHonorPoints * grade.Key.CreditHours;
            }
            double GPA = (double)totalHonorPoints / totalHours;
            Student.GradePointAverage = GPA;
            await new WebRequestHandler().Post("http://localhost:5159/Person/UpdateGPA", Student);
        }

    }
}
