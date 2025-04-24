using AutoMapper;
using Data.Entities;
using Data.Models;
using Repository.Repositories;

namespace Service.Services
{
    public interface ICompanyService
    {
        CompanyViewModel Add(CompanyCreateModel model);
    }

    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository companyRepository, IMapper mapper)
        {
            _companyRepository = companyRepository;
            _mapper = mapper;
        }

        public CompanyViewModel Add(CompanyCreateModel model)
        {
            var data = _mapper.Map<Company>(model);

            var rs = _companyRepository.Add(data);
            return _mapper.Map<CompanyViewModel>(rs);
        }
    }
}
