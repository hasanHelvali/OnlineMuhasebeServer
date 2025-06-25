using OnlineMuhasebeServer.Application.Features.AppFeatıures.CompanyFeatures.Commands.CreateCompany;
using OnlineMuhasebeServer.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Application.Services.AppServices
{
    public interface ICompanyService
    {
        Task CreateCompany(CreateCompanyRequest createCompanyRequest);
        Task<Company?> GetCompanyByName(string name);

        Task MigrateCompanyDatabases();
    }
}
