using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContentItemController
    {
        private readonly ILogger<ContentItemController> _logger;

        public ContentItemController(ILogger<ContentItemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<ContentItem> Get()
        {
            return new List<ContentItem>();
        }

        [HttpPost("AddOrUpdate")]
        public ContentItem AddorUpdate([FromBody] ContentItem contentItem)
        {
            return contentItem;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] ContentItem contentItem)
        {
        }
    }
}
