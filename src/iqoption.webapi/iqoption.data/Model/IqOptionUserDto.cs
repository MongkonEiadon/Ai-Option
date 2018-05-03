using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iqoption.data.Model {
    [Table("IqOptionUser")]
    public class IqOptionUserDto : EntityWithDateTimeStamp
    {
        public UserDto User { get; set; }


        public int IqOptionUserId { get; set; }

        [Required]
        [EmailAddress]
        public string IqOptionUserName { get; set; }

        [Required]
        public string Password { get; set; }

    }
}