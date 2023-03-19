using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApp.Models
{
    public class LoginModel
    {
        [Required]
        [Display(Name ="Email")]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [Display(Name ="Password")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
