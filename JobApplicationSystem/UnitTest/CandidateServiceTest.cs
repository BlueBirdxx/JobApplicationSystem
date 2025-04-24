using AutoMapper;
using Data.AppException;
using Data.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.MapperProfiles;
using Service.Services;

namespace UnitTest
{
    public class CandidateServiceTest
    {
        private readonly CandidateService _candidateService;

        public CandidateServiceTest()
        {
            // Config mapper
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MapperProfile>(); });
            Mapper mapper = new Mapper(config);

            // Config in memory database
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            AppDbContext dbContext = new AppDbContext(options);

            // Create service
            CandidateRepository candidateRepository = new CandidateRepository(dbContext);
            _candidateService = new CandidateService(candidateRepository, mapper);
        }

        [Theory]
        [InlineData("usersuccess1@domain.com", "Candidate 1")]
        [InlineData("usersuccess2@domain.com", "Candidate 2")]
        public void Create_ValidInput_ReturnsSuccess(string email, string fullName)
        {
            var model = new Data.Models.CandidateCreateModel()
            {
                Email = email,
                FullName = fullName
            };

            var result = _candidateService.Create(model);

            Assert.True(result.Id > 0);
        }

        [Theory]
        [InlineData("userf1@domain.com", "Candidate 3")]
        [InlineData("userf2@domain.com", "Candidate 4")]
        public void Create_DuplicateEmail_ThrowsAppException(string email, string fullName)
        {
            _ = _candidateService.Create(new Data.Models.CandidateCreateModel() { Email = email, FullName = "Test Candidate"});

            var model = new Data.Models.CandidateCreateModel()
            {
                Email = email,
                FullName = fullName
            };

            Assert.Throws<AppException>(() => _candidateService.Create(model));
        }

    }
}