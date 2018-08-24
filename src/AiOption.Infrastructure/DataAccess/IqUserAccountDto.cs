using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AiOption.Domain;
using AiOption.Domain.Customers;

namespace AiOption.Infrastructure.DataAccess {

    [Table("IqUserAccount")]
    public class IqUserAccountDto : IActivable {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public string SecuredToken { get; set; }
        public DateTimeOffset SecuredUpdated { get; set; }

        public bool IsActive { get; set; }

        public TradeMode TradeMode { get; set; } = TradeMode.Follower;

        public CustomerLevel CustomerLevel { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

    }

    public enum TradeMode {

        Follower = 1,
        Traders = 2
    }

}