using System;
using System.Globalization;
using System.Linq;

namespace CustomerRewardPointsApp
{
    class Program
    {
        static void Main(string[] args)
        {
                   

            var customers = from c in DataAccess.GetCustomers()
                            orderby c.lastName, c.firstName
                            select c;

            Console.WriteLine("{0,-20} {1,-22} {2,-24} {3,-25} {4,-26}", "CUSTOMER NAME", "TRANSACTION ID", "AMOUNT", "POINTS", "DATE");

            foreach (var customer in customers)
            {
                var totalCustomerPoints = 0;

                //iterate over each month            
                for (var month =1; month <=12; month ++)
                {
                    //get transactions for the month
                    var monthlyTransactions = from t in DataAccess.GetTransactions()
                                              where t.customerID == customer.customerID
                                              && t.transactionDate.Month == month
                                              select t;

                    //iterate over transactions for the month
                    var totalMonthlyPoints = 0;
                    foreach (var transaction in monthlyTransactions)
                    {
                        //calculate points and print transaction
                        var monthlyPoints = CalculatePoints(transaction.amount);
                        totalMonthlyPoints  += monthlyPoints;
                        transaction.points = monthlyPoints;
                        var name = customer.firstName + " " + customer.lastName;
                        Console.WriteLine("{0,-21} {1,-22} {2,-24} {3,-25} {4,-26}", name, transaction.transactionID, transaction.amount, transaction.points, transaction.transactionDate);
                    }
                    totalCustomerPoints += totalMonthlyPoints;

                    if (totalMonthlyPoints > 0)
                    {
                        Console.WriteLine("{0,-21} {1,-22} {2,-24} {3,-25} {4,-26}", "MONTHLY POINTS", "", "", totalMonthlyPoints, "");
                    }
                }
           
                
                if (totalCustomerPoints > 0)
                {
                    Console.WriteLine("{0,-21} {1,-22} {2,-24} {3,-25} {4,-26}", "TOTAL CUSTOMER POINTS", "", "", totalCustomerPoints, "");
                }
            }

            Console.ReadLine();
        }

        public static int CalculatePoints(int amount)
        {
            int ptover100 = 0;
            int ptover50 = 0; 
            if (amount / 100 > 0)
            {
                ptover100 = (amount - 100) * 2;
            }
            else
            {
                ptover100 = 0;
            }

            if (amount / 50 > 0 )
            {
                ptover50 = (amount - 50) * 1;
            }
            else
            {
                ptover50 = 0;
            }
            int points = ptover100 + ptover50;


            return points;
        }
    }
}
