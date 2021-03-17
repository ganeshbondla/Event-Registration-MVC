using SchoolEvent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolEvent.ViewModels
{
    public class TransectionViewModel
    {
        public Events events { get; set; }
        public Transections transections { get; set; }
        public UserBooking userBooking { get; set; }

    }
}