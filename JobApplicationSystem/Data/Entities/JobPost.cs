
namespace Data.Entities
{
    public class JobPost : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PostedAt { get; set; }
        public int CompanyId { get; set; }
        public Company Company { get; set; } = null!;
    }
}
