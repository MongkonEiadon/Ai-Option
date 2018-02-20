using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;

namespace iqoption.core
{
    
    public class Class1
    {
    }
}

namespace iqoption.core.data
{
    public interface IDateTimeStamp
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        DateTime? CreatedOn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        DateTime? UpdatedOn { get; set; }
    }

}
