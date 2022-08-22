using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.Models
{
    public class IndividualChat
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }

        public int IndividualRoomId { get; set; }
        public IndividualRoom IndividualRoom { get; set; }


        public string myUserId { get; set; }
        public myUser myUser { get; set; }
    }
}
