using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace iqoption.core {
    public interface IDateTimeStamp {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        DateTime? CreatedOn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        DateTime? UpdatedOn { get; set; }
    }
}