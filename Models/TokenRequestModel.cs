﻿using System.ComponentModel.DataAnnotations;

namespace chatApplication.Models
{
    public class TokenRequestModel
    {
        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Password { get; set; }
    }
}