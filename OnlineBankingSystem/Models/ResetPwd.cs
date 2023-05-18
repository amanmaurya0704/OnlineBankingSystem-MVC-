using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBankingSystem.Models
{
    public class ResetPwd
    {
        public int Id { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "The Password cannot be empty.")]
        [DisplayName("Password")]
        [MaxLength(100, ErrorMessage = "The Password cannot be longer than 100 characters.")]
        public string CurrentPassword
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [DisplayName("Password confirmation")]
        [MaxLength(100, ErrorMessage = "The Password cannot be longer than 100 characters.")]
        
        public string NewPassword
        {
            get;
            set;
        }

        [DataType(DataType.Password)]
        [DisplayName("Password confirmation")]
        [MaxLength(100, ErrorMessage = "The Password cannot be longer than 100 characters.")]
        [Compare("NewPassword", ErrorMessage = "The entered passwords do not match.")]
        public string NewPasswordConfirm
        {
            get;
            set;
        }
    }
}