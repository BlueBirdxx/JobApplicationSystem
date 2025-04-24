using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class JobApplication
    {
        [Key, ForeignKey("Candidate")]
        public int CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; } = null!;

        [Key, ForeignKey("JobPost")]
        public int JobPostId { get; set; }
        public virtual JobPost JobPost { get; set; } = null!;

        public string ResumeUrl { get; set; } = null!;
        public DateTime AppliedAt { get; set; }
    }
}
