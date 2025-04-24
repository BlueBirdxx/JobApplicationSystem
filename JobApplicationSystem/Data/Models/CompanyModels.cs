using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class CompanyViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class CompanyCreateModel
    {
        [StringLength(maximumLength: 256, MinimumLength = 1)]
        public string Name { get; set; } = null!;
    }
}
