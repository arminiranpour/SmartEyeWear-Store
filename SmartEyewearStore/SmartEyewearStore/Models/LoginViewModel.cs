﻿using System.ComponentModel.DataAnnotations;

namespace SmartEyewearStore.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public string? PendingSurvey { get; set; }
    }
}