using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IJobPostService _jobPostService;

        public CompaniesController(ICompanyService companyService, IJobPostService jobPostService)
        {
            _companyService = companyService;
            _jobPostService = jobPostService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CompanyCreateModel model)
        {
            return Ok(_companyService.Add(model));
        }

        [HttpPost("{id}/jobs")]
        public IActionResult CreateJob(int id, [FromBody] JobPostCreateModel model)
        {
            return Ok(_jobPostService.Create(id, model));
        }
    }
}
