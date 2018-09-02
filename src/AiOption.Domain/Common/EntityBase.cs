using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiOption.Domain.Common {

    public interface IDbEntity<TKey> {

        [Key]
        [Column(Order = 0)]
        TKey Id { get; set; }

    }


    public abstract class EntityBase<TKey> : IEntityTrackable, IDbEntity<TKey> {

        [Key]
        [Column(Order = 0)]
        public TKey Id { get; set; }

        [Column(Order = 100)]
        public DateTimeOffset CreatedDate { get; set; }
        
        [Column(Order = 101)]
        public DateTimeOffset UpdatedDate { get; set; }

    }

}