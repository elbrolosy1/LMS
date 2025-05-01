using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL__Buisness_Logic_Layer_.Dtos.AccountManager
{
    public class Register
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }

        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 2)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress]
        public string Email { get; set; }


        //[Required(ErrorMessage = "Full name is required.")]
        //[StringLength(maximumLength: 50, MinimumLength = 2)]
        //public string FullNameUser { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
        [Required(ErrorMessage = "Confirm password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
       
        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }
       
        public string Address { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        [Required(ErrorMessage = "Gender is required.")]
        [Display(Name = "Gender")]
        public string Gender { get; set; }

    }
}
