using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;
        private readonly IJobPostService _jobPostService;

        public JobsController(IJobApplicationService jobApplicationService, IJobPostService jobPostService)
        {
            _jobApplicationService = jobApplicationService;
            _jobPostService = jobPostService;
        }

        [HttpGet]
        public IActionResult Search([FromQuery] string? keyword, [FromQuery] string? companyName, [FromQuery] DateTime? dateFrom, [FromQuery] DateTime? dateTo, [FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 50)
        {
            return Ok(_jobPostService.Search(keyword, companyName, dateFrom, dateTo, pageIndex, pageSize));
        }

        [HttpGet("{id}/applications")]
        public IActionResult ListApplicationForAJob(int id)
        {
            return Ok(_jobApplicationService.ListApplicationForAJob(id));
        }
    }
}
