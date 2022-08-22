using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.ViewModels
{
    public class VM_Rooms
    {
        public int roomId { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public string lastMSG { get; set; }
        public DateTime lastDate { get; set; }
    }
}
