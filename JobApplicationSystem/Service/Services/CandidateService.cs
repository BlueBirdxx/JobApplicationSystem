using AutoMapper;
using Data.AppException;
using Data.Entities;
using Data.Models;
using Repository.Repositories;

namespace Service.Services
{
    public interface ICandidateService
    {
        CandidateViewModel Create(CandidateCreateModel model);
    }

    public class CandidateService : ICandidateService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IMapper _mapper;

        public CandidateService(ICandidateRepository candidateRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _mapper = mapper;
        }

        public CandidateViewModel Create(CandidateCreateModel model)
        {
            var checkExistedEmail = _candidateRepository.CheckExistedEmail(model.Email);
            if (checkExistedEmail) throw new AppException("Email already in used.");

            var entity = _mapper.Map<Candidate>(model);

            var rs = _candidateRepository.Add(entity);
            return _mapper.Map<CandidateViewModel>(rs);
        }
    }
}
