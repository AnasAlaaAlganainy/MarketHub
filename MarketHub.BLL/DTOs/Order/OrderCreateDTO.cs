using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketHub.BLL.DTOs.Order
{
    public class OrderCreateDTO
    {
        [Required(ErrorMessage = "Shipping Address is required.")]
        [MaxLength(255, ErrorMessage = "Address Must be Less Than Or Equal to 255 Characters")]
        public string ShippingAddress { get; set; }
        
        [Required(ErrorMessage = "Email Address is required.")]
        [EmailAddress(ErrorMessage = "InValid Email Syntax.")]
        [MaxLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [MaxLength(15)]
        [MinLength(11, ErrorMessage = "Phone Number Must Be At Least 11 character.")]
        public string PhoneNumber { get; set; }
    }
}
