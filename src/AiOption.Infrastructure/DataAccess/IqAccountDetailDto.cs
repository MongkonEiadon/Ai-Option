using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AiOption.Infrastructure.DataAccess {
    [Table("IqOptionAccountDetails")]
    public class IqAccountDetailDto {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public long Balance { get; set; }
        public long BalanceId { get; set; }
        public string Currency { get; set; }
        public string CurrencyChar { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Avartar { get; set; }
        public DateTimeOffset? LastSyned { get; set; }
    }

}