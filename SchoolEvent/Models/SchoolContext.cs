using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SchoolEvent.Models
{
    public class SchoolContext : DbContext
    {
        public DbSet<UserBooking> userBookings { get; set; }
        public DbSet<Events> events { get; set; }
        public DbSet<AccountLogin> accountLogins { get; set; }
        public DbSet<Transections> transections { get; set; }
    }
}