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
    public class Person
    {
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
