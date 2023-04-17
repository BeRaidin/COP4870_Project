using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class AssignmentGroup
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public AssignmentGroup()
        {
            Name = string.Empty;
            Weight = 0;
        }
        public virtual string Display => $"[{Name}] - {Weight}%";
    }
}
