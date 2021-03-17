using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SchoolEvent.Models
{
    public class UserBooking
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Your Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Your Email")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Your Mobile Number")]
        public string Mobile { get; set; }
        public string Uid { get; set; }

        public string Eventid { get; set; }
    }
}