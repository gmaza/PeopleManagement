using PM.Common.CommonModels;
using PM.Domain.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace PM.Domain.Interfaces.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : BaseEntity
    {
        TEntity Get(int ID);

        ICollection<TEntity> GetAll();

        Result<int> Add(TEntity entity);
        Result<IEnumerable<int>> AddRange(IEnumerable<TEntity> entities);

        Result Update(int id, TEntity entity);

        void Remove(int entityID);
    }
}
