using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class PageItem : ContentItem
    {
        public string HTMLBody { get; set; }

        public override string Display => $"[{Id}] - {Name} - Page";
    }
}
