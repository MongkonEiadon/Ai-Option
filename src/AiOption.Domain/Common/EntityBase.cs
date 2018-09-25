using System;

namespace AiOption.Domain.Common {

    public interface IDbEntity<TKey> {

        TKey Id { get; set; }

    }


    public abstract class EntityBase<TKey> : IEntityTrackable, IDbEntity<TKey> {

        public TKey Id { get; set; }

        public DateTimeOffset CreatedDate { get; set; }

        public DateTimeOffset UpdatedDate { get; set; }

    }

}