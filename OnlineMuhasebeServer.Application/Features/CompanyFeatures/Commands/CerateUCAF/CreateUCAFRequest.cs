using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Application.Features.CompanyFeatures.Commands.CerateUCAF
{
    public sealed class CreateUCAFRequest:IRequest<CreateUCAFResponse>
    {
        public string CompanyID { get; set; }
        public string Name{ get; set; }
        public string Type{ get; set; }
        public string Code { get; set; }
    }
}
