using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWP.Library.LearningManagement.Models
{
    public class ListNavigator<T>
    {
        private int pageSize;
        private int currentPage;
        private List<T> state;
        public List<T> State
        {
            get { return state; }
            set { state = value; }
        }


        private int lastPage
        {
            get
            {
                var val = state.Count / pageSize;
                if (state.Count % pageSize > 0)
                {
                    val++;
                }
                return val;
            }
        }
        public bool HasPreviousPage
        {
            get
            {
                return currentPage > 1;
            }
        }
        public bool HasNextPage
        {
            get
            {
                return currentPage < lastPage;
            }
        }
        public ListNavigator(List<T> list, int pageSize = 5)
        {
            this.pageSize = pageSize;
            currentPage = 1;
            state = list;
        }
        public Dictionary<int, T> GoForward()
        {
            if (currentPage + 1 > lastPage)
            {
                throw new PageFaultException("Cannot navigate to the right of the last page in the list!");
            }
            currentPage++;
            return GetWindow();
        }
        public Dictionary<int, T> GoBackward()
        {
            if (currentPage - 1 <= 0)
            {
                throw new PageFaultException("Cannot navigate to the left of the first page in the list!");
            }
            currentPage--;
            return GetWindow();
        }
        public Dictionary<int, T> GoToPage(int page)
        {
            if (page <= 0 || page > lastPage)
            {
                throw new PageFaultException("Cannot navigate to a page outside of the bounds of the list!");
            }
            currentPage = page;
            return GetWindow();
        }
        public Dictionary<int, T> GetCurrentPage()
        {
            return GoToPage(currentPage);
        }
        public Dictionary<int, T> GoToFirstPage()
        {
            currentPage = 1;
            return GetWindow();
        }
        public Dictionary<int, T> GoToLastPage()
        {
            currentPage = lastPage;
            return GetWindow();
        }
        private Dictionary<int, T> GetWindow()
        {
            var window = new Dictionary<int, T>();
            for (int i = (currentPage - 1) * pageSize; i < (currentPage - 1) * pageSize + pageSize && i < state.Count; i++)
            {
                window.Add(i + 1, state[i]);
            }
            return window;
        }

        public List<T> PrintPage(Dictionary<int, T> values)
        {
            var list = new List<T>();
            foreach (KeyValuePair<int, T> value in values)
            {
                list.Add(value.Value);
            }
            return list;
        }

        public void ChangePage()
        {
            var cont = true;

            Console.WriteLine("Use \"<\" and \">\" to navigate. ('Q' to Quit)");
            PrintPage(GoToFirstPage());
            while (cont)
            {
                var choice = Console.ReadLine() ?? string.Empty;
                if (choice.Equals("<", StringComparison.InvariantCultureIgnoreCase) && HasPreviousPage)
                {
                    PrintPage(GoBackward());
                }
                else if (choice.Equals("<", StringComparison.InvariantCultureIgnoreCase) && !HasPreviousPage)
                {
                    Console.WriteLine("Cannot move further that direction.");
                }
                else if (choice.Equals(">", StringComparison.InvariantCultureIgnoreCase) && HasNextPage)
                {
                    PrintPage(GoForward());
                }
                else if (choice.Equals(">", StringComparison.InvariantCultureIgnoreCase) && !HasNextPage)
                {
                    Console.WriteLine("Cannot move further that direction.");
                }
                else if (choice.Equals("Q", StringComparison.InvariantCultureIgnoreCase))
                {
                    cont = false;
                }
                else
                {
                    PrintPage(GetCurrentPage());
                }
            }
        }

        public void Add(T item)
        {
            state.Add(item);
        }

        public void Remove(T item)
        {
            state.Remove(item);
        }

        public void Clear()
        {
            state.Clear();
        }
    }
    public class PageFaultException : Exception
    {
        public PageFaultException(string message) : base(message)
        {
        }
    }
}