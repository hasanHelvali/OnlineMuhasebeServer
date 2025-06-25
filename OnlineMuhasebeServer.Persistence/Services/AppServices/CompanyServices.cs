using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineMuhasebeServer.Application.Features.AppFeatıures.CompanyFeatures.Commands.CreateCompany;
using OnlineMuhasebeServer.Application.Services.AppServices;
using OnlineMuhasebeServer.Domain.AppEntities;
using OnlineMuhasebeServer.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Persistence.Services.AppServices
{
    public sealed class CompanyServices : ICompanyService
     {
        private static readonly Func<AppDbContext, string, Task<Company>> GetCompanyByNameCompiled = EF.CompileAsyncQuery((AppDbContext context, string name) =>
        context.Set<Company>().FirstOrDefault(x => x.Name == name));
        /*
         
         EF Core içindeki LINQ sorgunuzu (burada FirstOrDefault(x => x.Name == name)) alır, bir kez derler (compile), SQL’e çevirir ve bunu bir delegate’a (fonksiyon işaretçisine) gömer.
        Böylece her seferinde expression tree’yi baştan derleyip SQL’e çevirmeye gerek kalmaz; performans artışı sağlar.
        Derlenmiş sorgu tek bir kez oluşturulur ve bellekte tutulur (static readonly).
        Girdi olarak bir AppDbContext ve bir string name alır,
        Dönen değer olarak da Task<Company> (asenkron bir Company objesi) verir.
        
         */
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CompanyServices(AppDbContext appDbContext,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task CreateCompany(CreateCompanyRequest createCompanyRequest)
        {
            Company company = _mapper.Map<Company>(createCompanyRequest);
            company.Id = Guid.NewGuid().ToString();
            await _appDbContext.Set<Company>().AddAsync(company);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Company?> GetCompanyByName(string name)
        {
            //return await _appDbContext.Set<Company>().FirstOrDefaultAsync(x => x.Name == name); //Bu sekilde de yapılabilirdi lakin alttaki daha performanslı bir kod yapısııdr.
            return await GetCompanyByNameCompiled(_appDbContext, name); //Bu sekilde delegate li bir cozum de mevcuttur. Digerinden performanslı ama zordur. Denendi.

            /*_appDbContext ve aranan name parametresini delegate’a verirsiniz.
             await ile sorgu veritabanında çalışır, sonucu (eşleşen ilk şirket kaydı) döner.*/
        }

        public async Task MigrateCompanyDatabases()
        {
            var companies=await _appDbContext.Set<Company>().ToListAsync();
            foreach (var company in companies)
            {
                var db =new  CompanyDbContext(company);
                db.Database.Migrate();//Bu metot ile mgiration islemi yapılmıs olur.
                //Runtime da db olustururuz.
            }
        }
    }
}
