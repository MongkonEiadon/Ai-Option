using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iqoption.core.Extensions;

namespace iqoption.data.Model {

    [Table("IqOptionUser")]
    public sealed class IqOptionUserDto : EntityWithDateTimeStamp
    {

        public int IqOptionUserId { get; set; }

        [Required]
        [EmailAddress]
        public string IqOptionUserName { get; set; }

        [Required]
        public string Password { get; set; }

        public UserDto User { get; set; }

        public DateTime? LastSyned { get; set; }
        

    }
}