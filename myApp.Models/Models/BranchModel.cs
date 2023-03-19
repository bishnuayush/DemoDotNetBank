using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApp.Models
{
    public class BranchModel
    {
        public int branchid { get; set; }

        [Required]
        [Display(Name ="BRANCH NUMBER")]
        public string branchNum { get; set; }

        [Required]
        [Display(Name = "BRANCH NAME")]
        public string branchName { get; set; }

    }
}
