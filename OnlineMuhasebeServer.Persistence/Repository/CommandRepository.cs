using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Domain.Abstractions;
using OnlineMuhasebeServer.Domain.Repositories;
using OnlineMuhasebeServer.Persistence.Context;

namespace OnlineMuhasebeServer.Persistence.Repository
{
    public class CommandRepository<T> : ICommandRepository<T> where T : Entity
    {
        private CompanyDbContext _companyDbContext; 

        private static readonly Func<CompanyDbContext, string, Task<T>> GetByIdCompiled = EF.CompileAsyncQuery((CompanyDbContext dbContext, string id) =>
        dbContext.Set<T>().FirstOrDefault(p => p.Id == id));
        public DbSet<T> Entity { get; set; }


        public void SetDbContextInstance(DbContext context)//Context bruadan alınır
        {
            _companyDbContext = (CompanyDbContext)context;//instance i uretilir.
            Entity=_companyDbContext.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            await Entity.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await Entity.AddRangeAsync(entities);
        }

        public void Remove(T entity)
        {
            Entity.Remove(entity);
        }

        public async Task RemoveById(string id)
        {
            T Entity =  await GetByIdCompiled(_companyDbContext, id);
            Remove(Entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            Entity.RemoveRange(entities);
        }

        public void Update(T entity)
        {
            Entity.Update(entity);

        }

        public void UpdateRange(IEnumerable<T> entities)
        {
            Entity.UpdateRange(entities);

        }
    }
}
