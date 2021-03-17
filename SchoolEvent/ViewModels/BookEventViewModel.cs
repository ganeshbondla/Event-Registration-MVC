using SchoolEvent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolEvent.ViewModels
{
    public class BookEventViewModel
    {
        public UserBooking userBooking { get; set; }
        public Events events { get; set; }
    }
}