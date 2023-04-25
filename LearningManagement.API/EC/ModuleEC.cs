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
                var lastId = FakeDataBase.Modules.Select(p => p.Id).Max();
                lastId++;
                m.Id = lastId;
                isNew = true;
            }

            if (isNew)
            {
                FakeDataBase.Modules.Add(m);
            }
            else
            {
                var editedModule = FakeDataBase.Modules.FirstOrDefault(i => i.Id == m.Id);
                if (editedModule != null)
                {
                    editedModule.Name = m.Name;
                    editedModule.Description = m.Description;
                    return editedModule;
                }
            }
            return m;
        }

        public void Delete(Module m)
        {

            var deletedModule = FakeDataBase.Modules.FirstOrDefault(d => d.Id == m.Id);
            if (deletedModule != null)
            {
                FakeDataBase.Modules.Remove(deletedModule);
            }
        }
    }
}
