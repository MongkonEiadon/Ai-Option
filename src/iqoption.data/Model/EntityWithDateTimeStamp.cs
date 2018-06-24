using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using iqoption.core;
using iqoption.core.data;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace iqoption.data.Model {

    public abstract class EntityWithDateTimeStamp<T> : Entity<T>, IDateTimeStamp, IActiveable, IEntity<T> {

        
        [DefaultValue("getdate()")]
        public DateTime? CreatedOn { get; set; } = DateTime.Now;
        
        public DateTime? UpdatedOn { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public abstract class EntityWithDateTimeStamp : EntityWithDateTimeStamp<Guid>, IDateTimeStamp, IActiveable, IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DefaultValue("newid()")]
        public override Guid Id { get; set; }
    }
}