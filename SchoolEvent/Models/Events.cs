using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolEvent.Models
{
    public class Events
    {
        public int Id { get; set; }

        [Required]
        public string EventName { get; set; }

        [Required]
        public string EventDate { get; set; }

        [Required]
        public string EventAmount { get; set; }

        [Required]
        public string EventId { get; set; }

        public bool EventExpired { get; set; }
    }
}