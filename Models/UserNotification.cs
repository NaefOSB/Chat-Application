using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.Models
{
    public class UserNotification
    {
        public int Id { get; set; }



        public string myUserId { get; set; }
        public myUser myUser { get; set; }


        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public bool IsReaded { get; internal set; }
    }
}
