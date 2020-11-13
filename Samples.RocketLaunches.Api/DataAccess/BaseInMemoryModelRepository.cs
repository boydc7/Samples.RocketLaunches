using System;
using Samples.RocketLaunches.Api.Models;

namespace Samples.RocketLaunches.Api.DataAccess
{
    public abstract class BaseInMemoryModelRepository<T> : BaseInMemoryRepository<T, int>, IBaseModelRepository<T>
        where T : BaseModel
    {
        public override int Add(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            return base.Add(model);
        }

        public override T Update(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (!_dbModels.TryGetValue(model.Id, out var existing))
            {
                throw new ApplicationException($"Record does not exist [{typeof(T).Name}].[{model.Id}]");
            }

            return base.Update(model);
        }
    }
}
