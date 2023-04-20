using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnouncementController
    {
        private readonly ILogger<AnnouncementController> _logger;

        public AnnouncementController(ILogger<AnnouncementController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<Announcement> Get()
        {
            return new List<Announcement>();
        }

        [HttpPost("AddOrUpdate")]
        public Announcement AddorUpdate([FromBody] Announcement announcement)
        {
            return announcement;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] Announcement announcement)
        {
        }
    }
}
