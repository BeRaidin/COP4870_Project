using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.EC
{
    public class ContentItemEC
    {
        public List<ContentItem> GetItems()
        {
            return FakeDataBase.ContentItems;
        }
        public List<AssignmentItem> GetAssignmentItems()
        {
            return FakeDataBase.AssignmentItems;
        }

        public List<FileItem> GetFileItems()
        {
            return FakeDataBase.FileItems;
        }

        public List<PageItem> GetPageItems()
        {
            return FakeDataBase.PageItems;
        }

        public AssignmentItem AddOrUpdateAssignmentItem(AssignmentItem aI)
        {
            bool isNew = false;
            if (FakeDataBase.ContentItems.Count == 0)
            {
                aI.Id = 0;
                isNew = true;
            }
            else if (aI.Id < 0)
            {
                var lastId = FakeDataBase.ContentItems.Select(x => x.Id).Max();
                lastId++;
                aI.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.ContentItems.Add(aI);
                FakeDataBase.CurrentSemester[0].ContentItems.Add(aI);
                UpdateSemester();
            }
            else
            {
                if (FakeDataBase.ContentItems.FirstOrDefault(x => x.Id == aI.Id) is AssignmentItem editedAssignmentItem)
                {
                    editedAssignmentItem.Assignment = aI.Assignment;
                    editedAssignmentItem.Name = aI.Name;
                    editedAssignmentItem.Description = aI.Description;

                    if (FakeDataBase.CurrentSemester[0].ContentItems.FirstOrDefault(x => x.Id == aI.Id) is AssignmentItem semesterEditedAssignmentItem)
                    {
                        semesterEditedAssignmentItem.Assignment = aI.Assignment;
                        semesterEditedAssignmentItem.Name = aI.Name;
                        semesterEditedAssignmentItem.Description = aI.Description;
                        UpdateSemester();
                    }
                    return editedAssignmentItem;
                }
            }
            return aI;
        }

        public FileItem AddOrUpdateFileItem(FileItem fI)
        {
            bool isNew = false;
            if (FakeDataBase.ContentItems.Count == 0)
            {
                fI.Id = 0;
                isNew = true;
            }
            else if (fI.Id < 0)
            {
                var lastId = FakeDataBase.ContentItems.Select(x => x.Id).Max();
                lastId++;
                fI.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.ContentItems.Add(fI);
                FakeDataBase.CurrentSemester[0].ContentItems.Add(fI);
                UpdateSemester();
            }
            else
            {
                if (FakeDataBase.ContentItems.FirstOrDefault(x => x.Id == fI.Id) is FileItem editedFileItem)
                {
                    editedFileItem.Name = fI.Name;
                    editedFileItem.Description = fI.Description;
                    if (FakeDataBase.CurrentSemester[0].ContentItems.FirstOrDefault(x => x.Id == fI.Id) is FileItem semesterEditedFileItem)
                    {
                        semesterEditedFileItem.Name = fI.Name;
                        semesterEditedFileItem.Description = fI.Description;
                    }
                    UpdateSemester();
                    return editedFileItem;
                }
            }
            return fI;
        }
        public PageItem AddOrUpdatePageItem(PageItem pI)
        {
            bool isNew = false;
            if (FakeDataBase.ContentItems.Count == 0)
            {
                pI.Id = 0;
                isNew = true;
            }
            else if (pI.Id < 0)
            {
                var lastId = FakeDataBase.ContentItems.Select(x => x.Id).Max();
                lastId++;
                pI.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.ContentItems.Add(pI);
                FakeDataBase.CurrentSemester[0].ContentItems.Add(pI);
                UpdateSemester();
            }
            else
            {
                if (FakeDataBase.ContentItems.FirstOrDefault(x => x.Id == pI.Id) is PageItem editedPageItem)
                {
                    editedPageItem.Name = pI.Name;
                    editedPageItem.Description = pI.Description;
                    if (FakeDataBase.CurrentSemester[0].ContentItems.FirstOrDefault(x => x.Id == pI.Id) is PageItem semesterEditedPageItem)
                    {
                        semesterEditedPageItem.Name = pI.Name;
                        semesterEditedPageItem.Description = pI.Description;
                    }
                    UpdateSemester();
                    return editedPageItem;
                }
            }
            return pI;
        }

        public void Delete(ContentItem i)
        {

            var deletedItem = FakeDataBase.ContentItems.FirstOrDefault(x => x.Id == i.Id);
            if (deletedItem != null)
            {
                FakeDataBase.ContentItems.Remove(deletedItem);
            }
            var semesterDeletedItem = FakeDataBase.CurrentSemester[0].ContentItems.FirstOrDefault(x => x.Id == i.Id);
            if (semesterDeletedItem != null)
            {
                FakeDataBase.CurrentSemester[0].ContentItems.Remove(semesterDeletedItem);
            }
            UpdateSemester();
        }

        public void UpdateSemester()
        {
            var semester = FakeDataBase.Semesters.FirstOrDefault(x => x.Id == FakeDataBase.CurrentSemester[0].Id);
            if (semester != null)
            {
                FakeDataBase.Semesters.Remove(semester);
                FakeDataBase.Semesters.Add(FakeDataBase.CurrentSemester[0]);
            }
        }
    }
}
