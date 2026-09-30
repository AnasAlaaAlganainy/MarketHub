using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Account
{
    public class LoginDTO
    {
        //[Required(ErrorMessage = "User Name Required")]
        //public string UserName { get; set; }

        [Required(ErrorMessage = "Password Required")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Email Required")]
        [EmailAddress(ErrorMessage = "InValid Email Syntax")]
        public string Email { get; set; }

    }
}
