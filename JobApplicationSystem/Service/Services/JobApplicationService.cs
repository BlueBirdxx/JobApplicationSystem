using AutoMapper;
using Data.AppException;
using Data.Entities;
using Data.Models;
using Repository.Repositories;

namespace Service.Services
{
    public interface IJobApplicationService
    {
        int Create(JobApplicationCreateModel model);
        JobApplicationViewModel ListApplicationForAJob(int id);
    }

    public class JobApplicationService : IJobApplicationService
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IJobPostRepository _jobPostRepository;
        private readonly IJobApplicationRepository _jobApplicationRepository;
        private readonly IMapper _mapper;

        public JobApplicationService(ICandidateRepository candidateRepository, IJobPostRepository jobPostRepository, IJobApplicationRepository jobApplicationRepository, IMapper mapper)
        {
            _candidateRepository = candidateRepository;
            _jobPostRepository = jobPostRepository;
            _jobApplicationRepository = jobApplicationRepository;
            _mapper = mapper;
        }

        public int Create(JobApplicationCreateModel model)
        {
            if (_candidateRepository.CheckExistedId(model.CandidateId) == false) throw new AppException("Candidate does not exsit.");
            if (_jobPostRepository.CheckExisted(model.JobPostId) == false) throw new AppException("Job does not exsit.");

            if (_jobApplicationRepository.CheckCandidateApplied(model.CandidateId, model.JobPostId)) throw new AppException("Candidate already applied for this job.");

            var entity = _mapper.Map<JobApplication>(model);
            return _jobApplicationRepository.Application(entity);
        }

        public JobApplicationViewModel ListApplicationForAJob(int id)
        {
            // Get job info
            var jobInfo = _jobPostRepository.GetById(id) ?? throw new AppException("Job does not exsit.");

            // Get candidate application data
            var applicationData = _jobApplicationRepository.ListApplicationForAJob(id);

            // Map result
            var candidateData = _mapper.Map<List<CandidateApplicationModel>>(applicationData);
            var result = _mapper.Map<JobApplicationViewModel>(jobInfo);

            result.Candidates = candidateData;

            return result;
        }
    }
}
