using Microsoft.AspNetCore.Mvc;
using UWP.Library.LearningManagement.Models;

namespace LearningManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GradesDictionaryController
    {
        private readonly ILogger<GradesDictionaryController> _logger;

        public GradesDictionaryController(ILogger<GradesDictionaryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public List<GradesDictionary> Get()
        {
            return new List<GradesDictionary>();
        }

        [HttpPost("AddOrUpdate")]
        public GradesDictionary AddorUpdate([FromBody] GradesDictionary gradesDictionary)
        {
            return gradesDictionary;
        }

        [HttpPost("Delete")]
        public void Delete([FromBody] GradesDictionary gradesDictionary)
        {
        }
    }
}
