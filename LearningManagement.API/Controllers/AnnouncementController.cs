using LearningManagement.API.EC;
using Microsoft.AspNetCore.Mvc;
using System;
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
            return new AnnouncementEC().GetAnnouncements();
        }

        [HttpPost("AddOrUpdate")]
        public Announcement AddorUpdate([FromBody] Announcement announcement)
        {
            return new AnnouncementEC().AddorUpdate(announcement);
        }

        [HttpPost("Delete")]
        public void DeleteInstructor([FromBody] Announcement announcement)
        {
            new AnnouncementEC().Delete(announcement);
        }
    }
}
