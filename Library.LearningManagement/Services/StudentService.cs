using Library.LearningManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.LearningManagement.Services
{
    public class StudentService
    {
        private List<Person> studentList;
        private static StudentService? instance;
        private StudentService()
        {
            studentList = new List<Person>();
        }

        public static StudentService Current
        {
            get 
            {
                if (instance == null)
                {
                    instance = new StudentService();
                }

                return instance;
            }
        }
           
        public void Add(Person student)
        {
            studentList.Add(student);
        }

        public List<Person> Students
        {
            get
            {
                return studentList;
            }
        }

        public IEnumerable<Person> Search(string query)
        {
            return Students.Where(s => s.Name.ToUpper().Contains(query.ToUpper()));
        }
    }
}
