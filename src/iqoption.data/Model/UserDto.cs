using System;
using System.ComponentModel.DataAnnotations.Schema;
using iqoption.core;
using iqoption.core.data;
using Microsoft.AspNetCore.Identity;

namespace iqoption.data.Model {
    [Table("User")]
    public class UserDto : IdentityUser, IDateTimeStamp, IEntity<string> {
        /// <summary>Given name(s) or first name(s) of the End-User.</summary>
        public virtual string GivenName { get; set; }

        /// <summary>Surname(s) or last name(s) of the End-User.</summary>
        public virtual string FamilyName { get; set; }

        public string InviationCode { get; set; }


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedOn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedOn { get; set; }
    }
}