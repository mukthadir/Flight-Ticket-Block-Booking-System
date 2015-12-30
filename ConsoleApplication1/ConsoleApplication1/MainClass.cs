using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FlightBlockSystem
{
    public class MainClass
    {
        static void Main(string[] args)
        {
            Console.WriteLine("CSE 598 Assignemnt 2.");
            Console.WriteLine("Press Enter to start the application.");
            Console.ReadKey();
            Airlines air = new Airlines();
            TravelAgency ta = new TravelAgency();
            BankService bankDetails = new BankService();
            Airlines.priceCut += new priceCutEvent(ta.ticketsOnSale);

            Random rand = new Random();

            Thread[] tas = new Thread[5];
            Thread[] airs = new Thread[3];

            Thread.Sleep(1000);
            Console.WriteLine("Starting Airline threads.");

            for (Int32 j = 0; j < 3; j++)
            {
                airs[j] = new Thread(new ThreadStart(air.airlineFunction));
                airs[j].Name = "Airline " + (j + 1).ToString();
                airs[j].Start();
            }
            Thread.Sleep(1000);
            Console.WriteLine("Started airline treads.");

            int counter = 127000;
            int[] threadNumber = new int[5];

            for (int i = 0; i < 5; i++) {
                bankDetails.creditCardNumber[i] = counter;
                bankDetails.travelAgencyName[i] = String.Format("Travel Agency {0}", i + 1);
                bankDetails.balance[i] = rand.Next(20000, 30000);
                counter++;
            }

            TravelAgency.BankObject(bankDetails);
            Thread.Sleep(1000);
            Console.WriteLine("Starting travel agency threads.");

            for (Int32 i = 0; i < 5; i++)
            {
                tas[i] = new Thread(new ThreadStart(ta.travelAgencyFunc));
                tas[i].Name = "Travel Agency " + (i + 1).ToString();
                tas[i].Start();
            }
            Thread.Sleep(1000);
            Console.WriteLine("Started travel agency threads.");

            for (Int32 i = 0; i < 3; i++)
            {
                airs[i].Join();
            }

            for (Int32 i = 0; i < 5; i++)
            {
                tas[i].Join();
            }

            Thread.Sleep(3000);
            Console.WriteLine();
            Console.WriteLine("Thank you for shopping with us.");
            Thread.Sleep(1000);
            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
