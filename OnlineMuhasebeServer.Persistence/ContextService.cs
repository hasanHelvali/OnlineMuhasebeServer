using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Domain;
using OnlineMuhasebeServer.Domain.AppEntities;
using OnlineMuhasebeServer.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Persistence
{
    public sealed class ContextService : IContextService
    {
        private readonly AppDbContext _AppDbContext;

        public ContextService(AppDbContext appDbContext)
        {
            _AppDbContext = appDbContext;
        }

        public DbContext CreateDbContextInstance(string companyId)
        {
            Company company = _AppDbContext.Set<Company>().Find(companyId);
            return new CompanyDbContext(company);
        }
    }
}
