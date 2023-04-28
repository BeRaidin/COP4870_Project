using UWP.Library.LearningManagement.Database;
using UWP.Library.LearningManagement.Models;
using Microsoft.AspNetCore.Mvc;


namespace LearningManagement.API.EC
{
    public class ModuleEC
    { 
        public List<Module> GetModules()
        {
            return FakeDataBase.Modules;
        }

        public Module AddOrUpdateModule(Module m)
        {
            bool isNew = false;
            if (FakeDataBase.Modules.Count == 0)
            {
                m.Id = 0;
                isNew = true;
            }
            else if (m.Id < 0)
            {
                var lastId = FakeDataBase.Modules.Select(x => x.Id).Max();
                lastId++;
                m.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.Modules.Add(m);
                FakeDataBase.CurrentSemester[0].Modules.Add(m);
            }
            else
            {
                var editedModule = FakeDataBase.Modules.FirstOrDefault(x => x.Id == m.Id);
                if (editedModule != null)
                {
                    editedModule.Name = m.Name;
                    editedModule.Description = m.Description;
                    var semesterEditedModule = FakeDataBase.CurrentSemester[0].Modules.FirstOrDefault(x => x.Id == m.Id);
                    if (semesterEditedModule != null)
                    {
                        semesterEditedModule.Name = m.Name;
                        semesterEditedModule.Description = m.Description;
                    }

                    return editedModule;
                }
            }
            return m;
        }

        public Module UpdateItems(Module m)
        {
            var editedModule = FakeDataBase.Modules.FirstOrDefault(x => x.Id == m.Id);
            if (editedModule != null)
            {
                editedModule.Content = m.Content;
                var semesterEditedModule = FakeDataBase.CurrentSemester[0].Modules.FirstOrDefault(x => x.Id == m.Id);
                if (semesterEditedModule != null)
                {
                    semesterEditedModule.Content = m.Content;
                }
                return editedModule;
            }
            return m;
        }

        public void Delete(Module m)
        {

            var deletedModule = FakeDataBase.Modules.FirstOrDefault(x => x.Id == m.Id);
            if (deletedModule != null)
            {
                FakeDataBase.Modules.Remove(deletedModule);
                var semesterDeletedModule = FakeDataBase.CurrentSemester[0].Modules.FirstOrDefault(x => x.Id == m.Id);
                if (semesterDeletedModule != null)
                {
                    FakeDataBase.CurrentSemester[0].Modules.Remove(semesterDeletedModule);
                }
            }
        }
    }
}
