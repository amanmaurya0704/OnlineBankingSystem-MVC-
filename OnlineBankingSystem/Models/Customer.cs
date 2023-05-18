using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Models
{
    public enum Account_Type
    {
        Current,
        Saving,
        Demat
    }
    public enum Customer_Gender
    {
        Male,
        Female
    }
    public class Customer
    {

        [Required(ErrorMessage = "*")]
        public int AccountID { get; set; }
        [Required(ErrorMessage = "*")]
        public int AccountNo { get; set; }
        [Required(ErrorMessage = "*")]
        public Account_Type AccountType { get; set; }
        [Required(ErrorMessage = "*")]
        public string Name { get; set; }
        [Required(ErrorMessage = "*")]
        public  Customer_Gender Gender { get; set; }
        [Required(ErrorMessage = "*")]
        [EmailAddress]
        [StringLength(150)]
        [Display(Name = "Email Address: ")]
        public string Email { get; set; }
        [Required(ErrorMessage = "*")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "*")]
        public string Address { get; set; }
        [Required(ErrorMessage = "*")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [StringLength(150, MinimumLength = 6)]
        [Display(Name = "Password: ")]
        public string Password { get; set; }
        public DateTime DateOfOpening { get; set; }

        
    }
    
   
    
}