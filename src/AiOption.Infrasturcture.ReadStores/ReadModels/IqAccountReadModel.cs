using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AiOption.Domain.Common;
using AiOption.Domain.IqAccounts;

namespace AiOption.Infrasturcture.ReadStores.ReadModels
{
    [Table("IqAccountReadModel")]
    public class IqAccountReadModelDto : IqAccountReadModel
    {
        [Key] [Column("AccountId")] public override string AggregateId { get; set; }

        public User UserName { get; set; }

        public Password Password { get; set; }

        public string IqOptionToken { get; set; }

        public DateTimeOffset TokenUpdatedDate { get; set; }

        public virtual CustomerReadModelDto CustomerReadModel { get; set; }

    }
}