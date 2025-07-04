﻿using OnlineMuhasebeServer.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMuhasebeServer.Domain.Repositories
{
    public interface IQueryRepository<T>:IRepository<T> where T : Entity
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetWhere(Expression<Func<T,bool>> expression);
        Task<T> GetById(string id);
        Task<T> GetFirstByExpression(Expression<Func<T, bool>> expression);
        Task<T> GeFirst(string id);
    }
}
