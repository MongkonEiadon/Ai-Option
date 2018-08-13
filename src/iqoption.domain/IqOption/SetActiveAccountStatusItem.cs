namespace iqoption.domain.IqOption {
    public class SetActiveAccountStatusItem {
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        public SetActiveAccountStatusItem(bool isActive, int userId) {
            IsActive = isActive;
            UserId = userId;
        }
    }
}