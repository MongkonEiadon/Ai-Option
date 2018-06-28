using System;

namespace iqoption.core {
    public abstract class DomainEvent : INotification {
        protected DomainEvent() {
            OccuredOn = DateTime.UtcNow;
        }

        public DateTime OccuredOn { get; }
    }
}