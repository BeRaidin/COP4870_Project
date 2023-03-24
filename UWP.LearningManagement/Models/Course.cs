using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Composition;
using Windows.UI.WebUI;
using Windows.UI.Xaml;

namespace UWP.LearningManagement.Models
{
    public class Course
    {
        public string Name { get; set; }
        public int CreditHours { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Description}";
        }
    }
}
