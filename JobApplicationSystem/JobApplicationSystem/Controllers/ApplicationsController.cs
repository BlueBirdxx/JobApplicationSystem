using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        private readonly IJobApplicationService _jobApplicationService;

        public ApplicationsController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [HttpPost]
        public IActionResult JobApplication(JobApplicationCreateModel model)
        {
            return Ok(_jobApplicationService.Create(model));
        }
    }
}
