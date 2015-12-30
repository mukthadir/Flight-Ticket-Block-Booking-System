using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FlightBlockSystem
{
    public class BankService
    {
        public int[] _creditCardNumber = new int[5];
        public String[] _travelAgencyName = new String[5];
        public int[] _balance = new int[5];

        public  int[] creditCardNumber { get { return _creditCardNumber; } set { _creditCardNumber = value; } }
        public String[] travelAgencyName { get { return _travelAgencyName; } set { _travelAgencyName = value; } }
        public int[] balance { get { return _balance; } set { _balance = value; } }

         public void checkCC(string crc, string amt)
         {
            var checker = decryptFunc(crc, amt);

            for (int i = 0; i < 5; i++)
            {
                if (TravelAgency.bankAccounts.creditCardNumber[i] == checker.Item1) {
                    Thread.Sleep(1000);
                    Console.WriteLine("{0} has provided valid Credit Card.", TravelAgency.bankAccounts.travelAgencyName[i]);
                    if (TravelAgency.bankAccounts.balance[i] > checker.Item2) {
                        Thread.Sleep(1000);
                        Console.WriteLine("{0} has sufficient balance to purchase the tickets.", TravelAgency.bankAccounts.travelAgencyName[i]);
                        TravelAgency.bankAccounts.balance[i] -= (int) checker.Item2;
                    }
                }
            }
         }

        public Tuple<long, float> decryptFunc(string cc, string amt)
        {
            EncryptionDecryption.IService dc = new EncryptionDecryption.ServiceClient();
            long decryptedCC;
            float decryptedAmount;

            decryptedCC =long.Parse( dc.Decrypt(cc).ToString());
            decryptedAmount = float.Parse(dc.Decrypt(amt).ToString());

            return Tuple.Create(decryptedCC, decryptedAmount);  
        }
    }
}
