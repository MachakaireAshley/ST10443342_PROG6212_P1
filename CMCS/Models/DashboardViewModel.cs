namespace CMCS.Models
{
    public class DashboardViewModel
    {
        public int PendingClaims { get; set; }
        public int RejectedClaims { get; set; }
        public int AcceptedClaims { get; set; }
        public List<Claim> RecentClaims { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Message> Messages { get; set; }
    }
}
