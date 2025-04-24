using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class JobApplicationCreateModel
    {
        public int CandidateId { get; set; }
        public int JobPostId { get; set; }

        [StringLength(maximumLength: 2048, MinimumLength = 1)]
        public string ResumeUrl { get; set; } = null!;
    }

    public class JobApplicationViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PostedAt { get; set; }
        public CompanyViewModel Company { get; set; } = null!;

        public List<CandidateApplicationModel> Candidates { get; set; } = new List<CandidateApplicationModel>();
    }
}
