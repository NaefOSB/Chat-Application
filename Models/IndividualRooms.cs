using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.Models
{
    public class IndividualRoom
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string RecievedId { get; set; }

        public List<IndividualChat> IndividualChat { get; set; }


    }
}
