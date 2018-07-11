using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using iqoption.core;
using iqoption.core.data;
using iqoption.data.IqOptionAccount;
using Microsoft.AspNetCore.Identity;

namespace iqoption.data.User {
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

        public virtual DateTime? LastLoginTime { get; set; }

        public virtual DateTime? RegistrationDate { get; set; }

        public virtual ICollection<IqOptionAccountDto> IqOptionAccounts { get; set; }
        
    }
}