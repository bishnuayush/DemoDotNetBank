using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApp.Models
{
    public class TransactionModel
    {
        public int id { get; set; }

        public int? userId { get; set; }

        public int? accountId { get; set; }

        [Required]
        [Display(Name ="AMOUNT")]
        public int? amount { get; set; }

        public int deposit { get; set; }

        public int withdrawl { get; set; }

        [Display(Name ="TRANSACTION DATE")]
        public Nullable<System.DateTime> date { get; set; }

        [Required]
        [Display(Name ="DEPOSIT / WITHDRAWL")]
        public string transactionType { get; set; }

        public CashTransactionModel CashTransaction { get; set; }

        public AccountModel Account { get; set; }
        public UserModel User { get; set; }
        public List<AccountModel> accountList { get; set; }

        public int C100 { get; set; }
        public int C200 { get; set; }
        public int C500 { get; set; }
        public int C2000 { get; set; }

    }
}
