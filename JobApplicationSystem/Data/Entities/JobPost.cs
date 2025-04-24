using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    public class JobPost : BaseEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime PostedAt { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public virtual Company Company { get; set; } = null!;
    }
}
