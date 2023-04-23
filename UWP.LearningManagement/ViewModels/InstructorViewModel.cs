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
    public class InstructorViewModel : AdminViewModel
    {

        public new string Display
        {
            get { return $"[{Person.Id}] {Person.FirstName} {Person.LastName} - Instructor"; }
        }

        public InstructorViewModel(InstructorViewViewModel ivm)
        {
            ParentViewModel = ivm;
            if (ParentViewModel.SelectedInstructor == null)
            {
                Person = new Instructor { Id = -1 };
            }
            else Person = ParentViewModel.SelectedInstructor;
        }

        public InstructorViewModel(InstructorViewViewModel ivm, Instructor instructor)
        {
            ParentViewModel = ivm;
            Person = instructor;
        }


    }
}
