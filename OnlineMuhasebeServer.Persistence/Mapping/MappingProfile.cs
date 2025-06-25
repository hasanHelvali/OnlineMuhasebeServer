using AutoMapper;
using OnlineMuhasebeServer.Application.Features.AppFeatıures.CompanyFeatures.Commands.CreateCompany;
using OnlineMuhasebeServer.Domain.AppEntities;

namespace OnlineMuhasebeServer.Persistence.Mapping
{
    public  class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCompanyRequest, Company>().ReverseMap();
        }
    }
}
