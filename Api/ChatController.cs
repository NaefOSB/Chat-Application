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
    public class ChatController : ControllerBase
    {
        ApplicationDbContext db;
        private readonly UserManager<myUser> _userManager;

        public ChatController(ApplicationDbContext context, UserManager<myUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }


        [HttpGet("GetChats")]
        public List<Chat> GetChats(int roomId, string userId)
        {
            return db.Chats.Where(c => c.RoomId == roomId && c.myUserId == userId).ToList();
        }

        [HttpPost("CreateChat")]
        public bool CreateChat(VM_Chat mv_chat)
        {
            if (ModelState.IsValid)
            {
                Chat chat = new Chat {
                    Message = mv_chat.Message,
                    Date = DateTime.Now,
                    myUserId = mv_chat.UserId,
                    RoomId = mv_chat.RoomId,
                    Type = mv_chat.Type
                };

                if (chat.RoomId != 0)
                {
                    var room = db.Rooms.Where(r => r.Id == chat.RoomId).FirstOrDefault();
                    room.IsSeen = true;
                    db.Update(room);
                    db.Add(chat);
                    db.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        [HttpDelete("DeleteChat")]
        public void DeleteChat(int chatId)
        {
            db.Chats.Remove(db.Chats.Where(c => c.Id == chatId).FirstOrDefault());
            db.SaveChanges();
        }


        [HttpPost("GetOrCreateIndividualRoom")]
        public async Task<int> GetOrCreateIndividualRoom(string userId, string recievedId)
        {
            try
            {
                var checkIfTheresAnyRoom = await db.IndividualRooms.Where(r => r.SenderId == userId && r.RecievedId == recievedId || r.SenderId == recievedId && r.RecievedId == userId).FirstOrDefaultAsync();
                if (checkIfTheresAnyRoom != null)
                {
                    return checkIfTheresAnyRoom.Id;
                }
                IndividualRoom room = new IndividualRoom()
                {

                    SenderId = userId,
                    RecievedId = recievedId

                };

                db.Add(room);
                db.SaveChanges();
                return room.Id;
            }
            catch (Exception e)
            {

                return 0;
            }

        }

        [HttpPost("SendIndividualMessage")]
        public bool SendIndividualMessage(string Message, string senderId, int roomId)
        {
            try
            {
                IndividualChat chat = new IndividualChat()
                {
                    Message = Message,
                    Date = DateTime.Now.Date,
                    IndividualRoomId = roomId,
                    myUserId = senderId
                };
                db.IndividualChats.Add(chat);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        [HttpGet("GetIndividualMessaged")]
        public JsonResult GetIndividualMessaged(int roomId)
        {
            var chats = db.IndividualChats.Where(c => c.IndividualRoomId == roomId).ToList();
            return new JsonResult(chats);
        }


        [HttpGet("GetAllIndividualRoom")]
        public async Task<JsonResult> GetAllIndividualRoom(string userId)
        {
            try
            {
                List< VM_IndividualRooms> finalRooms = new List<VM_IndividualRooms>();
                var rooms = await db.IndividualRooms.Where(r => r.SenderId == userId || r.RecievedId == userId).ToListAsync();

                for (int i = 0; i < rooms.Count; i++)
                {
                    var room = rooms[i];
                    var targetUserId = (room.SenderId == userId) ? room.RecievedId : room.SenderId;
                    //var userDetails = await _userManager.FindByIdAsync(targetUserId);
                    var userDetails = await (from user in _userManager.Users
                                      join branch in db.Branches
                                      on user.BranchId equals branch.Id
                                      where user.Id == targetUserId
                                      select new {
                                          userName   = user.UserName,
                                          branchName = branch.Name
                                      }).FirstOrDefaultAsync();

                    VM_IndividualRooms singleRoom = new VM_IndividualRooms() {
                    
                        RoomId = room.Id,
                        PeerUserId = targetUserId,
                        PeerUserName = userDetails.userName,
                        BranchName = userDetails.branchName
                    };

                    finalRooms.Add(singleRoom);


                }
                if (finalRooms.Count > 0)
                {
                    finalRooms.Reverse();
                }

                return new JsonResult(finalRooms);
            }
            catch (Exception e)
            {

                return new JsonResult(0);
            }

        }



    }
}