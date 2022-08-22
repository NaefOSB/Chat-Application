using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string NotificationMessage { get; set; }

        public List<UserNotification> UserNotifications { get; set; }
    }

}
