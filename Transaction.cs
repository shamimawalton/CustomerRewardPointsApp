using System;
using System.Collections.Generic;
using System.Text;

namespace CustomerRewardPointsApp
{
    public class Transaction
    {
        public int transactionID { get; set; }
        public int customerID { get; set; }
        public int amount { get; set; }
        public int points { get; set; }
        public DateTime transactionDate { get; set; }
    }
}
