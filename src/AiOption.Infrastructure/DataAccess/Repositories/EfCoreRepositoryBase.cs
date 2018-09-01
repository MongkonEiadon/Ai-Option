using System;
using System.Collections.Generic;
using System.Linq;

using AiOption.Application.Repositories;

using Microsoft.EntityFrameworkCore;

namespace AiOption.Infrastructure.DataAccess.Repositories {

    public class EfCoreRepositoryBase<TEntity, TPrimaryKey> : GenericRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IDbEntity<TPrimaryKey> {

        private readonly Func<DbContext> _dbDbContext;

        public EfCoreRepositoryBase(Func<DbContext> db) {
            _dbDbContext = db;
        }

        private DbSet<TEntity> Table => _dbDbContext().Set<TEntity>();

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
            }
        }

        protected virtual void AttachIfNot(TEntity entity) {
            var entry = _dbDbContext().ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);

            if (entry != null) return;

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