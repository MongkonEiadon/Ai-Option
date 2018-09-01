using System;

namespace AiOption.Domain.IqOptions.Events {

    public class StatusChangeEventItem {

        public StatusChangeEventItem(bool isActive, int userId) {
            IsActive = isActive;
            UserId = userId;
            ChangedDateTime = DateTimeOffset.Now;
        }

        public int UserId { get; set; }
        public bool IsActive { get; set; }

        public DateTimeOffset ChangedDateTime { get; set; }

    }

}