using Data.DataAccessLayer;
using Data.Entities;

namespace Repository.Repositories
{
    public interface ICompanyRepository
    {
        Company Add(Company candidate);
        bool CheckExisted(int id);
    }

    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _dbContext;

        public CompanyRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckExisted(int id)
        {
            var data = _dbContext.Companies.FirstOrDefault(f => f.Id == id);
            if (data == null) return false;
            return true;
        }

        public Company Add(Company candidate)
        {
            _dbContext.Add(candidate);
            _dbContext.SaveChanges();

            return candidate;
        }
    }
}
