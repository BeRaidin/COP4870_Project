using Library.LearningManagement.Services;
using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UWP.LearningManagement.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        public Person SelectedPerson { get; set; }
        public ObservableCollection<Person> People { get; set; }

        public PersonViewModel()
        {
            People = new ObservableCollection<Person>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Remove()
        {
            People.Remove(SelectedPerson);
        }
    }
}
