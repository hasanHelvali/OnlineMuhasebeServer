using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using OnlineMuhasebeServer.Domain.Abstractions;
using OnlineMuhasebeServer.Domain.AppEntities;
using OnlineMuhasebeServer.Domain.AppEntities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Persistence.Context
{
    public sealed class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<UserAndCompanyRelation> userAndCompanyRelations { get; set; }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<Entity>();//Memory deki butun entity lere ulasıldı.
            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    //entry.Property(p => p.Id).CurrentValue = Guid.NewGuid().ToString();//Her nesnenin id sine bir guid eklendi.
                    //Id yi CompanyService de biz verdik. Buradan verinde hata aldık.

                    //Etity class ı diger class ların base inde oldugu icin automapper bunlara ulasamaz. O sebeple buradaki edgerleri bu sekilde dolduruyoruz.
                    entry.Property(p => p.CreatedDate).CurrentValue = DateTime.Now;//Her nesnenin id sine bir guid eklendi.
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(p => p.UpdatedDate).CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        public class AppDbContextFatory : IDesignTimeDbContextFactory<AppDbContext>
        {
            public AppDbContext CreateDbContext(string[] args)
            {
                //PM> add-migration company_database_olusturma -Context AppDbContext komutuna karsılık hata verdigi icin bu sekilde bir yapı kurulamsı gerekiyor.

                var optionsBuilder = new DbContextOptionsBuilder();
                var connectionString = "Data Source=HASANHELVALI;Initial Catalog=MuhasebeMasterDB;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
                optionsBuilder.UseSqlServer(connectionString);  
                return new AppDbContext(optionsBuilder.Options);
            }
        }
    }

}
