using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBlockSystem
{
    class OrderClass
    {
        private String _senderId;
        private long _cardNo;
        private String _receiverId;
        private Int32 _amount; // No. of Tickets
        private Double _unitPrice;

        public String senderId { get { return _senderId; } set { _senderId = value; } }
        public long cardNo { get { return _cardNo; } set { _cardNo = value; } }
        public String receiverId { get { return _receiverId; } set { _receiverId = value; } }
        public Int32 amount { get { return _amount; } set { _amount = value; } }
        public Double unitPrice { get { return _unitPrice; } set { _unitPrice = value; } }
    }
}
