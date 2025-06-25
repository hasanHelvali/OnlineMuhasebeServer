using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineMuhasebeServer.Application.Features.AppFeatıures.CompanyFeatures.Commands.CreateCompany;
using OnlineMuhasebeServer.Application.Features.AppFeatıures.CompanyFeatures.Commands.MigrateCompanyDatabases;
using OnlineMuhasebeServer.Presentation.Abstraction;

namespace OnlineMuhasebeServer.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ApiController//Api controller tarafımca yapıldı.
    {
        public CompaniesController(IMediator mediator) : base(mediator)//IMediator nensesini direkt olarak container dan aldık. Ve base e yolladık. 
        {
            //Bu yapı ile sureklı MediatR inject i yapmıyor oluruz.
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateCompany(CreateCompanyRequest createCompanyRequest)
        {
            CreateCompanyResponse createCompanyResponse = await _mediator.Send(createCompanyRequest);//Bu mediator ApiController dan gelir.
            return Ok(createCompanyResponse);
        }

        [HttpGet("[ation]")]
        //public async Task<IActionResult> MigrateCompanyDatabases(MigrateCompanyDatabasesRequest migrateCompanyDatabasesRequest)
        public async Task<IActionResult> MigrateCompanyDatabases( )
        {
            MigrateCompanyDatabasesRequest migrateCompanyDatabasesRequest = new();//Bos gelecegi icin buradan talep ettim.
            MigrateCompanyDatabasesResponse migrateCompanyDatabasesResponse = await _mediator.Send(migrateCompanyDatabasesRequest);
            return Ok(migrateCompanyDatabasesResponse);
        }
    }
}
