﻿using OnlineMuhasebeServer.Domain.CompanyEntities;
using OnlineMuhasebeServer.Domain.Repositories.UCAFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Persistence.Repository.UCAFRepositories
{
    public sealed class UCAFQueryRepository:QueryRepository<UniformChartOfAccount>,IUCAFQueryRepository
    {
    }
}
