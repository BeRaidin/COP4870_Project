using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class AnnouncementEC
    {
        public List<Announcement> GetAnnouncements()
        {
            return FakeDataBase.Announcements;
        }

        public Announcement AddorUpdate(Announcement a)
        {
            bool isNew = false;
            if (FakeDataBase.Announcements.Count == 0)
            {
                a.Id = 0;
                isNew = true;
            }
            else if (a.Id < 0)
            {
                var lastId = FakeDataBase.Announcements.Select(x => x.Id).Max();
                lastId++;
                a.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.Announcements.Add(a);
                FakeDataBase.CurrentSemester[0].Announcements.Add(a);
            }
            else
            {
                var editedAnnouncement = FakeDataBase.Announcements.FirstOrDefault(x => x.Id == a.Id);
                if (editedAnnouncement != null)
                {
                    editedAnnouncement.Title = a.Title;
                    editedAnnouncement.Message = a.Message;

                    var semesterEditedAnnouncement = FakeDataBase.CurrentSemester[0].Announcements.FirstOrDefault(x => x.Id == a.Id);
                    if(semesterEditedAnnouncement != null)
                    {
                        semesterEditedAnnouncement.Title = a.Title;
                        semesterEditedAnnouncement.Message = a.Message;
                    }
                    return editedAnnouncement;
                }
            }
            return a;
        }
        public void Delete(Announcement a)
        {

            var deletedAnnouncement = FakeDataBase.Announcements.FirstOrDefault(x => x.Id == a.Id);
            if (deletedAnnouncement != null)
            {
                FakeDataBase.Announcements.Remove(deletedAnnouncement);
                var semesterDeletedAnnouncement = FakeDataBase.CurrentSemester[0].Announcements.FirstOrDefault(x => x.Id == a.Id);
                if (semesterDeletedAnnouncement != null)
                {
                    FakeDataBase.CurrentSemester[0].Announcements.Remove(semesterDeletedAnnouncement);
                }
            }
        }
    }
}
