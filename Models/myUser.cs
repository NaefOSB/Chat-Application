using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.Models
{
    public class myUser:IdentityUser
    {

        public string Address { get; set; }
        public List<Room> Rooms { get; set; }
        public List<Chat> Chats { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }


        public List<UserNotification> UserNotifications { get; set; }
    }
}
