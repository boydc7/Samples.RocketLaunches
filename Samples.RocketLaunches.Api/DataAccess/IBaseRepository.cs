using System;
using System.Collections.Generic;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.DataAccess
{
    public interface IBaseRepository<T, TIdType>
        where T : class, IHasId<TIdType>
    {
        T GetById(TIdType id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Query(Predicate<T> predicate);
        TIdType Add(T model);
        T Update(T model);
        void Delete(TIdType id);
    }

    public interface IBaseModelRepository<T> : IBaseRepository<T, int>
        where T : BaseModel { }
}
