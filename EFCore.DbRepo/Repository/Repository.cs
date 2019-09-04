using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreDbRepo.EntityBase;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDbRepo.Repository
{
    public abstract class Repository<TDomain, TEntity> : IRepository<TDomain> where TEntity : class, IAggregateRoot
    {
        protected IMapper Mapper { get; private set; }
        protected DbSet<TEntity> DbSet { get; private set; }

        private DbContext context;
        private IEntityDeepUpdater<TDomain, TEntity> updater;
        private bool disposedValue = false; // To detect redundant calls

        public IQueryable<TDomain> DomainSet
        {
            get { return DbSet.ProjectTo<TDomain>(Mapper.ConfigurationProvider); }
            private set { }
        }

        public abstract IQueryable<TDomain> EagerLoadedDomainSet { get; }

        public Repository(DbContext db, IMapper mapper, IEntityDeepUpdater<TDomain, TEntity> updater)
        {

            this.context = db;
            this.Mapper = mapper;
            this.updater = updater;
            DbSet = context.Set<TEntity>();
        }

        public abstract Task<TDomain> GetByIdEager(object id);
        public abstract Task Insert(TDomain domain_object);
        public abstract void Update(TDomain domain_object);

        public virtual async Task<TDomain> GetById(object id)
        {
            return MapEntityToDomain(await DbSet.FindAsync(id));
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                context.Attach(entityToDelete);
            }
            context.Remove(entityToDelete);
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected TDomain MapEntityToDomain(TEntity entity_object)
        {
            return Mapper.Map<TEntity, TDomain>(entity_object);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                disposedValue = true;
            }
        }

        private void ClearAllEntities()
        {
            foreach (var entity in context.ChangeTracker.Entries().Where(e => e.State == EntityState.Added ||
            e.State == EntityState.Modified || e.State == EntityState.Deleted || e.State == EntityState.Unchanged))
            {
                context.Entry(entity.Entity).State = EntityState.Detached;
            }
        }

        private List<TDomain> MapEntities(List<TEntity> entities)
        {
            var results = new List<TDomain>();
            entities.ForEach(r => results.Add(MapEntityToDomain(r)));
            return results;
        }

        private TEntity MapDomainToEntity(TDomain domain_object)
        {
            var entity = Mapper.Map<TDomain, TEntity>(domain_object);

            return entity;
        }

        private void UpdateEntity(TDomain source, TEntity dest)
        {
            updater.Update(source, dest, Mapper);
        }

        protected virtual async Task DefaultInsert(TDomain domain_object)
        {
            var entity = Mapper.Map(domain_object, domain_object.GetType(), typeof(TEntity));
            await context.AddAsync(entity);
            Mapper.Map(entity, domain_object, entity.GetType(), typeof(TDomain));
        }

        protected virtual void DefaultUpdate(TDomain domain_object)
        {
            var tracked_entity = DbSet.Find(((IAggregateRoot)(Mapper.Map<TEntity>(domain_object))).Id);
            if (tracked_entity != null)
            {
                UpdateEntity(domain_object, tracked_entity);
                context.Update(tracked_entity);
                Mapper.Map(tracked_entity, domain_object, tracked_entity.GetType(), typeof(TDomain));
            }
            else
            {
                throw new InvalidOperationException("This entity does not exist in the context use Insert");
            }
        }
    }
}