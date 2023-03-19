using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myApp.Models
{
    public class CashTransactionModel
    {
        public int Id { get; set; }

        public int transactionId { get; set; }

        public string cash { get; set; }

        public int Count { get; set; }

        public int C100 { get; set; }

        public int C200 { get; set; }

        public int C500 { get; set; }

        public int C2000 { get; set; }
    }
}
