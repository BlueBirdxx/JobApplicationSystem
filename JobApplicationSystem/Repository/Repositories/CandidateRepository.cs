using Data.DataAccessLayer;
using Data.Entities;

namespace Repository.Repositories
{
    public interface ICandidateRepository
    {
        Candidate Add(Candidate candidate);
        bool CheckExistedId(int id);
        bool CheckExistedEmail(string email);
    }

    public class CandidateRepository : ICandidateRepository
    {
        private readonly AppDbContext _dbContext;

        public CandidateRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckExistedId(int id)
        {
            var data = _dbContext.Candidates.FirstOrDefault(f => f.Id == id);
            if (data == null) return false;
            return true;
        } 
        
        public bool CheckExistedEmail(string email)
        {
            var data = _dbContext.Candidates.FirstOrDefault(f => f.Email == email);
            if (data == null) return false;
            return true;
        }

        public Candidate Add(Candidate candidate)
        {
            _dbContext.Add(candidate);
            _dbContext.SaveChanges();

            return candidate;
        }
    }
}
