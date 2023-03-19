using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApp.Models
{
    public class AccountModel
    {

        public int id { get; set; }

        public int? userId { get; set; }

        [Required]
        [Display(Name ="ACCOUNT NUMBER")]
        public string accountNumber { get; set; }     

        [Required]
        [Display(Name ="MINIMUM BALANCE")]
        public int? minBalance { get; set; }

        [Required]
        [Display(Name = "WITHDRAWL LIMIT")]
        public int? withdrawLimit { get; set; }

        [Display(Name = "BALANCE")]
        public int? balance { get; set; }

        public int branchId { get; set; }

        public UserModel User { get; set; }
        public List<UserModel> UserList { get; set; }

        public BranchModel Branch { get; set; }
        public List<BranchModel> BranchList { get; set; }

    }
}
