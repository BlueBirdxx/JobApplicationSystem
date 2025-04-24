using AutoMapper;
using Data.AppException;
using Data.Entities;
using Data.Models;
using Repository.Repositories;

namespace Service.Services
{
    public interface IJobPostService
    {
        JobPostViewModel Create(int companyId, JobPostCreateModel model);
        List<JobPostViewModel> Search(string? keyword, string? companyName, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize);
    }

    public class JobPostService : IJobPostService
    {
        private readonly IJobPostRepository _jobPostRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public JobPostService(IJobPostRepository jobPostRepository, ICompanyRepository companyRepository, IMapper mapper)
        {
            _jobPostRepository = jobPostRepository;
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public JobPostViewModel Create(int companyId, JobPostCreateModel model)
        {
            var isCompanyExsit = _companyRepository.CheckExisted(companyId);
            if (!isCompanyExsit) throw new AppException("Company does not exsit.");

            var data = _mapper.Map<JobPost>(model);

            data.CompanyId = companyId;

            var rs = _jobPostRepository.Create(data);
            return _mapper.Map<JobPostViewModel>(rs);
        }

        public List<JobPostViewModel> Search(string? keyword, string? companyName, DateTime? dateFrom, DateTime? dateTo, int pageIndex, int pageSize)
        {
            var data = _jobPostRepository.Search(keyword, companyName, dateFrom, dateTo, pageIndex, pageSize);
            return _mapper.Map<List<JobPostViewModel>>(data);
        }
    }
}
