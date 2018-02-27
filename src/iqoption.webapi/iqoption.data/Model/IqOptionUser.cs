using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using iqoption.core.data;

namespace iqoption.data.Model
{
    [Table("IqOptionUser")]
    public class IqOptionUser : Entity, IDateTimeStamp
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string IqOptionUserName { get; set; }

        [Required]
        public string Password { get; set; }

        public bool IsEnable { get; set; } = true;

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? CreatedOn { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? UpdatedOn { get; set; }
    }
}