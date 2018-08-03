using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iqoption.data.Model {
    [Table("TraderFollwers")]
    public class TraderFollwerDto : EntityWithDateTimeStamp {
        [Required]
        public TraderDto Trader { get; set; }

        [Required]
        public FollowerDto Follower { get; set; }
    }
}