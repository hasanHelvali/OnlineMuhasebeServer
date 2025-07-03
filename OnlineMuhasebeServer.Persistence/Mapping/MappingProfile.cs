using AutoMapper;
using OnlineMuhasebeServer.Application.Features.AppFeatıures.CompanyFeatures.Commands.CreateCompany;
using OnlineMuhasebeServer.Application.Features.CompanyFeatures.Commands.CerateUCAF;
using OnlineMuhasebeServer.Domain.AppEntities;
using OnlineMuhasebeServer.Domain.CompanyEntities;

namespace OnlineMuhasebeServer.Persistence.Mapping
{
    public  class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateCompanyRequest, Company>().ReverseMap();
            CreateMap<CreateUCAFRequest,UniformChartOfAccount>().ReverseMap();
        }
    }
}
