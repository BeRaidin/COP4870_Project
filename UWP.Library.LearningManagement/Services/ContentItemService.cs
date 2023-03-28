using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class ContentItemService
    {
        private List<ContentItem> itemList;
        public List<ContentItem> ContentList
        {
            get
            {
                return itemList;
            }
            set
            {
                itemList = value;
            }
        }
        private ContentItem _currentContent;
        public ContentItem CurrentContent
        {
            get { return _currentContent; }
            set { _currentContent = value; }
        }
        public static ContentItemService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ContentItemService();
                }

                return instance;
            }
        }
        private static ContentItemService instance;

        public ContentItemService()
        {
            ContentList = new List<ContentItem>();
        }

        public void Add(ContentItem item)
        {
            ContentList.Add(item);
        }

        public void Remove()
        {
            ContentList.Remove(CurrentContent);
        }
    }
}
