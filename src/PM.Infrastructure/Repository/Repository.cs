using AutoMapper;
using PM.Common.CommonModels;
using PM.Domain.BaseClasses;
using PM.Domain.Interfaces.Repository;
using PM.Infrastructure.EF.Context;
using PM.Infrastructure.EF.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PM.Infrastructure.Repository
{
    public abstract class Repository<TDomainEntity> : IRepository<TDomainEntity> where TDomainEntity : BaseEntity
    {
        protected readonly PMContext Context;
        private readonly IMapper mapper;

        public Repository(PMContext context, IMapper mapper)
        {
            Context = context;
            this.mapper = mapper;
        }

        public Result<int> Add(TDomainEntity entity)
        {
            var dbEntity = mapper.Map<TEntity>(entity);
            Context.Set<TEntity>().Add(dbEntity);
            return Result<int>.GetSuccessInstance(entity.ID);
        }

        public Result<IEnumerable<int>> AddRange(IEnumerable<TDomainEntity> entities)
        {
            var dbEntities = mapper.Map<IEnumerable<TEntity>>(entities);
            Context.Set<TEntity>().AddRange(dbEntities);
            return Result<IEnumerable<int>>
                .GetSuccessInstance(dbEntities.Select(e => e.ID));
        }

        public TDomainEntity Get(int ID)
        {
            var res = Context.Set<TEntity>().Find(ID);
            var entity = res.IsDeleted ? null : res;
            return mapper.Map<TDomainEntity>(entity);
        }

        public ICollection<TDomainEntity> GetAll()
        {
            var entities = Context
                .Set<TEntity>()
                .Where(t => !t.IsDeleted);

            return mapper.Map<ICollection<TDomainEntity>>(entities);
        }

        public void Remove(int entityID)
        {
            var entity = Context
                .Set<TEntity>()
                .Find(entityID);

            if (entity == null)
                return;

            entity.IsDeleted = true;
            entity.DeleteDate = DateTime.Now;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

        public abstract Result Update(int id, TDomainEntity entity);
    }
}
