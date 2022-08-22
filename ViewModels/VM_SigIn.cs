using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace chatApplication.ViewModels
{
    public class VM_SigIn
    {

        [Required(ErrorMessage ="يجب ادخال رقم الهاتف")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "يجب ادخال كلمة المرور")]
        public string Password { get; set; }
    }
}
