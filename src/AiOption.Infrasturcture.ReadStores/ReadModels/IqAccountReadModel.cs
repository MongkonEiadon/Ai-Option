using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts;

namespace AiOption.Domain.IqAccounts
{
    [Table("IqAccounts")]
    public partial class IqAccountReadModel
    {
        [Key] [Column("AccountId")] public new string AggregateId { get; set; }
    }
}