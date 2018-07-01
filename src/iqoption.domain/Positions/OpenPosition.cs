using System;

namespace iqoption.domain.Positions {
    public class OpenPosition {
        public long Price { get; set; }

        public ActivePair ActivePair { get; set; }

        public DateTime Expiration { get; set; }

        public string Type { get; set; } = "turbo";

        public Direction Direction { get; set; }

        public DateTime Time { get; set; }
    }
}