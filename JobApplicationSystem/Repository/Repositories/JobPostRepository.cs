using Data.DataAccessLayer;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public interface IJobPostRepository
    {
        JobPost? GetById(int id);
        JobPost Create(JobPost entity);
        bool CheckExisted(int id);
        List<JobPost> Search(string? keyword, string? companyName, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize);
    }

    public class JobPostRepository : IJobPostRepository
    {
        private readonly AppDbContext _dbContext;

        public JobPostRepository(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public bool CheckExisted(int id)
        {
            var data = _dbContext.JobPosts.FirstOrDefault(f => f.Id == id);
            if (data == null) return false;
            return true;
        }

        public JobPost? GetById(int id)
        {
            return _dbContext.JobPosts.Include(i => i.Company).FirstOrDefault(f => f.Id == id);
        }

        public JobPost Create(JobPost entity)
        {
            entity.PostedAt = DateTime.UtcNow.AddHours(7);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return entity;
        }

        public List<JobPost> Search(string? keyword, string? companyName, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize)
        {
            var data = _dbContext.JobPosts.Include(i => i.Company)
                .Where(f => string.IsNullOrWhiteSpace(keyword) || f.Title.Contains(keyword))
                .Where(f => string.IsNullOrWhiteSpace(companyName) || f.Company.Name.Contains(companyName))
                .Where(f => dateFrom == null || dateFrom <= f.PostedAt)
                .Where(f => dateTo == null || dateTo >= f.PostedAt)
                .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList()
                .ToList();

            return data;
        }
    }
}
