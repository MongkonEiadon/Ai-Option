using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AiOption.Infrastructure.DataAccess {

    public class IqAccountDto {

        [Key]
        public int UserId { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

    }

}