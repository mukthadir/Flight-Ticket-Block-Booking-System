using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FlightBlockSystem
{
    class TravelAgency
    {
        public static BankService bankAccounts;
        private static object threadLocker = new object();

        public void travelAgencyFunc()
        {
            while (Airlines.priceCutCount < Airlines.maximumPriceCut)
            {
                Airlines air = new Airlines();
                Int32 originalPrice = air.getPrice();
                Random rand = new Random();
                Int32 p = Airlines.getCurrentPrice();
                String airlineThread = air.getThreadName();

                Int32 ticketsToOrder = rand.Next(1, 20);

                if (p < originalPrice)
                {
                    // Create a orderClass object for current booking
                    OrderClass order = new OrderClass();

                    if (Thread.CurrentThread.Name.Equals("Travel Agency 1"))
                    {
                        order.cardNo = bankAccounts.creditCardNumber[0];
                    }
                    else if (Thread.CurrentThread.Name.Equals("Travel Agency 2"))
                    {
                        order.cardNo = bankAccounts.creditCardNumber[1];
                    }
                    else if (Thread.CurrentThread.Name.Equals("Travel Agency 3"))
                    {
                        order.cardNo = bankAccounts.creditCardNumber[2];
                    }
                    else if (Thread.CurrentThread.Name.Equals("Travel Agency 4"))
                    {
                        order.cardNo = bankAccounts.creditCardNumber[3];
                    }
                    else if (Thread.CurrentThread.Name.Equals("Travel Agency 5"))
                    {
                        order.cardNo = bankAccounts.creditCardNumber[4];
                    }

                    order.senderId = Thread.CurrentThread.Name;

                    order.receiverId = airlineThread;
                    order.amount = ticketsToOrder;
                    order.unitPrice = p;

                    String startTime = DateTime.Now.ToString("HH:mm:ss tt");
                    MultiBufferCell.setOne(Encoder(order));

                    String confirmation = ConfirmationClass.getOne();
                    String endTime = DateTime.Now.ToString("HH:mm:ss tt");

                    Thread.Sleep(1000);
                    Console.WriteLine("Order was sent at {0} by {1}. Order was processed at {2} by the airline.", startTime, Thread.CurrentThread.Name, endTime);

                    lock (threadLocker) {
                        Thread.Sleep(1000);
                        Console.WriteLine("{0} depositing amount in its bank account.",Thread.CurrentThread.Name);
                        float deposit = rand.Next(4000, 6000);
                        if(Thread.CurrentThread.Name.Equals("Travel Agency 1"))
                        {
                            TravelAgency.bankAccounts.balance[0] += (int) deposit;
                            Thread.Sleep(1000);
                            Console.WriteLine("Current available balance in your account is {0}", TravelAgency.bankAccounts.balance[0]);
                        }
                        else  if(Thread.CurrentThread.Name.Equals("Travel Agency 2"))
                        {
                            TravelAgency.bankAccounts.balance[1] += (int) deposit;
                            Thread.Sleep(1000);
                            Console.WriteLine("Current available balance in your account is {0}", TravelAgency.bankAccounts.balance[1]);
                        }
                        else if (Thread.CurrentThread.Name.Equals("Travel Agency 3"))
                        {
                            TravelAgency.bankAccounts.balance[2] += (int)deposit;
                            Thread.Sleep(1000);
                            Console.WriteLine("Current available balance in your account is {0}", TravelAgency.bankAccounts.balance[2]);
                        }
                        else if (Thread.CurrentThread.Name.Equals("Travel Agency 4"))
                        {
                            TravelAgency.bankAccounts.balance[3] += (int)deposit;
                            Thread.Sleep(1000);
                            Console.WriteLine("Current available balance in your account is {0}", TravelAgency.bankAccounts.balance[3]);
                        }
                        else if (Thread.CurrentThread.Name.Equals("Travel Agency 5"))
                        {
                            TravelAgency.bankAccounts.balance[4] += (int)deposit;
                            Thread.Sleep(1000);
                            Console.WriteLine("Current available balance in your account is {0}", TravelAgency.bankAccounts.balance[4]);
                        }   
                    }
                }
            }
        }

        public void ticketsOnSale(Int32 p)
        {
            Airlines.priceCutCount++;
            Thread.Sleep(1000);
            Console.WriteLine("Tickets are available for sale. Price cut event has been generated {0} - {1}", Thread.CurrentThread.Name, p);
        }

        public static string Encoder(OrderClass myObject)
        {
            return myObject.senderId + "," + myObject.cardNo + "," + myObject.receiverId + "," + myObject.amount + "," + myObject.unitPrice;
        }

        public static void BankObject(BankService bankObject)
        {
            bankAccounts=bankObject;             
        }
    }
}
