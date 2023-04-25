using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ModuleController
    {
        private readonly ILogger<ModuleController> _logger;

        public ModuleController(ILogger<ModuleController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Module> Get()
        {
            return new ModuleEC().GetModules();
        }

        [HttpPost("AddOrUpdate")]
        public Module AddorUpdate([FromBody] Module module)
        {
            return new ModuleEC().AddOrUpdateModule(module);
        }

        [HttpPost("Delete")]
        public void DeleteModule([FromBody] Module module)
        {
            new ModuleEC().Delete(module);
        }
    }
}
