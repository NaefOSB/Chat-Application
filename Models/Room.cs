using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.Models
{
    public class Room
    {
        public int Id { get; set; }
        public bool IsIndividual { get; set; }
        public bool IsSeen { get; set; }
        public string myRolesId { get; set; }
        public myRoles myRoles { get; set; }


        public string myUserId { get; set; }
        public myUser myUser { get; set; }


        public List<Chat> Chats { get; set; }
    }
}
