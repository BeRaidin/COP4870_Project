using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FileItemController
    {
        private readonly ILogger<FileItemController> _logger;

        public FileItemController(ILogger<FileItemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<FileItem> Get()
        {
            return new List<FileItem>();
        }

        [HttpPost("AddOrUpdate")]
        public FileItem AddorUpdate([FromBody] FileItem fileItem)
        {
            return fileItem;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] FileItem fileItem)
        {
        }
    }
}
