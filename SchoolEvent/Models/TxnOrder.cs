using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolEvent.Models
{
    public class TxnOrder
    {
        public string orderId { get; set; }
        public string razorpayKey { get; set; }
        public float amount { get; set; }
        public string currency { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string contactNumber { get; set; }
        public string address { get; set; }
        public string description { get; set; }
        public string EventId { get; set; }
        public string BookingId { get; set; }
        public string EventName { get; set; }
    }
}