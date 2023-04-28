using System.Collections.Generic;
using System.ComponentModel;
using UWP.Library.LearningManagement.Models;

namespace UWP.Library.LearningManagement.Database
{
    public static class FakeDataBase
    {
        private static List<Person> people = new List<Person>
        {
            new Student {Id = 0, FirstName="Brayden", LastName="Lewis", Classification=Student.Classes.Sophmore},
            new Instructor {Id = 1, FirstName="Chris", LastName="Millls"},
            new TeachingAssistant {Id = 2, FirstName="Joe", LastName="Joey"}
        };
        private static List<Course> courses = new List<Course>{
            new Course {Id = 0, Code="COP4530", Name="C#", CreditHours=4, Room="HCB"}
        };
        private static List<Semester> semesters = new List<Semester>{};
        private static List<Assignment> assignments = new List<Assignment>();
        private static List<Announcement> announcements = new List<Announcement>();
        private static List<Module> modules = new List<Module>();
        private static List<ContentItem> contentItems = new List<ContentItem>();

        public static List<Semester> currentSemester = new List<Semester>
        {
            new Semester{Period = "Spring", Year = 2023, Id = 0 }
        };



        public static List<Course> Courses
        {
            get { return courses; }
        }
        public static List<Semester> Semesters
        {
            get { return semesters; }
        }
        public static List<Assignment> Assignments
        {
            get { return assignments; }
        }
        public static List<Announcement> Announcements
        {
            get { return announcements; }
        }
        public static List<Module> Modules
        {
            get { return modules; }
        }

        public static List<Person> People
        {
            get { return people; }

        }
        public static List<Student> Students
        {
            get
            {
                var returnList = new List<Student>();
                foreach (var person in people)
                {
                    if (person is Student student)
                    {
                        returnList.Add(student);
                    }
                }
                return returnList;
            }
        }
        public static List<Instructor> Instructors
        {
            get
            {
                var returnList = new List<Instructor>();
                foreach (var person in people)
                {
                    if (person is Instructor instructor)
                    {
                        returnList.Add(instructor);
                    }
                }
                return returnList;
            }
        }
        public static List<TeachingAssistant> Assistants
        {
            get
            {
                var returnList = new List<TeachingAssistant>();
                foreach (var person in people)
                {
                    if (person is TeachingAssistant assistant)
                    {
                        returnList.Add(assistant);
                    }
                }
                return returnList;
            }
        }
        public static List<ContentItem> ContentItems
        {
            get
            {
                return contentItems;
            }
        }
        public static List<AssignmentItem> AssignmentItems
        {
            get
            {
                var returnList = new List<AssignmentItem>();
                foreach (var item in contentItems)
                {
                    if (item is AssignmentItem assignment)
                    {
                        returnList.Add(assignment);
                    }
                }
                return returnList;
            }
        }
        public static List<FileItem> FileItems
        {
            get
            {
                var returnList = new List<FileItem>();
                foreach (var item in contentItems)
                {
                    if (item is FileItem file)
                    {
                        returnList.Add(file);
                    }
                }
                return returnList;
            }
        }
        public static List<PageItem> PageItems
        {
            get
            {
                var returnList = new List<PageItem>();
                foreach (var item in contentItems)
                {
                    if (item is PageItem page)
                    {
                        returnList.Add(page);
                    }
                }
                return returnList;
            }
        }

        public static List<Semester> CurrentSemester
        {
            get { return currentSemester; }
        }
    }
}
