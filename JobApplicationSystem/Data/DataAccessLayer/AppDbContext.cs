using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #region Entities
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Double PK, FK
            modelBuilder.Entity<JobApplication>().HasKey(s => new { s.CandidateId, s.JobPostId });
            #endregion
        }
    }
}
