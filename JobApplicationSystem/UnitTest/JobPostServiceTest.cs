using AutoMapper;
using Data.AppException;
using Data.DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Service.MapperProfiles;
using Service.Services;

namespace UnitTest
{
    public class JobPostServiceTest
    {
        private readonly JobPostService _jobPostService;
        private readonly CompanyRepository _companyRepository;

        public JobPostServiceTest()
        {
            // Config mapper
            var config = new MapperConfiguration(cfg => { cfg.AddProfile<MapperProfile>(); });
            Mapper mapper = new Mapper(config);

            // Config in memory database
            var options = new DbContextOptionsBuilder<AppDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            AppDbContext dbContext = new AppDbContext(options);

            // Create service
            JobPostRepository jobPostRepository = new(dbContext);
            _companyRepository = new(dbContext);

            _jobPostService = new JobPostService(jobPostRepository, _companyRepository, mapper);

            // Mock company data
            if (!_companyRepository.CheckExisted(1)) _companyRepository.Add(new Data.Entities.Company() { Name = "Company 1" });
            if (!_companyRepository.CheckExisted(2)) _companyRepository.Add(new Data.Entities.Company() { Name = "Company 2" });
        }

        [Theory]
        [InlineData(1, "Job 1", "")]
        [InlineData(2, "Job 2", "")]
        public void Create_ValidInput_ReturnsSuccess(int companyId, string title, string description)
        {

            var model = new Data.Models.JobPostCreateModel()
            {
                Title = title,
                Description = description
            };

            var result = _jobPostService.Create(companyId, model);

            Assert.True(result.Id > 0);
        }

        [Theory]
        [InlineData(0, "Job 0", "")]
        [InlineData(9999, "Job 9999", "")]
        public void Create_NonEistentCompany_ShouldThrowAppException(int companyId, string title, string description)
        {
            var model = new Data.Models.JobPostCreateModel()
            {
                Title = title,
                Description = description
            };

            Assert.Throws<AppException>(() => _jobPostService.Create(companyId, model));
        }

        public static IEnumerable<object[]> SearchTestData =>
            [
                ["", "Candidate 1", new DateTime(), new DateTime(), 1, 50],
                ["developer", "Candidate 2", new DateTime(2024, 1, 1), new DateTime(2024, 12, 31), 1, 10]
            ];

        [Theory]
        [MemberData(nameof(SearchTestData))]
        public void Search_NotMatchSearchCondition_ShouldReturnEmpty(string? keyword, string? companyName, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize)
        {
            var result = _jobPostService.Search(keyword, companyName, dateFrom, dateTo, pageIndex, pageSize);
            Assert.Empty(result);
        }

        public static IEnumerable<object[]> SearchTestDataMatchCondition =>
        [
            ["Job 1", "Company 1", new DateTime(2024, 1, 1), new DateTime(2025, 12, 31), 1, 50]
        ];

        [Theory]
        [MemberData(nameof(SearchTestDataMatchCondition))]
        public void Search_MatchSearchCondition_ShouldReturnData(string? keyword, string? companyName, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize)
        {
            var result = _jobPostService.Search(keyword, companyName, dateFrom, dateTo, pageIndex, pageSize);
            Assert.Single(result);
        }

        [Fact]
        public void Search_NullFilters_ShouldReturnAllResult()
        {
            var result = _jobPostService.Search(null, null, null, null, 1, 50);
            Assert.Equal(2, result.Count);
        }
    }
}
