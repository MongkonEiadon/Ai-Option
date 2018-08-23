using System;
using System.Collections.Generic;
using System.Text;

namespace AiOption.Domain.Accounts.Events
{
    public class StatusChangeEventItem
    {
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        public DateTimeOffset ChangedDateTime { get; set; }

        public StatusChangeEventItem(bool isActive, int userId)
        {
            IsActive = isActive;
            UserId = userId;
            ChangedDateTime = DateTimeOffset.Now;
        }
    }
}
