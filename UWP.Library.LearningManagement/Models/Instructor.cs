using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class Instructor : Person
    {
        public override string Display => $"[{Id}] {FirstName} {LastName}  - Instructor";
    }
}
