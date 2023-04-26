using LearningManagement.API.EC;
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

        [HttpGet("GetItems")]
        public List<ContentItem> GetItems()
        {
            return new ContentItemEC().GetItems();
        }

        [HttpGet("GetAssignmentItems")]
        public List<AssignmentItem> GetAssignmentItems()
        {
            return new ContentItemEC().GetAssignmentItems();
        }

        [HttpGet("GetFileItems")]
        public List<FileItem> GetFileItems()
        {
            return new ContentItemEC().GetFileItems();
        }

        [HttpGet("GetPageItems")]
        public List<PageItem> GetPageItems()
        {
            return new ContentItemEC().GetPageItems();
        }

        [HttpPost("AddOrUpdateAssignmentItem")]
        public AssignmentItem AddOrUpdateAssignmentItem([FromBody] AssignmentItem assignmentItem)
        {
            return new ContentItemEC().AddOrUpdateAssignmentItem(assignmentItem);
        }

        [HttpPost("AddOrUpdateFileItem")]
        public FileItem AddOrUpdateFileItem([FromBody] FileItem fileItem)
        {
            return new ContentItemEC().AddOrUpdateFileItem(fileItem);
        }

        [HttpPost("AddOrUpdatePageItem")]
        public PageItem AddOrUpdatePageItem([FromBody] PageItem pageItem)
        {
            return new ContentItemEC().AddOrUpdatePageItem(pageItem);
        }

        [HttpPost("Delete")]
        public void DeleteItem([FromBody] ContentItem contentItem)
        {
            new ContentItemEC().Delete(contentItem);
        }
    }
}
