using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iqoption.data.Model;
using iqoption.data.User;

namespace iqoption.data.IqOptionAccount {
    [Table("IqOptionAccount")]
    public class IqOptionAccountDto : EntityWithDateTimeStamp {
        public int IqOptionUserId { get; set; }

        [Required]
        [EmailAddress]
        public string IqOptionUserName { get; set; }
        [Required]
        public string Password { get; set; }
        public DateTime? LastSyned { get; set; }
        public long Balance { get; set; }
        public long BalanceId { get; set; }
        public string Currency { get; set; }
        public string CurrencyChar { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Avartar { get; set; }
        public string Ssid { get; set; }
        public DateTime? SsidUpdated { get; set; }
        public virtual UserDto User { get; set; }
    }
}