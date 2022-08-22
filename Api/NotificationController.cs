using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using chatApplication.Data;
using chatApplication.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace chatApplication.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        ApplicationDbContext db;
        UserManager<myUser> userManager;
        RoleManager<myRoles> roleManager;

        public NotificationController(ApplicationDbContext context, UserManager<myUser> _userManager, RoleManager<myRoles> _roleManager)
        {
            db = context;
            userManager = _userManager;
            roleManager = _roleManager;
        }


        [HttpGet("GetUserNotification")]
        public JsonResult GetUserNotification(string userId)
        {
            var notification = (from notificationTable in db.Notifications
                                join userNotificationTable in db.UserNotifications
                                on notificationTable.Id equals userNotificationTable.NotificationId
                                where userNotificationTable.myUserId == userId
                                select new { 
                                id = notificationTable.Id,
                                date = notificationTable.Date,
                                notificationMessage = notificationTable.NotificationMessage,
                                title = notificationTable.Title,
                                userNotificationId = userNotificationTable.Id,
                                isReaded = userNotificationTable.IsReaded
                                }).ToList();
            return new JsonResult(notification);
        }

        [HttpPost("SendNotificationToSpecificBranch")]
        public bool SendNotificationToSpecificBranch(int branchId, Notification notification, string state, string adminId)
        {

            try
            {

                // State Options :
                // 1 - All Employees
                // 2 - All Customers
                // 3 - All Users( Employees + Customers )


                // To Add The Notification to Notification Table
                var notificationBody = new Notification();
                notificationBody = notification;
                notificationBody.Date = DateTime.Now.Date;
                db.Notifications.Add(notificationBody);
                db.SaveChanges();

                List<myUser> targetUsers = new List<myUser>();





                // to Get The Target Users

                switch (state)
                {
                    case "Employees":
                        {
                            // For only Employees

                            targetUsers = (from userTable in userManager.Users
                                           join UserRoleTable in db.UserRoles on userTable.Id equals UserRoleTable.UserId
                                           where
                                            UserRoleTable.RoleId == "454AC4C" && userTable.BranchId == branchId ||
                                            UserRoleTable.RoleId == "5CD736F" && userTable.BranchId == branchId ||
                                            UserRoleTable.RoleId == "B09E9AF" && userTable.BranchId == branchId ||
                                            UserRoleTable.RoleId == "364D12E" && userTable.BranchId == branchId
                                           select userTable).ToList();
                            break;
                        }
                    case "Customers":
                        {
                            // For only Customers

                            targetUsers = (from userTable in userManager.Users
                                           join UserRoleTable in db.UserRoles on userTable.Id equals UserRoleTable.UserId
                                           where UserRoleTable.RoleId == "C740DEA" && userTable.BranchId == branchId
                                           select userTable).ToList();
                            break;
                        }
                    case "Users":
                        {
                            // For All Users (Employees + Customers)
                            targetUsers = (from userTable in userManager.Users
                                           join UserRoleTable in db.UserRoles on userTable.Id equals UserRoleTable.UserId
                                           where UserRoleTable.RoleId != "E1765C7" && userTable.BranchId == branchId
                                           select userTable).ToList();
                            break;
                        }
                }

                // to Add The Notification to table of Notification


                foreach (var user in targetUsers)
                {
                    UserNotification userNotification = new UserNotification();
                    userNotification.IsReaded = false;
                    userNotification.myUserId = user.Id;
                    userNotification.NotificationId = notificationBody.Id;

                    db.UserNotifications.Add(userNotification);
                }

                // to show the notification to the admin

                UserNotification adminNotification = new UserNotification();
                adminNotification.IsReaded = true;
                adminNotification.myUserId = adminId;
                adminNotification.NotificationId = notificationBody.Id;

                db.UserNotifications.Add(adminNotification);


                db.SaveChanges();


                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        [HttpPost("SendNotificationToAllBranches")]
        public bool SendNotificationToAllBranches(Notification notification, string adminId)
        {

            try
            {

               

                // To Add The Notification to Notification Table
                var notificationBody = new Notification();
                notificationBody = notification;
                notificationBody.Date = DateTime.Now;
                db.Notifications.Add(notificationBody);
                db.SaveChanges();

                List<myUser> targetUsers = new List<myUser>();





                // to Get The Target Users
                targetUsers = (from userTable in userManager.Users
                               join UserRoleTable in db.UserRoles on userTable.Id equals UserRoleTable.UserId
                               where userTable.Id != adminId
                               select userTable).ToList();

                // to Add The Notification to table of Notification


                foreach (var user in targetUsers)
                {
                    UserNotification userNotification = new UserNotification();
                    userNotification.IsReaded = false;
                    userNotification.myUserId = user.Id;
                    userNotification.NotificationId = notificationBody.Id;

                    db.UserNotifications.Add(userNotification);
                }

                // to show the notification to the admin

                UserNotification adminNotification = new UserNotification();
                adminNotification.IsReaded = true;
                adminNotification.myUserId = adminId;
                adminNotification.NotificationId = notificationBody.Id;

                db.UserNotifications.Add(adminNotification);


                db.SaveChanges();


                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }


        [HttpPost("SendNotificationToSingleUser")]
        public ActionResult SendNotificationToSingleUser(int branchId, string notification, List<string> _targetUsers, string adminId)
        {

            try
            {



                // To Add The Notification to Notification Table
                var notificationBody = new Notification();
                notificationBody.NotificationMessage = notification;
                notificationBody.Date = DateTime.Now;
                db.Notifications.Add(notificationBody);
                db.SaveChanges();

                // to Add The Notification to table of Notification


                foreach (var user in _targetUsers)
                {
                    UserNotification userNotification = new UserNotification();
                    userNotification.IsReaded = false;
                    userNotification.myUserId = user;
                    userNotification.NotificationId = notificationBody.Id;

                    db.UserNotifications.Add(userNotification);
                }

                // to show the notification to the admin

                UserNotification adminNotification = new UserNotification();
                adminNotification.IsReaded = true;
                adminNotification.myUserId = adminId;
                adminNotification.NotificationId = notificationBody.Id;

                db.UserNotifications.Add(adminNotification);

                db.SaveChanges();


                return Ok();

            }
            catch (Exception)
            {
                return NotFound();
            }
        }


        [HttpPost("SetReadNotification")]
        public bool SetReadNotification(int userNotificationId)
        {
            try
            {
                var notification = db.UserNotifications.Where(n => n.Id == userNotificationId).FirstOrDefault();
                if (notification != null)
                {
                    notification.IsReaded = true;
                    db.Update(notification);
                    db.SaveChanges();
                    return true;
                }

                return false ;
            }
            catch (Exception)
            {

                return false;
            }
           
        }


        [HttpPost("SetAllNotificationRead")]
        public async Task<bool> SetAllNotificationRead(string userId)
        {
            try
            {
                var notifications = await db.UserNotifications.Where(n => n.myUserId == userId).ToListAsync();

                for (int i = 0; i < notifications.Count; i++)
                {
                    UserNotification notification = notifications[i];
                    notification.IsReaded = true;
                    db.Update(notification);
                }
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }



    }
}