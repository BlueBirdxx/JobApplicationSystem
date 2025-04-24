using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace JobApplicationSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService _candidateService;

        public CandidatesController(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        [HttpPost]
        public IActionResult Create([FromBody]CandidateCreateModel model)
        {
            return Ok(_candidateService.Create(model));
        }
    }
}
