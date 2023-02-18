using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace source.Models
{
    public class Account
    {
        [Key]
        public string id { set; get; } = default!;

        [Required(ErrorMessage = "User name is require")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "account name has at least 6 characters")]
        public string userName { set; get; } = default!;

        [Required(ErrorMessage = "email is require")]
        [EmailAddress(ErrorMessage = "is valid email")]
        public string email { set; get; } = default!;

        [Required(ErrorMessage = "Password is require")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "password has at least 6 characters")]
        public string hashPassword { set; get; } = default!;

        public DateTime? createdAt { set; get; } = DateTime.Now;

        public Role Role { set; get; } = default!;
    }


    public class UserRegister
    {
        public string Email { get; set; } = default!;

        public string UserName { get; set; } = default!;

        public string Password { get; set; } = default!;

        public string ConfirmPassword { get; set; } = default!;
    }
}