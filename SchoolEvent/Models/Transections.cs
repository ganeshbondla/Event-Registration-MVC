using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolEvent.Models
{
    public class Transections
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string EventId { get; set; }
        public string TransectionId { get; set; }
        public string TransectionStatus { get; set; }
        public string TransectionDate { get; set; }



    }
}