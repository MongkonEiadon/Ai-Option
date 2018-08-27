using System;
using System.ComponentModel.DataAnnotations;

using AiOption.Domain.Accounts.Queries;

namespace AiOption.Infrastructure.DataAccess {

    public interface IDbEntity<TKey> {
        [Key]
        TKey Id { get; set; }

    }

    public abstract class EntityBase<TKey> : IEntityTrackable {

        [Key]
        public virtual TKey Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }



    }

}