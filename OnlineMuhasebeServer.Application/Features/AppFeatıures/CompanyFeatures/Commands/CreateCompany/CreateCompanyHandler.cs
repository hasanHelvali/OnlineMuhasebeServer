using MediatR;
using OnlineMuhasebeServer.Application.Services.AppServices;
using OnlineMuhasebeServer.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Application.Features.AppFeatıures.CompanyFeatures.Commands.CreateCompany
{
    public sealed class CreateCompanyHandler : IRequestHandler<CreateCompanyRequest, CreateCompanyResponse>
    {
        private readonly ICompanyService _companyService;

        public CreateCompanyHandler(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        public async  Task<CreateCompanyResponse> Handle(CreateCompanyRequest request, CancellationToken cancellationToken)
        {
            Company? company = await _companyService.GetCompanyByName(request.Name);
            if (company != null) throw new Exception("Bu şirket adı daha öneden kullanılmış. ");
            await _companyService.CreateCompany(request);
            return new();
        }
    }
}
