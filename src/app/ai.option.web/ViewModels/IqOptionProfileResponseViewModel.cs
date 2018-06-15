using System;

namespace ai.option.web.ViewModels {
    public class IqOptionProfileResponseViewModel {
        public string FirstName { get; set; }
        public string LastName { get; set; }


        public string Avartar { get; set; }
        public string Address { get; set; }

        public long BalanceId { get; set; }

        public long Balance { get; set; }
        public long UserId { get; set; }
        public string Currency { get; set; }
        public string CurrencyChar { get; set; }

        public string City { get; set; }

        public DateTime Birthdate { get; set; }
    }
}