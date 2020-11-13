using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.DataAccess
{
    public abstract class BaseInMemoryRepository<T, TIdType> : IBaseRepository<T, TIdType>
        where T : class, IHasId<TIdType>
    {
        protected readonly ConcurrentDictionary<TIdType, T> _dbModels = new ConcurrentDictionary<TIdType, T>();

        public IEnumerable<T> Query(Predicate<T> predicate)
            => _dbModels.Values.Where(m => predicate?.Invoke(m) ?? true);

        public T GetById(TIdType id)
            => _dbModels.TryGetValue(id, out var model)
                   ? model
                   : null;

        public IEnumerable<T> GetAll()
            => _dbModels.Values;

        public virtual TIdType Add(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!_dbModels.TryAdd(model.Id, model))
            {
                throw new ApplicationException($"Duplicate record [{typeof(T).Name}].[{model.Id}]");
            }

            return model.Id;
        }

        public virtual T Update(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!_dbModels.TryUpdate(model.Id, model, model))
            {
                throw new ApplicationException($"Invalid record state [{typeof(T).Name}].[{model.Id}]");
            }

            return model;
        }

        public void Delete(TIdType id)
            => _dbModels.TryRemove(id, out _);
    }
}
