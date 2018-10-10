using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts;

namespace AiOption.Infrasturcture.ReadStores.ReadModels
{
    [Table("IqAccounts")]
    public class IqAccountReadModelDto : IqAccountReadModel
    {
        [Key] [Column("AccountId")] public override string AggregateId { get; set; }
    }
}