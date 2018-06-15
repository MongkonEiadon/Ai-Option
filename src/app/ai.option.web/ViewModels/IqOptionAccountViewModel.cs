using System;

namespace ai.option.web.ViewModels {
    public class IqOptionAccountViewModel {
        public string EmailAddress { get; set; }
        public string UserId { get; set; }
        public string Balance { get; set; }
        public string Currency { get; set; }
        public string CurrencyChar { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastSynced { get; set; }
    }
}