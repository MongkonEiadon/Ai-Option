using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iqoption.data.IqOptionAccount;

namespace iqoption.data.Model {
    [Table("Follower")]
    public class FollowerDto : EntityWithDateTimeStamp {
        [Required]
        public IqOptionAccountDto IqOptionAccountDto { get; set; }
    }
}