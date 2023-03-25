using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
{
    public class Assignment
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalAvailablePoints { get; set; }
        public AssignmentGroup AssignmentGroup { get; set; } 
        public DateTime DueDate { get; set; }
        public Decimal Grade { get; set; }

        public Assignment() 
        {
            Name = string.Empty;
            Description = string.Empty;
            AssignmentGroup = new AssignmentGroup();
        }

        public override string ToString()
        {
            return $"({DueDate}) {Name} - {AssignmentGroup.Name}";
        }

        public void UpdateName()
        {
            Console.WriteLine("What is the assignment name?");
            Name = Console.ReadLine() ?? string.Empty;
        }

        public void UpdateDescription()
        {
            Console.WriteLine("What is the assignment description?");
            Description = Console.ReadLine() ?? string.Empty;
        }

        public void UpdateTotalPoints()
        {
            Console.WriteLine("How many points does the assignment have?");
            var totalPoints = Console.ReadLine() ?? string.Empty;
            while (!int.TryParse(totalPoints, out int result))
            {
                Console.WriteLine("Please enter an integer:");
                totalPoints = Console.ReadLine() ?? string.Empty;
            }
            TotalAvailablePoints = int.Parse(totalPoints);
        }

        public void UpdateDueDate() 
        {
            Console.WriteLine("What is the due date?");
            var totalPoints = Console.ReadLine() ?? string.Empty;
            while(!DateTime.TryParse(totalPoints, out DateTime result))
            {
                Console.WriteLine("Please enter a vaild date: (MM/DD/YYYY)");
                totalPoints = Console.ReadLine() ?? string.Empty;
            }
        }

        public void AddAssignmentGroup(AssignmentGroup group)
        {
            AssignmentGroup = group;
        }

        public void UpdateAssignment()
        {
            UpdateName();
            UpdateDescription();
            UpdateTotalPoints();
            UpdateDueDate();
        }
    }
}
