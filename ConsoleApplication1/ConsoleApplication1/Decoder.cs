using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace FlightBlockSystem
{
    static class Decoder
    {
        public static OrderClass getDecodedValue(string strObj)
        {
            String[] attributes = strObj.Split(',');

            OrderClass orderObj = new OrderClass();

            orderObj.receiverId = attributes[2];
            orderObj.senderId = attributes[0];
            orderObj.unitPrice = Double.Parse(attributes[4]);
            orderObj.amount = Int32.Parse(attributes[3]);
            orderObj.cardNo = Int32.Parse(attributes[1]);

            return orderObj;
        }
    }
}
