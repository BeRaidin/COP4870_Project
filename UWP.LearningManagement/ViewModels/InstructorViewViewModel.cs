﻿using UWP.Library.LearningManagement.Models;
using Library.LearningManagement.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UWP.LearningManagement.Dialogs;
using UWP.Library.LearningManagement.Database;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using UWP.Library.LearningManagement.DTO;

namespace UWP.LearningManagement.ViewModels
{
    public class InstructorViewViewModel
    {
        private readonly PersonService personService;
        private readonly SemesterService semesterService;


        public IEnumerable<InstructorViewModel> AllInstructors
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Instructor").Result;
                var returnVal = JsonConvert.DeserializeObject<List<InstructorDTO>>(payload).Select(d => new InstructorViewModel(d));
                return returnVal;
            }
        }

        public ObservableCollection<InstructorViewModel> Instructors { get; set; }
        public Person SelectedPerson
        {
            get { return personService.CurrentPerson; }
            set { personService.CurrentPerson = value; }
        }
        public Semester SelectedSemester
        {
            get { return semesterService.CurrentSemester; }
        }
        public string Semester
        {
            get { return semesterService.CurrentSemester.Period; }
        }
        public int Year
        {
            get { return semesterService.CurrentSemester.Year; }
        }
        public string Query { get; set; }

        public InstructorViewViewModel()
        {
            personService = PersonService.Current;
            semesterService = SemesterService.Current;
            Instructors = new ObservableCollection<InstructorViewModel>(AllInstructors);
        }

        public void Search()
        {
            if (Query != null && Query != "")
            {

                IEnumerable<InstructorViewModel> searchResults = AllInstructors.Where(i => i.Dto.FirstName.Contains(Query, StringComparison.InvariantCultureIgnoreCase)
                                                    || i.Dto.Id.Contains(Query, StringComparison.InvariantCultureIgnoreCase));
                Instructors.Clear();
                foreach (var person in searchResults)
                {
                    Instructors.Add(person);
                }
            }
            else
            {
                Refresh();
            }
        }

        

        public async void Add()
        {
            SelectedPerson = new Person();
            var dialog = new PersonDialog();
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            if(!dialog.TestValid())
            {
                var errorDialog = new ErrorDialog();
                if (errorDialog != null)
                {
                    await errorDialog.ShowAsync();
                }
            }
            Refresh();
        }

        public void Refresh()
        {
            Instructors.Clear();
            foreach (var person in AllInstructors)
            {
                Instructors.Add(person);
            }
        }
    }
}
