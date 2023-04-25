using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int TotalAvailablePoints { get; set; }
        public AssignmentGroup AssignmentGroup { get; set; }
        public DateTimeOffset DueDate { get; set; }

        public Assignment()
        {
            Name = string.Empty;
            Description = string.Empty;
            AssignmentGroup = new AssignmentGroup();
        }

        public virtual string Display => $"({DueDate}) {Name} - {AssignmentGroup.Name}";
    }
}
