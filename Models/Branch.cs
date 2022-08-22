using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.Models
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<myUser> myUsers { get; set; }
    }
}
