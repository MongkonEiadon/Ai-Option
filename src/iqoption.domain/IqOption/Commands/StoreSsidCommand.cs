using iqoption.core;

namespace iqoption.domain.IqOption.Command {
    public class StoreSsidCommand  {
        public string Ssid { get; }
        public string EmailAddress { get; set; }

        public StoreSsidCommand(string email, string ssid) {
            EmailAddress = email;
            Ssid = ssid;
        }
    }

    public class StoreSsidResult {

    }
}