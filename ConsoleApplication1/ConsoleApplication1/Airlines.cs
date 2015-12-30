using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FlightBlockSystem
{
    public delegate void priceCutEvent(Int32 p);

    public class Airlines
    {
        static Random range = new Random();

        public static event priceCutEvent priceCut;
        public static Int32 numberOfTickets = 500;
        public static Int32 numberOfBookedTickets = 0;
        public static Int32 numberOfTicketsToBeBooked;
        public static Int32 priceCutCount = 0;
        public static Int32 maximumPriceCut = 20;

        private static Int32 ticketPrice = 650;
        private static Int32 currentPrice;
        public static String airlineName;
        private static object threadLockAir = new object();
        public Int32 getPrice()
        {
            return ticketPrice;
        }

        public static Int32 getCurrentPrice()
        {
            return currentPrice;
        }

        public String getThreadName()
        {
            String name = airlineName; 
            return name;
        }

        public void changePrice()
        {
            lock (threadLockAir)
            {
                currentPrice = pricingModel();
                if (currentPrice < ticketPrice)
                {
                    if (priceCut != null)
                    {
                        priceCut(currentPrice);
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    Console.WriteLine("Price Cut has not been made.");
                }
                ticketPrice = currentPrice;
            }
        }

        public static Int32 pricingModel()
        {
            Random rand = new Random();
            float temp = (numberOfBookedTickets / float.Parse(numberOfTickets.ToString())) * 100;
            return rand.Next(100, 900) + (Int32) temp;
        }

        public void airlineFunction()
        {
            while (priceCutCount < maximumPriceCut)
            {
                changePrice();
                airlineName = Thread.CurrentThread.Name;
                Thread[] OPThread = new Thread[5];
                for (Int32 i = 0; i < 5; i++)
                {
                    OPThread[i] = new Thread(new ThreadStart(OrderProcessor.Processor));
                    OPThread[i].Name = Thread.CurrentThread.Name;
                    OPThread[i].Start();
                }
          //      priceCutCount++;
            }
 
        }
        public void encryptFunc(long cc, float amt)
        {
            EncryptionDecryption.IService ed = new EncryptionDecryption.ServiceClient();

            string creditcardno;
            string amount;
            creditcardno = cc.ToString();
            amount = amt.ToString();

            string encryptedccn = ed.Encrypt(creditcardno).ToString();
            string encryptedamt = ed.Encrypt(amount).ToString();

            BankService bs = new BankService();
            bs.checkCC(encryptedccn, encryptedamt);
        }
    }
}
