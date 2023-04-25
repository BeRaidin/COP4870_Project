using Library.LearningManagement.Services;
using UWP.Library.LearningManagement.Models;
using System.Collections.Generic;
using System.Linq;

namespace UWP.LearningManagement.ViewModels
{
    public class GradesDialogViewModel
    {
        private readonly PersonService personService;
        private readonly CourseService courseService;

        public Assignment SelectedAssignment
        {
            get { return personService.CurrentAssignment; }
            set { personService.CurrentAssignment = value; }
        }
        public Student SelectedStudent
        {
            get { return personService.CurrentPerson as Student; }
            set { personService.CurrentPerson = value; }
        }
        public Course SelectedCourse
        {
            get { return courseService.CurrentCourse; }
        }

        public string Score { get; set; }
        public int Total
        {
            get { return SelectedAssignment.TotalAvailablePoints; }            
        }
        public GradesDialogViewModel() 
        {
            personService = PersonService.Current;
            courseService = CourseService.Current;
        }

        public void AddGrade()
        {
            if (double.TryParse(Score, out var value))
            {
                var grade = SelectedStudent.GetGradeDict(SelectedAssignment);
                    
                if (value > Total)
                {
                    grade.Grade = Total;
                }
                else if (value < 0)
                {
                    grade.Grade = 0;
                }
                else grade.Grade = value;
                GetFinalGrade();
                UpdateGPA();
            }
        }

        public void GetFinalGrade()
        {
            double rawGrade = 0;
            SelectedCourse.GetMaxGrade();
            foreach(GradesDictionary grade in SelectedStudent.Grades)
            {
                if (SelectedCourse.Assignments.Contains(grade.Assignment))
                {
                    rawGrade += ((double)grade.Grade * (grade.Assignment.AssignmentGroup.Weight / (double)100));
                }
            }

            double totalGrade = (double)(rawGrade / SelectedCourse.MaxGrade) * 100;
            var courseGrade = SelectedStudent.FinalGrades.FirstOrDefault(x => x.Key.Id == SelectedCourse.Id);
            courseGrade.Value = totalGrade;
        }

        public void UpdateGPA()
        {
            var totalHours = 0;
            double totalHonorPoints = 0;
            foreach (var grade in SelectedStudent.FinalGrades)
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
            SelectedStudent.GradePointAverage = GPA;
        }

    }
}
