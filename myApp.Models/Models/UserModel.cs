using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApp.Models
{
    public class UserModel
    {
        public int id { get; set; }
        
        [Required]
        [Display(Name ="FIRST NAME")]
        public string fName { get; set; }

        [Required]
        [Display(Name ="LAST NAME")]
        public string lName { get; set; }

        
        [Display(Name ="MOBILE NUMBER")]
        public string mobileNo { get; set; }

        [Display(Name ="ADDRESS")]
        public string address { get; set; }

        [Required]
        [Display(Name ="EMAIL")]
        public string email { get; set; }

        [Required]
        [Display(Name ="PASSWORD")]
        public string password { get; set; }

        public string image { get; set; }

        [Display(Name = "FULL NAME")]
        public string fullName
        {
            get
            {
                return fName + " " + lName;
            }
        }

        public static implicit operator int(UserModel v)
        {
            throw new NotImplementedException();
        }
    }
}