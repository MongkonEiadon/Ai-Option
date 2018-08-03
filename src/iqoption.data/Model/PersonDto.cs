using System.ComponentModel.DataAnnotations.Schema;

namespace iqoption.data.Model {
    [Table("Person")]
    public class PersonDto : EntityWithDateTimeStamp<string> {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}