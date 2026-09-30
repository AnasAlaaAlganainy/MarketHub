using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Account
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "UserName Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "User Name Must Be InBetween 3 -> 50")]
        public string UserName { get; set; }

        [StringLength(100, MinimumLength = 3)]
        public string? FullName { get; set; } = String.Empty;

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage ="InValid Email Syntax")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        //[MinLength(6,ErrorMessage = "Password Must be At Least 6 characters")] عاملها فى program identity options 
        public string Password { get; set; }

        [Required(ErrorMessage = "Phone Number Required")]
        [MinLength(11,ErrorMessage = "Phone Number Must Be At Least 11 character ")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [MaxLength(100,ErrorMessage = "Address Must be Less Than Or Equal to 100 Characters")]
        public string? Address { get; set; }

    }
}
