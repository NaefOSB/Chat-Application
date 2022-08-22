using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.ViewModels
{
    public class VM_Chat
    {
        public int ChatId { get; set; }
        public string Message { get; set; }
        public int Type { get; set; }
        public int RoomId { get; set; }
        public string UserId { get; set; }

    }
}
