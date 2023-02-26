﻿using Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System.Net.Mime;

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

        public void AddOrUpdateCourse(Course? selectedCourse = null)
        {
            if (selectedCourse == null)
            {
                selectedCourse = new Course();
                selectedCourse.ChangeCode(courseService.Courses);
                selectedCourse.ChangeName();
                selectedCourse.ChangeDescription();

                Console.WriteLine("Which students should be enrolled in the course? ('Q' to quit)");
                var roster = new List<Person>();
                bool contAdding = true;
                while (contAdding)
                {
                    personService.People.Where(s => !roster.Any(s2 => s2.Id == s.Id)).ToList().ForEach(Console.WriteLine);
                    var selection = "Q";
                    if (personService.People.Any(s => !roster.Any(s2 => s2.Id == s.Id)))
                    {
                        selection = Console.ReadLine() ?? string.Empty;
                        if(selection == string.Empty)
                        {
                            selection = "Q";
                        }
                    }

                    if (selection.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                    {
                        contAdding = false;
                    }
                    else
                    {
                        var selectedId = int.Parse(selection);
                        var selectedStudent = personService.People.FirstOrDefault(s => s.Id == selectedId);

                        if (selectedStudent != null)
                        {
                            roster.Add(selectedStudent);
                        }
                    }
                }
                selectedCourse.Roster = roster;

                Console.WriteLine("Would you like to add assignments? (Y/N)");
                var assignResponse = Console.ReadLine() ?? "N";
                var assignments = new List<Assignment>();
                if (assignResponse.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    contAdding = true;
                    while (contAdding)
                    {
                        Console.WriteLine("Name:");
                        var assignmentName = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Description:");
                        var assignmentDescription = Console.ReadLine() ?? string.Empty;
                        Console.WriteLine("Total Points:");
                        var totalPoints = decimal.Parse(Console.ReadLine() ?? "100");
                        Console.WriteLine("Due Date:");
                        var dueDate = DateTime.Parse(Console.ReadLine() ?? "01/01/1990");

                        var assignmnetGroup = new AssignmentGroup();
                        if (!selectedCourse.AssignmentGroups.Any())
                        {
                            Console.WriteLine("What is the name of the assignment group?");
                            var groupName = Console.ReadLine() ?? string.Empty;
                            Console.WriteLine("What is the weight of the assignment group?");
                            var groupWeight = Console.ReadLine() ?? "10";
                            while(!int.TryParse(groupWeight, out int weightInt))
                            {
                                Console.WriteLine("Please enter a whole number:");
                                groupWeight = Console.ReadLine() ?? "10";
                            }
                            assignmnetGroup = new AssignmentGroup(groupName, int.Parse(groupWeight));
                            selectedCourse.AssignmentGroups.Add(assignmnetGroup);
                        }
                        else
                        {
                            Console.WriteLine("Choose an existing assignment group, or make a new one:");
                            selectedCourse.AssignmentGroups.ForEach(Console.WriteLine);
                            var groupName = Console.ReadLine() ?? string.Empty;
                            if (selectedCourse.AssignmentGroups.Any(g => g.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase)))
                            {
                                assignmnetGroup = selectedCourse.AssignmentGroups.First(g => g.Name.Equals(groupName, StringComparison.InvariantCultureIgnoreCase));
                            }
                            else
                            {
                                Console.WriteLine("What is the weight of the assignment group?");
                                var groupWeight = Console.ReadLine() ?? "10";
                                while (!int.TryParse(groupWeight, out int weightInt))
                                {
                                    Console.WriteLine("Please enter a whole number:");
                                    groupWeight = Console.ReadLine() ?? "10";
                                }
                                assignmnetGroup = new AssignmentGroup(groupName, int.Parse(groupWeight));
                                selectedCourse.AssignmentGroups.Add(assignmnetGroup);
                            }
                        }

                        assignments.Add(new Assignment
                        {
                            Name = assignmentName,
                            Description = assignmentDescription,
                            TotalAvailablePoints = totalPoints,
                            DueDate = dueDate,
                            AssignmentGroup = assignmnetGroup
                        });

                        Console.WriteLine("Add more assignments? (Y/N)");
                        assignResponse = Console.ReadLine() ?? "N";
                        if (assignResponse.Equals("N", StringComparison.InvariantCultureIgnoreCase))
                        {
                            contAdding = false;
                        }
                    }
                }

                selectedCourse.Assignments.AddRange(assignments);

                courseService.Add(selectedCourse);
            }
            else
            {
                Console.WriteLine("Would you like to change the Code? (Y/N)");
                var choice = Console.ReadLine() ?? string.Empty;
                if(choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedCourse.ChangeCode(courseService.Courses);
                }
                Console.WriteLine("Would you like to change the Name? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedCourse.ChangeName();
                }
                Console.WriteLine("Would you like to change the Description? (Y/N)");
                choice = Console.ReadLine() ?? string.Empty;
                if (choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
                {
                    selectedCourse.ChangeDescription();
                }
             }
        }

        public void UpdateCourseRecord()
        {
            Console.WriteLine("Choose the code of course to update:");
            SearchOrListCourses();
            var selection = Console.ReadLine();
            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
                if (selectedCourse != null)
                {
                    AddOrUpdateCourse(selectedCourse);
                }
        }

        public void SearchOrListCourses(string? query = null)
        {
            if (query == null)
            {
                courseService.Courses.ForEach(Console.WriteLine);
            }
            else
            {
                courseService.Search(query).ToList().ForEach(Console.WriteLine);

                Console.WriteLine("Select a course to see more information:");
                var code = Console.ReadLine() ?? string.Empty;
                var selectedCourse = courseService.Courses
                    .FirstOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
                if (selectedCourse != null)
                {
                    Console.WriteLine(selectedCourse.DetailDisplay);
                }
            }            
        }

        public void AddModule()
        {
            Console.WriteLine("Choose the code of course you want to add a module to:");
            SearchOrListCourses();
            var selection = Console.ReadLine();
            var selectedCourse = courseService.Courses.FirstOrDefault(s => s.Code.Equals(selection, StringComparison.InvariantCultureIgnoreCase));
            if (selectedCourse != null)
            {
                var module = new Module();
                module.ChangeName();
                module.ChangeDescription();
                Console.WriteLine("Would you like to add content? (Y/N)");
                var choice = Console.ReadLine() ?? string.Empty;
                while(choice.Equals("Y", StringComparison.InvariantCultureIgnoreCase))
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

                    Console.WriteLine("What is the name of the content?");
                    content.Name = Console.ReadLine() ?? string.Empty;
                    Console.WriteLine("What is the description of the content?");
                    content.Description = Console.ReadLine() ?? string.Empty;

                    if (content is AssignmentItem)
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
                            module.Content.Add(assignment);
                        }
                    }
                    else
                    {
                        module.Content.Add(content);
                    }

                    Console.WriteLine("Would you like to add more content? (Y/N)");
                    choice = Console.ReadLine() ?? string.Empty;
                }
                selectedCourse.Modules.Add(module);
            }
        }
    }
}
