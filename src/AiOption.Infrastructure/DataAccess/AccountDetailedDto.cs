using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using AiOption.Domain.Common;

namespace AiOption.Infrastructure.DataAccess {


    [Table("IqAccountDetails")]
    public class AccountDetailedDto : EntityBase<int> {

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new int Id { get; set; }

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