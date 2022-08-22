using System;
using System.Collections.Generic;

namespace chatApplication.Models
{
    public class AuthModel
    {
        public string UserId { get; set; }
        public string Message { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public string Token { get; set; }
        public DateTime ExpiresOn { get; set; }
        public int CustomerService { get; set; }
        public int Accounting { get; set; }
        public int Maintenance { get; set; }
        public int Programming { get; set; }
        public int BranchId { get; set; }
    }
}