using Library.LearningManagement.Models;
using Library.LearningManagement.Services;

namespace App.LearningManagement.Helpers
{
    internal class CourseHelper
    {
        private CourseService courseService;
        private PersonService personService;

        public CourseHelper()
        {
            personService = PersonService.Current;
            courseService= CourseService.Current;
        }

        public Course GetCourse()
        {
            Console.WriteLine("Enter the code of a course:");
            ListCourses();
            var selection = Console.ReadLine();
            var selectedCourse = courseService.Courses
                .FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            while (selectedCourse == null)
            {
                Console.WriteLine("Please enter a vaild code:");
                selection = Console.ReadLine();
                selectedCourse = courseService.Courses
                    .FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            }
            return selectedCourse;
        }

        public void ListCourses()
        {
            if (personService.People.Count > 5)
            {
                var navigator = new ListNavigator<Course>(courseService.Courses);
                navigator.ChangePage();
            }
            else
            {
                courseService.Courses.ForEach(Console.WriteLine);
            }
        }

        public void AddCourse()
        {
                var course = new Course();
                course.ChangeCode(courseService.Courses);
                course.ChangeName();
                course.ChangeHours();
                course.ChangeDescription();
                AddStuToCourse(course);
                AddAssignments(course);
                courseService.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            Console.WriteLine("Would you like to change the Code? (Y/N)");
            var choice = Console.ReadLine() ?? string.Empty;
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                course.ChangeCode(courseService.Courses);
            }
            Console.WriteLine("Would you like to change the Name? (Y/N)");
            choice = Console.ReadLine() ?? string.Empty;
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                course.ChangeName();
            }
            Console.WriteLine("Would you like to change the amount of credit hours? (Y/N)");
            choice = Console.ReadLine() ?? string.Empty;
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                course.ChangeHours();
            }
            Console.WriteLine("Would you like to change the Description? (Y/N)");
            choice = Console.ReadLine() ?? string.Empty;
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                course.ChangeDescription();
            }
        }






        public void UpdateCourseModule()
        {
            var selectedCourse = GetCourse();
            if (selectedCourse != null)
            {
                Console.WriteLine("What module would you want to update?");
                selectedCourse.Modules.ForEach(Console.WriteLine);
                var selectedModule = Console.ReadLine();
                if (selectedCourse.Modules.Any(i => i.Name.Equals(selectedModule, StringComparison.InvariantCultureIgnoreCase)))
                {
                    var module = selectedCourse.Modules.First(i => i.Name.Equals(selectedModule, StringComparison.InvariantCultureIgnoreCase));
                    AddOrUpdateModule(selectedCourse, module);
                }
            }
        }

        public void SearchCourses(string? query = null)
        {
            var courseList = new List<Course>();
            if (query == null)
            {
                courseList = courseService.Courses;
            }
            else
            {
                courseList = courseService.Search(query).ToList();
            }
            courseList.ForEach(Console.WriteLine);
            Console.WriteLine("Which Course would you like to see the details of?");
            var selectedCourse = Console.ReadLine();
            if (courseList.Any(c => c.Code.Equals(selectedCourse, StringComparison.InvariantCultureIgnoreCase))) 
            {
                var displayCourse = courseList.First(c => c.Code.Equals(selectedCourse, StringComparison.InvariantCultureIgnoreCase));
                Console.WriteLine(displayCourse.DetailDisplay);
            }
        }
        
        public void AddOrUpdateModule(Course? selectedCourse = null, Module? selectedModule = null)
        {
            if (selectedCourse == null)
            {
                selectedCourse = GetCourse();
            }
            var isNew = false;
            if (selectedModule == null)
            {
                isNew = true;
                selectedModule = new Module();
            }
            var choice = "Y";
            if(!isNew)
            {
                Console.WriteLine("Would you like to update the name? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
            }
            if(choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                selectedModule.ChangeName();
            }
            if (!isNew)
            {
                Console.WriteLine("Would you like to update the description? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
            }
            if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                selectedModule.ChangeDescription();
            }
            Console.WriteLine("Would you like to add content? (Y/N)");
            choice = Console.ReadLine() ?? string.Empty;
            while (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                Console.WriteLine("What type of content are you adding? ");
                Console.WriteLine("(A)ssignment");
                Console.WriteLine("(F)ile");
                Console.WriteLine("(P)age");
                var contentChoice = Console.ReadLine() ?? string.Empty;
                var content = new ContentItem();
                if(contentChoice.Equals("A", StringComparison.InvariantCultureIgnoreCase))
                {
                    content = new AssignmentItem();
                }
                else if(contentChoice.Equals("F", StringComparison.InvariantCultureIgnoreCase))
                {
                    content = new FileItem();
                }
                else if (contentChoice.Equals("P", StringComparison.InvariantCultureIgnoreCase))
                {
                    content = new PageItem();
                }
                content.Id = selectedModule.Content.Count + 1;
                Console.WriteLine("What is the name of the content?");
                content.Name = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("What is the description of the content?");
                content.Description = Console.ReadLine() ?? string.Empty;

                if (content is AssignmentItem && selectedCourse != null)
                {
                    Console.WriteLine("What assingment would you like to add?");
                    selectedCourse.Assignments.ForEach(Console.WriteLine);
                    var assignmentName = Console.ReadLine() ?? string.Empty;
                    while(!selectedCourse.Assignments.Any(n => n.Name.Equals(assignmentName, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        Console.WriteLine("Please enter a valid assignment");
                        assignmentName = Console.ReadLine() ?? string.Empty;
                    }
                    var selectedAssignment = selectedCourse.Assignments.First(n => n.Name.Equals(assignmentName, StringComparison.InvariantCultureIgnoreCase));
                    var assignment = content as AssignmentItem;
                    if (assignment != null)
                    {
                        assignment.Assignment = selectedAssignment;
                        selectedModule.Content.Add(assignment);
                    }
                }
                else
                {
                    selectedModule.Content.Add(content);
                }
                Console.WriteLine("Would you like to add more content? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
            }
            if (selectedCourse != null && isNew) 
            {
                selectedCourse.Modules.Add(selectedModule);
            }
        }

        public void AddOrUpdateAnnouncement(bool? update = false)
        {
            var selectedCourse = GetCourse();
            if (selectedCourse != null)
            {
                var announcement = new Announcement();
                var delete = "N";
                if(update == false)
                {
                    announcement.Id = selectedCourse.Announcements.Count + 1;
                }
                else
                {
                    Console.WriteLine("Which announcement do you want to update?");
                    selectedCourse.Announcements.ForEach(Console.WriteLine);
                    var selection = Console.ReadLine() ?? string.Empty;
                    while(!int.TryParse(selection, out int test) || !selectedCourse.Announcements.Any(i => i.Id == int.Parse(selection)))
                    {
                        Console.WriteLine("Please enter a vaild integer:");
                        selection = Console.ReadLine() ?? string.Empty;
                    }
                    announcement = selectedCourse.Announcements.First(i => i.Id == int.Parse(selection));

                    Console.WriteLine("Would you like to delte this announcement? (Y/N)");
                    delete = Console.ReadLine() ?? string.Empty;
                }
                if (update == true && delete.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedCourse.Announcements.Remove(announcement);
                }
                else
                {
                    var choice = "Y";
                    if (update == true)
                    {
                        Console.WriteLine("Would you like to update the title? (Y/N)");
                        choice = Console.ReadLine() ?? string.Empty;
                    }
                    if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        announcement.ChangeTitle();
                    }
                    if (update == true)
                    {
                        Console.WriteLine("Would you like to update the message? (Y/N)");
                        choice = Console.ReadLine() ?? string.Empty;
                    }
                    if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                    {
                        announcement.ChangeMessage();
                    }
                    if (update == false)
                    {
                        selectedCourse.Announcements.Add(announcement);
                    }
                }
            }
        }

        public void DeleteModule()
        {
            var selectedCourse = GetCourse();
            if (selectedCourse != null)
            {
                Console.WriteLine("Which module would you like to delete?");
                selectedCourse.Modules.ForEach(Console.WriteLine);
                var module = Console.ReadLine();
                while(!selectedCourse.Modules.Any(m => m.Name == module))
                {
                    Console.WriteLine("Please choose a valid module:");
                    module = Console.ReadLine();
                }
                var removeModule = selectedCourse.Modules.First(m => m.Name == module);
                selectedCourse.Modules.Remove(removeModule);
            }
        }





        public void AddStuToCourse(Course course)
        {
            if (personService.People.Count > 0)
            {
                Console.WriteLine("Which students should be enrolled in the course? ('Q' to quit)");
                var cont = true;
                while (cont)
                {
                    var selection = "Q";
                    if (personService.People.Any(s => !course.Roster.Any(s2 => s2.Id == s.Id)))
                    {
                        personService.People.Where(s => !course.Roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                        selection = Console.ReadLine() ?? string.Empty;
                        if (selection == string.Empty)
                        {
                            selection = "Q";
                        }
                    }

                    if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                    {
                        cont = false;
                    }
                    else if (int.TryParse(selection, out var id))
                    {
                        var selectedStudent = personService.People.FirstOrDefault(s => s.Id == id);

                        if (selectedStudent != null)
                        {
                            course.Roster.Add(selectedStudent);
                            selectedStudent.AddCourse(course);
                        }
                        else Console.WriteLine("Enter a valid student id");
                    }
                    else Console.WriteLine("Enter a valid student id");
                }
            }
        }

        public void AddAssignments(Course course)
        {
            Console.WriteLine("Would you like to add assignments? (Y/N)");
            var selection = Console.ReadLine() ?? string.Empty;
            var assignments = new List<Assignment>();
            if (selection.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
            {
                var cont = true;
                while (cont)
                {
                    var assignment = new Assignment();
                    assignment.UpdateAssignment();

                    var assignmentGroup = new AssignmentGroup();
                    if (!course.AssignmentGroups.Any())
                    {
                        assignmentGroup = assignmentGroup.MakeAssignmentGroup();
                        course.AssignmentGroups.Add(assignmentGroup);
                    }
                    else
                    {
                        Console.WriteLine("Choose an existing assignment group, or make a new one:");
                        course.AssignmentGroups.ForEach(Console.WriteLine);
                        var groupName = Console.ReadLine() ?? string.Empty;
                        while (groupName == null)
                        {
                            Console.WriteLine("Please enter a name:");
                            groupName = Console.ReadLine() ?? string.Empty;
                        }
                        if (course.AssignmentGroups.Any(g => g.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase)))
                        {
                            assignmentGroup = course.AssignmentGroups.First(g => g.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase));
                        }
                        else
                        {
                            assignmentGroup = assignmentGroup.MakeAssignmentGroup(groupName);
                            course.AssignmentGroups.Add(assignmentGroup);
                        }
                    }
                    assignment.AddAssignmentGroup(assignmentGroup);
                    assignments.Add(assignment);
                    course.AddMaxGrade((double)assignment.TotalAvailablePoints * ((double)assignmentGroup.Weight / 100));

                    Console.WriteLine("Add more assignments? (Y/N)");
                    selection = Console.ReadLine() ?? "N";
                    if (selection.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                    {
                        cont = false;
                    }
                }
            }
            course.Assignments.AddRange(assignments);
        }





    }
}
