using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AiOption.Domain.Customer;

namespace AiOption.Infrastructure.DataAccess {

    [Table("IqOptionAccounts")]
    public class IqAccountDto {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public string SecuredToken { get; set; }
        public DateTimeOffset SecuredUpdated { get; set; }
        public bool IsActive { get; set; }
        public CustomerLevel CustomerLevel { get; set; }

    }

}