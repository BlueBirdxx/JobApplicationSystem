using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities
{
    public class BaseEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow.AddHours(7);
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow.AddHours(7);
        public bool IsDeleted { get; set; } = false;
    }
}
