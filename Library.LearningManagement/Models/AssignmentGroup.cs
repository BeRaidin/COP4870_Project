using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Models
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

        public AssignmentGroup(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"[{Name}] - {Weight}%";
        }
    }
}
