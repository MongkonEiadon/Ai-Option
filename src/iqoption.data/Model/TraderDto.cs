using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iqoption.data.IqOptionAccount;

namespace iqoption.data.Model {
    [Table("Trader")]
    public class TraderDto : EntityWithDateTimeStamp {
        [Required]
        public IqOptionAccountDto IqOptionAccountDto { get; set; }
    }
}