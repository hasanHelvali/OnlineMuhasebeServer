using MediatR;
using OnlineMuhasebeServer.Application.Services.AppServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Application.Features.AppFeatıures.CompanyFeatures.Commands.MigrateCompanyDatabases
{
    public sealed class MigrateCompanyDatabasesRequestHandler : IRequestHandler<MigrateCompanyDatabasesRequest, MigrateCompanyDatabasesResponse>
    {
        private readonly ICompanyService companyService;

        public MigrateCompanyDatabasesRequestHandler(ICompanyService companyService)
        {
            this.companyService = companyService;
        }

        public async Task<MigrateCompanyDatabasesResponse> Handle(MigrateCompanyDatabasesRequest request, CancellationToken cancellationToken)
        {
            await companyService.MigrateCompanyDatabases();
            return new();
        }
    }
}
