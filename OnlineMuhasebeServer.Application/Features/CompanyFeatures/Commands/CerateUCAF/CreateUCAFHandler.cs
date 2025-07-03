using MediatR;
using OnlineMuhasebeServer.Application.Services.CompanyServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Application.Features.CompanyFeatures.Commands.CerateUCAF
{
    public sealed class CreateUCAFHandler : IRequestHandler<CreateUCAFRequest, CreateUCAFResponse>
    {
        private readonly  IUCAFService _uCAFService;
public CreateUCAFHandler(IUCAFService uCAFService)
        {
            this._uCAFService = uCAFService;
        }

        public async Task<CreateUCAFResponse> Handle(CreateUCAFRequest request, CancellationToken cancellationToken)
        {
            await _uCAFService.CreateUCAFAsync(request);
            return new();
        }
    }
}
