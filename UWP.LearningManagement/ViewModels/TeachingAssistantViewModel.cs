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
    public class TeachingAssistantViewModel : AdminViewModel
    {
        public new string Display
        {
            get { return $"[{Person.Id}] {Person.FirstName} {Person.LastName} - Teaching Assistant"; }
        }

        public TeachingAssistantViewModel(InstructorViewViewModel ivm)
        {
            ParentViewModel = ivm;
            if (ParentViewModel.SelectedInstructor == null)
            {
                Person = new TeachingAssistant { Id = "-1" };
            }
            else Person = ParentViewModel.SelectedPerson;
        }

        public TeachingAssistantViewModel(InstructorViewViewModel ivm, TeachingAssistant assistant)
        {
            ParentViewModel = ivm;
            ParentViewModel.SelectedPerson = assistant;
            Person = assistant;
        }
    }
}
