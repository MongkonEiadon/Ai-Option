using System;
using iqoption.domain.Common;
using iqoption.domain.Users;

namespace iqoption.domain.Orders
{
    public class Order : BaseEntity {

        public long UserId { get; set; }

        public OrderDirection Direction { get; set; }

        public long Amount { get; set; }

        public long ActiveId { get; set; }
        
        public DateTime ExpirationDateTime { get; set; }

        public DateTime OpenPositionDateTime { get; set; }
    }
}
