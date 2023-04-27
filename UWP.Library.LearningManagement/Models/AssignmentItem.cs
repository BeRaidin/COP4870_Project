using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class AssignmentItem : ContentItem
    {
        public Assignment Assignment { get; set; }

        public AssignmentItem()
        {
            Assignment = new Assignment();
        }

        public AssignmentItem(Assignment assignment)
        {
            Assignment = assignment;
            Name = assignment.Name;
            Description = assignment.Description;
            Id = -1;
        }

        public override string Display => $"[{Name} - Assignment";
    }
}
