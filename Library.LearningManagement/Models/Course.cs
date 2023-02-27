namespace Library.LearningManagement.Models
{
    public class Course
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public string Description { get; set; }
        public List<Person> Roster { get; set; }
        public List<Assignment> Assignments { get; set; }
        public List<Module> Modules { get; set; }
        public List<AssignmentGroup> AssignmentGroups { get; set; }

        public List<Announcement> Announcements { get; set; }

        public Course() 
        { 
            Code = string.Empty;
            Name = string.Empty;
            CreditHours = 3;
            Description = string.Empty;
            Roster = new List<Person>();
            Assignments = new List<Assignment>();
            Modules = new List<Module>();
            AssignmentGroups = new List<AssignmentGroup>();
            Announcements = new List<Announcement>();
        }

        public override string ToString()
        {
            return $"[{Code}] - {Name}";
        }

        public string DetailDisplay
        {
            get
            {
                return $"{ToString()}\n{Description}" +
                    $"\n\nRoster:\n{string.Join("\n", Roster.Select(s => s.ToString()).ToArray())}\n\n" +
                    $"Assignments:\n{string.Join("\n", Assignments.Select(a => a.ToString()).ToArray())}\n\n" +
                    $"Modules:\n{string.Join("\n", Modules.Select(a => a.DetailDisplay).ToArray())}";
            }
        }

        public void ChangeCode(List<Course> course)
        {
            Console.WriteLine("What is the code of the course?");
            var code = Console.ReadLine() ?? string.Empty;

            while (course.Any(p => p.Code == code))
            {
                Console.WriteLine("That is already anohter course code, please enter another");
                code = Console.ReadLine() ?? string.Empty;
            }

            Code = code;
        }

        public void ChangeName()
        {
            Console.WriteLine("What is the name of the course?");
            Name = Console.ReadLine() ?? string.Empty;
        }

        public void ChangeDescription()
        {
            Console.WriteLine("What is the description of the course?");
            Description = Console.ReadLine() ?? string.Empty;
        }

        public void ChangeHours()
        {
            Console.WriteLine("How many credit hours is the course?");
            var hours = Console.ReadLine() ?? string.Empty;
            while (!int.TryParse(hours, out int result))
            {
                Console.WriteLine("Please enter an integer:");
                hours = Console.ReadLine() ?? string.Empty;
            }
            CreditHours = int.Parse(hours);
        }
    }
}
