using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FlightBlockSystem
{
    public class OrderProcessor
    {
        private static object threadLock1 = new object();
        public static void Processor()
        {
            OrderClass order = Decoder.getDecodedValue(MultiBufferCell.getOne());

            int numberOfTicketsToBeBooked = order.amount;
            long cc = order.cardNo;
            for (int i = 0; i < 5; i++)
            {
                Airlines al = new Airlines();
                al.encryptFunc(order.cardNo, order.amount);

                int unitPrice = Airlines.getCurrentPrice(); // get unit price from airlines

                double totalPrice = unitPrice * numberOfTicketsToBeBooked + 0.05 * unitPrice * numberOfTicketsToBeBooked + 30;
                Thread.Sleep(1000);
                Console.WriteLine();
                Console.WriteLine("*************************************************");
                Console.WriteLine("Following Order is being processed.");
                Console.WriteLine("Order booked for - {0}", order.senderId);
                Console.WriteLine("Order booked by - {0}", order.receiverId);
                Console.WriteLine("Number of tickets booked - {0}", numberOfTicketsToBeBooked);
                Console.WriteLine("Unit Price for each ticket - {0}", unitPrice);
                Console.WriteLine("Total amount charged(including taxes and destination charges) = {0}", totalPrice);
                Console.WriteLine("*************************************************");
                Console.WriteLine();
                Thread.Sleep(3000);
                ConfirmationClass.setOne(String.Format("Order Processed for {0}", order.senderId));
                lock (threadLock1)
                {
                    Airlines.numberOfBookedTickets += numberOfTicketsToBeBooked;
                }
            }
        }
    }
}