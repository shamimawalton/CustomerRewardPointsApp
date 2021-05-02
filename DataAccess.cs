using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CustomerRewardPointsApp
{
    public class DataAccess
    {
        private static string customerDataFile = @"C:\Users\shami\source\repos\CustomerRewardPointsApp\Data\Customer.json";
        private static string transactionDataFile = @"C:\Users\shami\source\repos\CustomerRewardPointsApp\Data\Transaction.json";
        public static List <Customer> GetCustomers()
        {
            var list = new List<Customer>();            

            using (StreamReader reader = File.OpenText(customerDataFile))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                JArray customerData = (JArray)JToken.ReadFrom(jsonReader);

                foreach(var data in customerData)
                {
                    var customer = new Customer();
                    customer.customerID = Int32.Parse(data["CustomerID"].ToString());
                    customer.firstName = data["FirstName"].ToString();
                    customer.lastName = data["LastName"].ToString();


                    list.Add(customer);
                }
            }

            return list;
        }

        public static List<Transaction> GetTransactions()
        {
            var list = new List<Transaction>();

            using (StreamReader reader = File.OpenText(transactionDataFile))
            using (JsonTextReader jsonReader = new JsonTextReader(reader))
            {
                JArray transactionData = (JArray)JToken.ReadFrom(jsonReader);

                foreach (var data in transactionData)
                {
                    var transaction = new Transaction();

                    transaction.transactionID = Int32.Parse(data["TransactionID"].ToString());
                    transaction.customerID = Int32.Parse(data["CustomerID"].ToString());
                    transaction.amount =Int32.Parse (data["Amount"].ToString());
                    transaction.points = data["Points"]==null ? 0 : Int32.Parse(data["Points"].ToString());
                    transaction.transactionDate = DateTime.Parse(data["TransactionDate"].ToString());
                    list.Add(transaction);
                }
                return list;
            }
        }
    }
}
