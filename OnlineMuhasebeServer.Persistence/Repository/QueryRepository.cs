using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Domain.Abstractions;
using OnlineMuhasebeServer.Domain.Repositories;
using OnlineMuhasebeServer.Persistence.Context;
using System.Linq.Expressions;

namespace OnlineMuhasebeServer.Persistence.Repository
{
    public class QueryRepository<T> : IQueryRepository<T> where T : Entity
    {
        public DbSet<T> Entity { get ; set; }
        private CompanyDbContext _companyDbContext;

        private static readonly Func<CompanyDbContext, string, Task<T>> GetByIdCompiled = EF.CompileAsyncQuery((CompanyDbContext dbContext, string id) =>
  dbContext.Set<T>().FirstOrDefault(p => p.Id == id));

        private static readonly Func<CompanyDbContext, Task<T>> GetFirstCompiled = EF.CompileAsyncQuery((CompanyDbContext dbContext) =>
dbContext.Set<T>().FirstOrDefault());

        private static readonly Func<CompanyDbContext, Expression<Func<T, bool>> , Task<T>> GetFirstByExpressionCompiled = EF.CompileAsyncQuery((CompanyDbContext dbContext, 
            Expression<Func<T, bool>> expression) =>
dbContext.Set<T>().FirstOrDefault(expression));

        public void SetDbContextInstance(DbContext context)
        {
            _companyDbContext = (CompanyDbContext)context;
            Entity = _companyDbContext.Set<T>();
        }

        public async Task<T> GeFirst(string id)
        {
            return  await GetFirstCompiled(_companyDbContext);
        }

        public IQueryable<T> GetAll()
        {
           return Entity.AsQueryable<T>();
        }

        public async Task<T> GetById(string id)
        {
            return await GetByIdCompiled(_companyDbContext, id);
        }

        public async Task<T> GetFirstByExpression(Expression<Func<T, bool>> expression)
        {
            return await GetFirstByExpressionCompiled(_companyDbContext, expression);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression)
        {
            return Entity.Where(expression);
        }
    }
}
