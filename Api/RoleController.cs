using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatApplication.Data;
using chatApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chatApplication.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        ApplicationDbContext db;
        private readonly RoleManager<myRoles> roleManager;
        private readonly UserManager<myUser> userManager;
        public RoleController(ApplicationDbContext context, RoleManager<myRoles> roleManager, UserManager<myUser> userManager)
        {
            db = context;
            this.roleManager = roleManager;
            this.userManager = userManager;
        }


        [HttpGet("GetAllRoles")]
        public async Task<List<myRoles>> GetAllRolesAsync()
        {
            
            var roles = await roleManager.Roles.ToListAsync();

            return  roles;
        }

        [HttpGet("GetUserRole")]
        public async Task<IList<string>> GetUserRole(string userId)
        {

            var user = await userManager.FindByIdAsync(userId);
            var roles = await userManager.GetRolesAsync(user);
            return roles;
        }



        [HttpPost("CreateRole")]
        public void CreateRole(string Name)
        {
            myRoles m = new myRoles();
            m.Id = "ddsds";
            m.Name = "FFF";
            db.Roles.Add(m);
            db.SaveChanges();
        }
    }
}