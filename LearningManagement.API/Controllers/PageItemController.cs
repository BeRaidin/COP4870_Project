using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PageItemController
    {
        private readonly ILogger<PageItemController> _logger;

        public PageItemController(ILogger<PageItemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<PageItem> Get()
        {
            return new List<PageItem>();
        }

        [HttpPost("AddOrUpdate")]
        public PageItem AddorUpdate([FromBody] PageItem pageItem)
        {
            return pageItem;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] PageItem pageItem)
        {
        }
    }
}
