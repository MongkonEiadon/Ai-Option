using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AiOption.Domain.Common;
using AiOption.Domain.IqOptions;
using EventFlow.ReadStores;

namespace AiOption.Infrastructure.ReadStores.ReadModels
{
    [Table("IqAccountReadModel")]
    public class IqAccountReadModel : IReadModel
    {
        [Key]
        [Column("Id")]
        public string AggregateId { get; set; }

        public User UserName { get; set; }

        public Password Password { get; set; }

        public CustomerReadModel CustomerReadModel { get; set; }


        public IqAccount ToIqAccount()
        {
            return new IqAccount(
                IqId.With(AggregateId), 
                UserName, 
                Password);
        }
    }
}