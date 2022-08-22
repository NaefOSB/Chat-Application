using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.Models
{
    //public enum MessageType
    //{
    //    text,
    //    image,
    //    video,
    //    audio
    //}
    public class Chat
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }


        public int RoomId { get; set; }
        public Room Room { get; set; }


        public string myUserId { get; set; }
        public myUser myUser { get; set; }

    }
}
