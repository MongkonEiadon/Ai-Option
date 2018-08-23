using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iqoption.data.Model;

namespace iqoption.data.IqOptionAccount {
    [Table("TraderRules")]
    public class TraderRulesDto : EntityWithDateTimeStamp {
        
        [Required]
        public int IqAccountUserId { get; set; }

        [DefaultValue(1)]
        public int Multiplier { get; set; }
        
    }
}