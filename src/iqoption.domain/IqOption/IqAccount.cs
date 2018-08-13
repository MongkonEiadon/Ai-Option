using System;
using EventFlow.Aggregates.ExecutionResults;

namespace iqoption.domain.IqOption {
    public class IqAccount : IExecutionResult {
        public int IqOptionUserId { get; set; }
        public string IqOptionUserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastSyned { get; set; }
        public long Balance { get; set; }
        public long BalanceId { get; set; }
        public string Currency { get; set; }
        public string CurrencyChar { get; set; }
        public DateTime BirthDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Avartar { get; set; }
        public string Ssid { get; set; }
        public DateTime? SsidUpdated { get; set; }
        public bool IsSuccess { get; set; }

        public string Level { get; set; }
    }
}