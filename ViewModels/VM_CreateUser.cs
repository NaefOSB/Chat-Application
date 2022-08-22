using chatApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.ViewModels
{
    public class VM_CreateUser
    {
       
        [Required(ErrorMessage = "يجب إدخال اسمك هناء")]
        public string Name { get; set; }
        [Required(ErrorMessage = "يجب إدخال رقم الجوال الأول على الأقل")]
        public string PhoneNumber { get; set; }
        [Required]

        public int BranchID { get; set; }
        [Required(ErrorMessage = "اكتب عنوان تواجدك")]
        public string Address { get; set; }
        [Required(ErrorMessage = "يجب إدخال كلمة المرور")]
        public string Password { get; set; }
    }
}
