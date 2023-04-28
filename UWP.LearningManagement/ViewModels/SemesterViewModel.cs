using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.Models;

namespace UWP.LearningManagement.ViewModels
{
    public class SemesterViewModel
    {
        private List<Semester> SemesterList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Semester/GetList").Result;
                return JsonConvert.DeserializeObject<List<Semester>>(payload);
            }
        }

        public Semester Semester { get; set; }

        public SemesterViewModel(){}

        public SemesterViewModel(int Id)
        {
            Semester = SemesterList.FirstOrDefault(x => x.Id == Id);
        }
    }
}
