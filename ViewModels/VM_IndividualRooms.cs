using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.ViewModels
{
    public class VM_IndividualRooms
    {
        public int RoomId { get; set; }
        public string PeerUserId { get; set; }
        public string PeerUserName { get; set; }
        public string BranchName { get; set; }
    }
}
