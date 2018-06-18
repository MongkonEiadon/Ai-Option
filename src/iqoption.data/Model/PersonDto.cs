using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using iqoption.core.data;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace iqoption.data.Model
{
    [Table("Person")]
    public class PersonDto : EntityWithDateTimeStamp<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
