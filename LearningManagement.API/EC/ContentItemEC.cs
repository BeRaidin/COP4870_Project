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
                var lastId = FakeDataBase.ContentItems.Select(p => p.Id).Max();
                lastId++;
                aI.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.ContentItems.Add(aI);
            }
            else
            {
                if (FakeDataBase.ContentItems.FirstOrDefault(p => p.Id == aI.Id) is AssignmentItem editedAssignmentItem)
                {
                    editedAssignmentItem.Name = aI.Name;
                    editedAssignmentItem.Description = aI.Description;
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
                var lastId = FakeDataBase.ContentItems.Select(p => p.Id).Max();
                lastId++;
                fI.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.ContentItems.Add(fI);
            }
            else
            {
                if (FakeDataBase.ContentItems.FirstOrDefault(p => p.Id == fI.Id) is FileItem editedFileItem)
                {
                    editedFileItem.Name = fI.Name;
                    editedFileItem.Description = fI.Description;
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
                var lastId = FakeDataBase.ContentItems.Select(p => p.Id).Max();
                lastId++;
                pI.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.ContentItems.Add(pI);
            }
            else
            {
                if (FakeDataBase.ContentItems.FirstOrDefault(p => p.Id == pI.Id) is PageItem editedPageItem)
                {
                    editedPageItem.Name = pI.Name;
                    editedPageItem.Description = pI.Description;
                    return editedPageItem;
                }
            }
            return pI;
        }



        public ContentItem Delete(ContentItem i)
        {

            var deletedItem = FakeDataBase.ContentItems.FirstOrDefault(d => d.Id == i.Id);
            if (deletedItem != null)
            {
                FakeDataBase.ContentItems.Remove(deletedItem);
            }
            return i;
        }
    }
}
