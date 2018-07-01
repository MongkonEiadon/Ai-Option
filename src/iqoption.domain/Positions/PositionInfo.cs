using System;

namespace iqoption.domain.Positions {
    public class PositionInfo {
        public long Amount { get; set; }

        public long Id { get; set; }

        public long Refund { get; set; }

        public string Currency { get; set; }

        public string CurrencyChar { get; set; }

        public ActivePair ActiveId { get; set; }

        public string Active { get; set; }

        public double Value { get; set; }

        public double ExpValue { get; set; }

        public Direction Direction { get; set; }

        public DateTime Created { get; set; }

        public DateTime Expired { get; set; }

        public long ExpTime { get; set; }

        public string TypeName { get; set; }

        public string Type { get; set; }

        public long Profit { get; set; }

        public long ProfitAmount { get; set; }

        public double WinAmount { get; set; }

        public long LooseAmount { get; set; }

        public long Sum { get; set; }

        public string Win { get; set; }

        public long Now { get; set; }

        public long UserId { get; set; }

        public long GameState { get; set; }

        public long ProfitIncome { get; set; }

        public long ProfitReturn { get; set; }

        public long OptionTypeId { get; set; }

        public long SiteId { get; set; }

        public bool IsDemo { get; set; }

        public long UserBalanceId { get; set; }

        public long ClientPlatformId { get; set; }

        public string ReTrack { get; set; }
    }
}