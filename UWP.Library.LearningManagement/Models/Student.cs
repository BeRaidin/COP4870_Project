﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class Student : Person
    {
        public List<GradesDictionary> Grades { get; set; }
        public Classes Classification { get; set; }
        public enum Classes
        {
            Freshman, Sophmore, Junior, Senior
        }
        public List<FinalGradesDictionary> FinalGrades { get; set; }
        public double GradePointAverage { get; set; }

        public override string Display => $"[{Id}] {FirstName} {LastName} - {Classification}";

        public Student()
        {
            Grades = new List<GradesDictionary>();
            FinalGrades = new List<FinalGradesDictionary>();
            IsSelected = false;
        }

        public void Remove(Assignment assignment)
        {
            GradesDictionary removedGrade = Grades.FirstOrDefault(x  => x.Assignment.Id == assignment.Id);
            Grades.Remove(removedGrade);
        }

        public GradesDictionary GetGradeDict(Assignment assignment)
        {
            foreach (var grade in Grades)
            {
                if (grade.Assignment.Id == assignment.Id)
                {
                    return grade;
                }
            }
            return null;
        }

        public void UpdateFinalGrade(double grade, int courseId)
        {
            var oldCourseGrade = FinalGrades.FirstOrDefault(x => x.Key.Id == courseId);
            oldCourseGrade.Value = grade;
        }

        public void Add(GradesDictionary grade)
        {
            GradesDictionary oldGrade = Grades.FirstOrDefault(x => x.Assignment.Id == grade.Assignment.Id);
            if(oldGrade != null)
            {
                Grades.Remove(oldGrade);
                Grades.Add(grade);
            }
        }
    }
}
