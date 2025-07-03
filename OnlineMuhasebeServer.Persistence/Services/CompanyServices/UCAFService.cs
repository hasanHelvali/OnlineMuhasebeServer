using AutoMapper;
using OnlineMuhasebeServer.Application.Features.CompanyFeatures.Commands.CerateUCAF;
using OnlineMuhasebeServer.Application.Services.CompanyServices;
using OnlineMuhasebeServer.Domain;
using OnlineMuhasebeServer.Domain.CompanyEntities;
using OnlineMuhasebeServer.Domain.Repositories.UCAFRepositories;
using OnlineMuhasebeServer.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Persistence.Services.CompanyServices
{
    public sealed class UCAFService : IUCAFService
    {
        private readonly    IUCAFCommandRepository _commandRepository;
        private readonly IContextService _contextService;
        private  CompanyDbContext _companyDbContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UCAFService(IUCAFCommandRepository commandRepository, IContextService contextService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _commandRepository = commandRepository;
            _contextService = contextService;
            _unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task CreateUCAFAsync(CreateUCAFRequest request)
        {
            _companyDbContext = (CompanyDbContext)_contextService.CreateDbContextInstance(request.CompanyID);//context uretildi

            _commandRepository.SetDbContextInstance(_companyDbContext);//command repo da set edildi.
            _unitOfWork.SetDbContextInstance(_companyDbContext);//uof de set edildi. Boytlece aynı context uzerinden ilerliyoruz. 
           UniformChartOfAccount uniformChartOfAccount=  _mapper.Map<UniformChartOfAccount>(request);//mapping
            uniformChartOfAccount.Id = Guid.NewGuid().ToString();//id ataması
            await _commandRepository.AddAsync(uniformChartOfAccount);//ekle
            await _unitOfWork.SaveChangesAsync();//kaydet.
        }
    }
}
