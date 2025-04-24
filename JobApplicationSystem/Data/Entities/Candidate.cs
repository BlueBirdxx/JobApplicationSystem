
namespace Data.Entities
{
    public class Candidate : BaseEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public ICollection<JobApplication> Applications { get; set; } = null!;
    }
}
