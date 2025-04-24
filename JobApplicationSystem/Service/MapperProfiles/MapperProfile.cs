using AutoMapper;
using Data.Entities;
using Data.Models;

namespace Service.MapperProfiles
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            // Candidates
            CreateMap<Candidate, CandidateCreateModel>().ReverseMap();
            CreateMap<Candidate, CandidateViewModel>().ReverseMap();

            // Companies
            CreateMap<Company, CompanyCreateModel>().ReverseMap();
            CreateMap<Company, CompanyViewModel>().ReverseMap();

            // Job post
            CreateMap<JobPost, JobPostCreateModel>().ReverseMap();
            CreateMap<JobPost, JobPostViewModel>().ReverseMap();
            CreateMap<JobPost, JobApplicationViewModel>()
                .ForMember(f => f.Candidates, map => map.Ignore())
                .ReverseMap();

            // Job application
            CreateMap<JobApplication, JobApplicationCreateModel>().ReverseMap();
            CreateMap<JobApplication, JobApplicationViewModel>().ReverseMap();

            CreateMap<JobApplication, CandidateApplicationModel>()
                .ForMember(f => f.Id, map => map.MapFrom(f => f.CandidateId))
                .ForMember(f => f.FullName, map => map.MapFrom(f => f.Candidate.FullName))
                .ForMember(f => f.Email, map => map.MapFrom(f => f.Candidate.Email))
                .ForMember(f => f.ResumeUrl, map => map.MapFrom(f => f.ResumeUrl))
                .ReverseMap();
        }
    }
}
