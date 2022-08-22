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

namespace chatApplication.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {

        ApplicationDbContext db;
        UserManager<myUser> _user;
        RoleManager<myRoles> _roleManager;
        public RoomController(ApplicationDbContext context, UserManager<myUser> user, RoleManager<myRoles> roleManager)
        {
            db = context;
            _user = user;
            _roleManager = roleManager;
        }

        [HttpGet("GetRooms")]
        public List<Room> GetRooms()
        {
            return db.Rooms.ToList();

        }

        [HttpGet("GetRoomsByRole")]
        public JsonResult GetRoomsByRole(string roleName)
        {
            return new JsonResult( db.Rooms.Where(r=>r.myRoles.Name == roleName).ToList());

        }

        [HttpGet("GetCategoryRooms")]
        public JsonResult GetCategoryRooms(string roleName)
        {



            var rooms = from user in _user.Users
                        join room in db.Rooms
                        on user.Id equals room.myUserId
                        where room.myRoles.Name == roleName && room.IsSeen == false
                        select new
                        {
                           roomId= room.Id,
                            userName =user.UserName,
                            userId=user.Id.ToString(),
                            lastMSG= "msg",
                            lastDaate ="date"
                        };



            //var rooms = db.Rooms.Include(u => u.myUser.UserName).Where(e => e.myRoles.Name == roleName).Select(i => new {

            //    name = i.myUser.UserName,
            //    roomId = i.Id

            //}).ToList();
            //var rooms = db.Rooms.Include(r=>r.myUser.UserName).Include(r=>r.Chats.)
            //var rooms = from user in _user.Users
            //            join room in db.Rooms
            //            on user.Id equals room.myUserId
            //            join chat in db.Chats
            //            on room.Id equals chat.RoomId
            //            where room.myRoles.Name == roleName && room.IsSeen == false
            //            orderby chat.RoomId
            //            select new
            //            {
            //                user.UserName,
            //                user.Id,
            //                chat.Message,
            //                chat.Date
            //            };

            //var rooms = db.Rooms.Where(r=>r.myRoles.Name==roleName && r.IsSeen).Include(u=>u.myUser.UserName).Include(c=>c.)
            //var rooms = db.Chats.Include(r => r.Room.myRoles.Name == roleName && r.Room.IsSeen).Include(i => i.myUser).Select(i => new
            //{

            //}).ToList();

            //var chats = db.Chats.Include(i => i.Room).ThenInclude(i => i.myRoles).Where(c => c.RoomId == roomId).Select(i => new
            //{
            //    Id = i.Id,
            //    Date = i.Date,
            //    Message = i.Message,
            //    Type = i.Type,
            //    RoomId = i.RoomId,
            //    UserId = i.myUserId

            //}).ToList();
            //return new JsonResult(chats);
            return new JsonResult(rooms);

        }

        [HttpGet("TestRooms")]
        public JsonResult TestRooms(string roleName,int branch)
        {
            List<VM_Rooms> rooms = new List<VM_Rooms>();
            rooms = (from user in _user.Users
                        join room in db.Rooms
                        on user.Id equals room.myUserId
                        where room.myRoles.Name == roleName && room.IsSeen == true && user.BranchId == branch
                        select new VM_Rooms
                        {
                            roomId = room.Id,
                            userName = user.UserName,
                            userId = user.Id.ToString(),
                        }).ToList();

            for (int i = 0; i < rooms.Count; i++)
            {
                var lastMandD = (from room in db.Rooms
                                 join chat in db.Chats
                                 on room.Id equals chat.RoomId
                                 where room.Id == rooms[i].roomId
                                 orderby chat.Date descending
                                 select new
                                 {
                                     lastMSG = chat.Message,
                                     lastDate = chat.Date
                                 }).FirstOrDefault() ;
                if(lastMandD != null)
                {
                    rooms[i].lastMSG = lastMandD?.lastMSG.ToString();
                    rooms[i].lastDate = lastMandD.lastDate;
                }
            }
            
            rooms.Reverse();

            return new JsonResult(rooms);
          
        }

        


        [HttpGet("GetRoom")]
        public Room GetRoom(int id)
        {
            return db.Rooms.SingleOrDefault(r => r.Id == id);

        }

        [HttpGet("GetRoomsForUser")]
        public List<Room> GetRoomsForUser(int branchId,string roleId)
        {

            return db.Rooms.Where(r => r.myRolesId == roleId && r.myUser.BranchId == branchId).ToList();

        }

        [HttpPost("CreateOrGetRoom")]
        public int CreateOrGetRoom(string userId,string roleName)
        {
            try
            {
                string roleId = _roleManager.FindByNameAsync(roleName).Result.Id;

                // to check if the currentUser have room already
                var hasRoom = db.Rooms.Where(r => r.myUserId.Equals(userId)  && r.myRolesId.Equals(roleId)).FirstOrDefault();
                if(hasRoom == null)
                {
                    var room = new Room {
                        IsIndividual = false,
                        myRolesId = roleId,
                        myUserId = userId
                    };
                    db.Add(room);
                    db.SaveChanges();
                    return room.Id;
                }
                return hasRoom.Id;
            }
            catch (Exception)
            {
                return 0;
            }

        }
        //[HttpGet("getRoomChats")]
        //public async Task<JsonResult> getRoomChats(string userId,string roleName)
        //{
        //    // to get room id if there is no room it will create  a new one
        //    int roomId = CreateOrGetRoom(userId,roleName);

        //    var chats = db.Chats.Include(i=>i.Room).ThenInclude(i=>i.myRoles).Where(c => c.RoomId == roomId).Select(i=>new {
        //        Id = i.Id,
        //        Date=i.Date,
        //        Message = i.Message,
        //        Type=i.Type,
        //        RoomId=i.RoomId,
        //        UserId=i.myUserId

        //    }).ToList();
        //    if (chats.Count < 1)
        //    {
        //        var fakeDate = new {
        //            id = 0,
        //            date = 0,
        //            message=0,
        //            type=0,
        //            roomId= roomId,
        //            userId=0

        //        };
        //        //chats.Add(new Chat() { RoomId = roomId });
        //        return new JsonResult(fakeDate);
        //    }
        //    //return new JsonResult(chats.Count);
        //    return new JsonResult(chats);
        //}

        [HttpGet("getRoomChats")]
        public async Task<JsonResult> getRoomChats(string userId, int roomId)
        {
            // to get room id if there is no room it will create  a new one

            var chats = db.Chats.Include(i => i.Room).ThenInclude(i => i.myRoles).Where(c => c.RoomId == roomId).Select(i => new {
                Id = i.Id,
                Date = i.Date,
                Message = i.Message,
                Type = i.Type,
                RoomId = i.RoomId,
                UserId = i.myUserId,
                UserName=i.myUser.UserName

            }).ToList();
            return new JsonResult(chats);
        }

    }
}