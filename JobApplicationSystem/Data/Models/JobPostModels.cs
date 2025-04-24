using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class JobPostViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PostedAt { get; set; }
        public CompanyViewModel Company { get; set; } = null!;
    }

    public class JobPostCreateModel
    {
        [StringLength(maximumLength: 256, MinimumLength = 1)]
        public string Title { get; set; } = null!;

        [StringLength(maximumLength: 256, MinimumLength = 1)]
        public string Description { get; set; } = null!;
    }
}
