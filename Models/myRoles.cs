using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.Models
{
    public class myRoles:IdentityRole
    {
        public List<Room> Rooms { get; set; }
    }
}
