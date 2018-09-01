using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using AiOption.Infrastructure.DataAccess;

namespace AiOption.Application.Repositories {

    public interface IWriteOnlyRepository<TEntity, TPrimaryKey> where TEntity : class, IDbEntity<TPrimaryKey> {

        TEntity Insert(TEntity entity);

        Task<TEntity> InsertAsync(TEntity entity);

        TPrimaryKey InsertAndGetId(TEntity entity);

        Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity);

        TEntity Update(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        void Delete(TEntity entity);

        Task DeleteAsync(TEntity entity);

        void Delete(TPrimaryKey id);

        Task DeleteAsync(TPrimaryKey id);

        void Delete(Expression<Func<TEntity, bool>> predicate);

        Task DeleteAsync(Expression<Func<TEntity, bool>> predicate);

    }

}