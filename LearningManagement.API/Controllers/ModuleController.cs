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
            return new List<Module>();
        }

        [HttpPost("AddOrUpdate")]
        public Module AddorUpdate([FromBody] Module module)
        {
            return module;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] Module module)
        {
        }
    }
}
