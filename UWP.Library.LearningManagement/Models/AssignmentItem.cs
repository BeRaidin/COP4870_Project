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

        public override string Display => $"[{Id}] - {Name} - Assignment";
    }
}
