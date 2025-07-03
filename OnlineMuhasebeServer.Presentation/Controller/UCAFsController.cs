using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineMuhasebeServer.Application.Features.CompanyFeatures.Commands.CerateUCAF;
using OnlineMuhasebeServer.Presentation.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Presentation.Controller
{
    public sealed class UCAFsController : ApiController
    {
        public UCAFsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CreateUCAF(CreateUCAFRequest createUCAFRequest)
        {
            CreateUCAFResponse createUCAFResponse =  await _mediator.Send(createUCAFRequest);
            return Ok(createUCAFResponse);
        }
    }
}
