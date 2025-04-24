
namespace Data.Entities
{
    public  class Company : BaseEntity
    {
        public string Name { get; set; } = null!;
        public ICollection<JobPost> JobPosts { get; set; } = null!;
    }
}
