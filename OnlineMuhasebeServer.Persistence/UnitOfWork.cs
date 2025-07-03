using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Domain;
using OnlineMuhasebeServer.Persistence.Context;

namespace OnlineMuhasebeServer.Persistence
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private CompanyDbContext _companyDbContext;
        public void SetDbContextInstance(DbContext context)
        {
            _companyDbContext = (CompanyDbContext)context;
        }

        public async Task<int> SaveChangesAsync()
        {
           int count =  await _companyDbContext.SaveChangesAsync();
            return count;
        }
    }
}
