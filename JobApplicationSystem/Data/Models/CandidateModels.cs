using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CandidateViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

    public class CandidateCreateModel
    {
        [StringLength(maximumLength: 256, MinimumLength = 1)]
        public string FullName { get; set; } = null!;

        [EmailAddress]
        public string Email { get; set; } = null!;
    }

    public class CandidateApplicationModel
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ResumeUrl { get; set; } = null!;
    }
}
