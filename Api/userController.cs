using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatApplication.Data;
using chatApplication.Models;
using chatApplication.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using chatApplication.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using chatApplication.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace chatApplication.Api
{
    [Route("api/[controller]")]
    [ApiController]


    public class userController : ControllerBase
    {
        ApplicationDbContext db;
        private readonly UserManager<myUser> _userManager;
        private readonly SignInManager<myUser> _signInManager;
        private readonly SigInManager _authService;
        private readonly JWT _jwt;




        public userController(ApplicationDbContext context, UserManager<myUser> userManager, SignInManager<myUser> signInManager, SigInManager authservice, IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            db = context;
            _authService = authservice;
            _jwt = jwt.Value;


        }



        [HttpPost("TryMe")]
        public async Task<JsonResult> TryMeAsync(string yasser)
        {
            return new JsonResult("heelow");


        }


        public JsonResult Error(string message)
        {
            return new JsonResult(new Exception(message));
        }



        public JsonResult Success(string message)
        {
            return new JsonResult(new Exception(message));
        }


    

        //Functio To Create Account User
        [HttpPost("CreateUser")]
        public async Task<AuthModel> RegisterAsync([FromBody] VM_CreateUser user,string roleName)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.RegisterAsync(user, roleName);
                var rooms = await Create4Rooms(result.UserId);

                result.CustomerService = rooms.CustomerService;
                result.Accounting  = rooms.Accounting;
                result.Maintenance = rooms.Maintenance;
                result.Programming = rooms.Programming;

                

                return  result;
            }
            return new AuthModel {Message = "حدث خطاء في عملية الإدخال"};
        }

        // to create 4 rooms for single user
        [HttpPost("Create4Rooms")]
        public async Task<VM_4Rooms> Create4Rooms(string userId)
        {
            string customerServiceId = "454AC4C";
            string accounttingId = "5CD736F";
            string maintenanceId = "B09E9AF";
            string programmingId = "364D12E";
            var customerServiceRoom = new Room() { IsIndividual = false, myRolesId = customerServiceId, myUserId = userId, IsSeen = false };
            var accountingRoom  = new Room() {IsIndividual=false,myRolesId = accounttingId, myUserId= userId, IsSeen  = false };
            var maintenanceRoom = new Room() {IsIndividual=false,myRolesId = maintenanceId,myUserId= userId, IsSeen   = false };
            var programmingRoom = new Room() {IsIndividual=false,myRolesId = programmingId, myUserId= userId, IsSeen  = false };
            db.Add(customerServiceRoom);
            db.Add(accountingRoom);
            db.Add(maintenanceRoom);
            db.Add(programmingRoom);
            db.SaveChanges();

            VM_4Rooms result = new VM_4Rooms()
            {
                CustomerService = customerServiceRoom.Id,
                Accounting = accountingRoom.Id,
                Maintenance = maintenanceRoom.Id,
                Programming = programmingRoom.Id
            };

            return result;
        }


        //Function To SignIn The User
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
                return Ok(ModelState);
            var result = await _authService.GetTokenAsync(model);
            if(result != null && result.UserId != null)
            {
                var rooms = await Get4Rooms(result.UserId);

                result.CustomerService = rooms.CustomerService;
                result.Accounting = rooms.Accounting;
                result.Maintenance = rooms.Maintenance;
                result.Programming = rooms.Programming;

            }



            if (!result.IsAuthenticated)
                return Ok(result);

            return Ok(result);
        }

        [HttpGet("Get4Rooms")]
        public  async Task<VM_4Rooms> Get4Rooms(string userId)
        {
            string customerServiceId = "454AC4C";
            string accounttingId = "5CD736F";
            string maintenanceId = "B09E9AF";
            string programmingId = "364D12E";
            var customerServiceRoom = db.Rooms.Where(r=>r.myRolesId == customerServiceId && r.myUserId == userId).SingleOrDefault();
            var accountingRoom = db.Rooms.Where(r => r.myRolesId == accounttingId && r.myUserId == userId).SingleOrDefault();
            var maintenanceRoom = db.Rooms.Where(r => r.myRolesId == maintenanceId && r.myUserId == userId).SingleOrDefault();
            var programmingRoom = db.Rooms.Where(r => r.myRolesId == programmingId && r.myUserId == userId).SingleOrDefault();


            VM_4Rooms result = new VM_4Rooms()
            {
                CustomerService = customerServiceRoom.Id,
                Accounting = accountingRoom.Id,
                Maintenance = maintenanceRoom.Id,
                Programming = programmingRoom.Id
            };

            return result;
        }


        //Function To Get User All Information
        [Authorize]
        [HttpGet("GetUserInformation")]
        public async Task<JsonResult> GetUserInformation(string id)
        {
            
            try
            {
                if (id == null)
                {
                    return Error("هاذا المستخدم غير موجود");

                }
                myUser user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return Error("هاذا المستخدم غير موجود");
                }
                return new JsonResult(user);
            }
            catch
            {
                return Error("خطاء  غير متوقع");
            }

        }








        
        ////FUnction To The Name && If He SignIn
        [HttpGet("CurrentUser")]
        public async Task<JsonResult> CurrentUser()
        {
 
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = _userManager.Users.Where(u => u.PhoneNumber == userId).SingleOrDefault();
            var Identity = new
            {
                name = user?.UserName,
                IsAuthenticated = User.Identity.IsAuthenticated
            };
            return new JsonResult(Identity);
        }


        [Authorize]
        [HttpGet("SignOut")]
        public async Task SignOut()
        {
           
            await _signInManager.SignOutAsync();
        }




        //Function To Update The User Information
        [Authorize]
        [HttpPut("UpdateUser")]
        public async Task<JsonResult> UpdateUser([Bind(" Name, PhoneNumber, BranchID, Address, Password")] VM_CreateUser vm_user, string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                return Error("المستخدم غير موجود");
            }
            user.Address = vm_user.Address;
            user.UserName = vm_user.Name;
            user.BranchId = vm_user.BranchID;
            user.PhoneNumber = vm_user.PhoneNumber;
            user.PasswordHash = vm_user.Password;
            IdentityResult result =  await _userManager.UpdateAsync(user);
            return new JsonResult(result);
        }




        //Function To Delete The User 
        [Authorize]
        [HttpDelete("DeleteUser")]
        public async Task<JsonResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
               return Error("المستخدم غير موجود");

            }
            await _signInManager.SignOutAsync();
            await _userManager.DeleteAsync(user);
            return Success("تم الحذف بنجاح");
        }

        [HttpGet("GetUserByBranch")]
        public JsonResult GetUserByBranch(string userId,int branchId)
        {
            var users = _userManager.Users.Where(u => u.BranchId == branchId && u.Id != userId).Select(i => new {
            
                id = i.Id,
                userName = i.UserName,
                branchName = i.Branch.Name

            }).ToList();
            return new JsonResult(users);
        }
    }
    

      
}