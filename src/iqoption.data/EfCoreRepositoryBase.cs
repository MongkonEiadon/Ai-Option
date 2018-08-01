using System;
using System.Collections.Generic;
using System.Linq;
using iqoption.core.data;
using Microsoft.EntityFrameworkCore;

namespace iqoption.data {
    public class EfCoreRepositoryBase<TEntity> : EfCoreRepositoryBase<TEntity, Guid>, IRepository<TEntity>
        where TEntity : class, IEntity {
        public EfCoreRepositoryBase(Func<DbContext> dbDbContext) : base(dbDbContext) {
        }
    }

    public class EfCoreRepositoryBase<TEntity, TPrimaryKey> : RepositoryBase<TEntity, TPrimaryKey>,
        IRepositoryWithDbContext
        where TEntity : class, IEntity<TPrimaryKey> {
        private readonly Func<DbContext> _dbDbContext;

        public EfCoreRepositoryBase(Func<DbContext> dbDbContext) {
            _dbDbContext = dbDbContext;
        }

        public virtual DbSet<TEntity> Table => _dbDbContext().Set<TEntity>();

        public DbContext GetDbContext() {
            return _dbDbContext();
        }

        public override IQueryable<TEntity> GetAll() {
            return Table.AsQueryable();
        }

        public override TEntity Insert(TEntity entity) {
            var newEntity = Table.Add(entity).Entity;
            _dbDbContext().SaveChanges();
            return newEntity;
        }

        public override TEntity Update(TEntity entity) {
            AttachIfNot(entity);
            _dbDbContext().Entry(entity).State = EntityState.Modified;

            _dbDbContext().SaveChanges();

            return entity;
        }

        public override void Delete(TEntity entity) {
            AttachIfNot(entity);
            Table.Remove(entity);

            _dbDbContext().SaveChanges();
        }

        public override void Delete(TPrimaryKey id) {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null) {
                Delete(entity);
                return;
            }

            entity = FirstOrDefault(id);
            if (entity != null) {
                Delete(entity);
                return;
            }
        }

        protected virtual void AttachIfNot(TEntity entity) {
            var entry = _dbDbContext().ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null) {
                return;
            }

            Table.Attach(entity);
        }

        private TEntity GetFromChangeTrackerOrNull(TPrimaryKey id) {
            var entry = _dbDbContext().ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<TPrimaryKey>.Default.Equals(id, ((TEntity) ent.Entity).Id)
                );

            return entry?.Entity as TEntity;
        }
    }
}