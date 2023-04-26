using Library.LearningManagement.Services;
using UWP.Library.LearningManagement.Models;
using UWP.LearningManagement.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using Newtonsoft.Json;
using UWP.LearningManagement.API.Util;
using Windows.ApplicationModel.UserActivities.Core;
using Windows.UI.WebUI;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;

namespace UWP.LearningManagement.ViewModels
{
    public class CourseDetailsViewModel
    {
        private readonly int Id;
        private List<Course> CourseList
        {
            get
            {
                var payload = new WebRequestHandler().Get("http://localhost:5159/Course").Result;
                return JsonConvert.DeserializeObject<List<Course>>(payload).ToList();
            }
        }
        public Course Course { get; set; }


        public AssignmentViewModel SelectedAssignment { get; set; }
        public AnnouncementViewModel SelectedAnnouncement { get; set; }
        public ModuleViewModel SelectedModule { get; set; }


        public ObservableCollection<StudentViewModel> Roster { get; set; }
        public ObservableCollection<AdminViewModel> Admin { get; set; }
        public ObservableCollection<AnnouncementViewModel> Announcements { get; set; }
        public ObservableCollection<ModuleViewModel> Modules { get; set; }
        public ObservableCollection<AssignmentViewModel> Assignments { get; set; }


        public CourseDetailsViewModel(int courseId)
        {
            Id = courseId;
            Course = CourseList.FirstOrDefault(x => x.Id == Id);

            Roster = new ObservableCollection<StudentViewModel>();
            Admin = new ObservableCollection<AdminViewModel>();
            Announcements = new ObservableCollection<AnnouncementViewModel>();
            Modules = new ObservableCollection<ModuleViewModel>();
            Assignments = new ObservableCollection<AssignmentViewModel>();

            foreach (var person in Course.Roster)
            {
                AdminViewModel admin = new AdminViewModel(person.Id);
                if (admin.Person == null)
                {
                    Roster.Add(new StudentViewModel(person.Id));
                }
                else
                {
                    Admin.Add(admin);
                }
            }
            foreach (var announcement in Course.Announcements)
            {
                Announcements.Add(new AnnouncementViewModel(announcement.Id));
            }
            foreach (var module in Course.Modules)
            {
                Modules.Add(new ModuleViewModel(module.Id));
            }
            foreach (var assignment in Course.Assignments)
            {
                Assignments.Add(new AssignmentViewModel(assignment.Id));
            }
        }

        public async void UpdateRoster()
        {
            var dialog = new RosterDialog(Course.Id);
            if (dialog != null)
            {
                await dialog.ShowAsync();
            }
            Refresh();
        }
        //public async void AddAssignment()
        //{
        //    bool cont = true;
        //    var dialog = new NewAssignmentDialog();
        //    if (dialog != null)
        //    {
        //        await dialog.ShowAsync();
        //    }
        //    if (!dialog.TestValid())
        //    {
        //        var errorDialog = new ErrorDialog();
        //        if (errorDialog != null)
        //        {
        //            await errorDialog.ShowAsync();
        //        }
        //    }
        //    else
        //    {
        //        var assignment = (moduleService.CurrentItem as AssignmentItem).Assignment;
        //        if (assignment.AssignmentGroup == null)
        //        {
        //            var Groupdialog = new AssignGroupDialog(assignment);
        //            if (Groupdialog != null)
        //            {
        //                await Groupdialog.ShowAsync();
        //            }
        //            if (!Groupdialog.TestValid())
        //            {
        //                var errorDialog = new ErrorDialog();
        //                if (errorDialog != null)
        //                {
        //                    await errorDialog.ShowAsync();
        //                }
        //                cont = false;
        //            }
        //        }
        //
        //        if (cont)
        //        {
        //            if (SelectedModule.Name.Equals("Make new Module"))
        //            {
        //                //SelectedModule = new Module();
        //                //var Moduledialog = new ModuleDialog();
        //                //if (Moduledialog != null)
        //                //{
        //                //    await Moduledialog.ShowAsync();
        //                //}
        //                //cont = Moduledialog.Test();
        //                //if (cont)
        //                //{
        //                //    moduleService.Add();
        //                //}
        //            }
        //        }
        //        if (!cont)
        //        {
        //            Course.Remove(assignment);
        //            var errorDialog = new ErrorDialog();
        //            if (errorDialog != null)
        //            {
        //                await errorDialog.ShowAsync();
        //            }
        //        }
        //    }
        //    Refresh();
        //    moduleService.CurrentItem = null;
        //}
        //public async void GetError()
        //{
        //    var errorDialog = new ErrorDialog();
        //    if (errorDialog != null)
        //    {
        //        await errorDialog.ShowAsync();
        //    }
        //}

        public async Task RemoveModule()
        {
            if (SelectedModule != null)
            {
                Course.Remove(SelectedModule.Module);
                await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateModules", Course);
                await new WebRequestHandler().Post("http://localhost:5159/Module/Delete", SelectedModule.Module);
                Refresh();
            }
        }
        public async Task RemoveAnnouncement()
        {
            if (SelectedAnnouncement != null)
            {
                Course.Remove(SelectedAnnouncement.Announcement);
                await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateAnnouncements", Course);
                await new WebRequestHandler().Post("http://localhost:5159/Announcement/Delete", SelectedAnnouncement.Announcement);
                Refresh();
            }
        }
        public async Task RemoveAssignment()
        {
            if (SelectedAssignment != null)
            {
                Course.Remove(SelectedAssignment.Assignment);
                await new WebRequestHandler().Post("http://localhost:5159/Course/UpdateAssignments", Course);
                await new WebRequestHandler().Post("http://localhost:5159/Assignment/Delete", SelectedAssignment.Assignment);
                Refresh();
            }
        }

        public async void ViewAnnouncement()
        {
            var messageDialog = new MessageDialog(SelectedAnnouncement.Announcement.Message, SelectedAnnouncement.Announcement.Title);
            messageDialog.Commands.Add(new UICommand("OK"));
            if (messageDialog != null)
            {
                await messageDialog.ShowAsync();
            }
        }

        public void Refresh()
        {
            Course = CourseList.FirstOrDefault(x => x.Id == Id);
            Roster.Clear();
            Admin.Clear();
            Modules.Clear();
            Assignments.Clear();
            Announcements.Clear();
            foreach (var person in Course.Roster)
            {
                AdminViewModel admin = new AdminViewModel(person.Id);
                if (admin.Person == null)
                {
                    StudentViewModel student = new StudentViewModel(person.Id);
                    Roster.Add(student);

                }
                else
                {
                    Admin.Add(admin);
                }
            }
            foreach (var module in Course.Modules)
            {
                Modules.Add(new ModuleViewModel(module.Id));
            }
            foreach (var assignment in Course.Assignments)
            {
                Assignments.Add(new AssignmentViewModel(assignment.Id));
            }
            foreach (var announcements in Course.Announcements)
            {
                Announcements.Add(new AnnouncementViewModel(announcements.Id));
            }
        }
    }
}
