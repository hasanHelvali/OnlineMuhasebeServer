using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using OnlineMuhasebeServer.Domain.Abstractions;
using OnlineMuhasebeServer.Domain.AppEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Persistence.Context
{
    public sealed class CompanyDbContext : DbContext
    {
        private string connectionString = "";
        //private readonly AppDbContext? _appDbContext;
        public CompanyDbContext(Company? company)
        {
            //_appDbContext = appDbContext;
            //company = _appDbContext.Companies.Find(companyId);
            if (company != null)
            {
                if (company.UserId == "")
                    connectionString = $"Data Source={company.ServerName};Initial Catalog={company.DatabaseName};Integrated Security=True;Connect Timeout=30;Encrypt=True;" +
                    "Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
                else
                    connectionString = $"Data Source={company.ServerName};Initial Catalog={company.DatabaseName}; User Id={company.UserId}; Password={company.Password};" +
                        $"Integrated Security=True;Connect Timeout=30;Encrypt=True;" +
                    "Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyReference).Assembly);
        }
        public class CompanyDbFatory : IDesignTimeDbContextFactory<CompanyDbContext>
        {
            public CompanyDbContext CreateDbContext(string[] args)
            {
                //PM> add-migration company_database_olusturma -Context CompanyDbContext komutuna karsılık hata verdigi icin bu sekilde bir yapı kurulamsı gerekiyor.
                return new CompanyDbContext( null);
            }
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Entity>();
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property(p => p.CreatedDate).CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.Now;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
