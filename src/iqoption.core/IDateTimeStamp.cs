using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using EventFlow.Aggregates.ExecutionResults;

namespace iqoption.core {
    public interface IDateTimeStamp {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        DateTime? CreatedOn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        DateTime? UpdatedOn { get; set; }
    }
    
}