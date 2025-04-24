using Data.DataAccessLayer;
using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public interface IJobApplicationRepository
    {
        int Application(JobApplication entity);
        List<JobApplication> ListApplicationForAJob(int id);
        bool CheckCandidateApplied(int candidateId, int jobPostId);
    }

    public class JobApplicationRepository : IJobApplicationRepository
    {
        private readonly AppDbContext _dbContext;

        public JobApplicationRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool CheckCandidateApplied(int candidateId, int jobPostId)
        {
            var data = _dbContext.JobApplications.FirstOrDefault(f => f.CandidateId == candidateId && f.JobPostId == jobPostId);

            if (data == null) return false;
            return true;
        }

        public int Application(JobApplication entity)
        {
            entity.AppliedAt = DateTime.UtcNow.AddHours(7);

            _dbContext.AddRange(entity);
            _dbContext.SaveChanges();

            return entity.JobPostId;
        }

        public List<JobApplication> ListApplicationForAJob(int id)
        {
            return _dbContext.JobApplications
                .Include(i => i.Candidate)
                .Where(f => f.JobPostId == id)
                .ToList();
        }
    }
}
