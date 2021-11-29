using SchoolEvent.Models;
using SchoolEvent.ViewModels;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Diagnostics;

namespace SchoolEvent.Controllers
{
    public class BookingController : Controller
    {
        SchoolContext _context = new SchoolContext();
       
        public ActionResult Index()
        {
            var Events = _context.events.ToList();
            return View(Events);
        }

        public ActionResult Register(string id)
        {
            var EventDetails = _context.events.Where(e => e.EventId == id).SingleOrDefault();

            UserBooking userBooking = new UserBooking();

            BookEventViewModel bookEventView = new BookEventViewModel()
            { 
                userBooking = userBooking,
                events = EventDetails
            };

            return View(bookEventView);
        }
         
        [HttpPost]
        public ActionResult Register(BookEventViewModel bookEvent)
        {
            Random random = new Random();

                UserBooking bookingDetails = new UserBooking()
                {
                    Name = bookEvent.userBooking.Name,
                    Email = bookEvent.userBooking.Email,
                    Mobile = bookEvent.userBooking.Mobile,
                    Uid = random.Next(100000, 999999).ToString(),
                    Eventid = bookEvent.events.EventId
                };

                var checkUser = _context.userBookings.Where(m => m.Email == bookingDetails.Email && m.Eventid == bookingDetails.Eventid).ToList();

                if(checkUser.Count() == 0)
                {

                    var status = _context.userBookings.Add(bookingDetails);

                    _context.SaveChanges();

                    if (status == null)
                    {
                        ViewData["RegisterStatus"] = "Registration Failed!, Please Try Again";
                        return View();
                    }
                    else
                    {
                         var email = sendEmailAsync(bookingDetails.Uid);
                         var EmailResponse = email.Result;

                        return RedirectToAction("Payment/" + bookingDetails.Uid);
                    }

                }
                else
                {
                    ViewData["RegisterStatus"] = "Username Already Registered For This Event!";
                    return View();
                }
            

        }

        private async Task<object> sendEmailAsync(string uid)
        {
            var getDetails = _context.userBookings.Where(m => m.Uid == uid).SingleOrDefault();

            UserBooking emailDetails = new UserBooking()
            { 
                Name = getDetails.Name,
                Email = getDetails.Email,
                Uid = getDetails.Uid
            };

            var apiKey = "-- sendgrid api key --";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("from-email", "Event Booking");
            var subject = "Event Registered Success #"+emailDetails.Uid;
            var to = new EmailAddress(emailDetails.Email, emailDetails.Name);
            var plainTextContent = "Event Registered Success #" + emailDetails.Uid;
            var htmlContent = "<strong>Event Registered Success #" + emailDetails.Uid + "</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);

            return response.StatusCode;
        }
        
        public ActionResult Payment(string id)
        {
            var BookDetails = _context.userBookings.Where(m => m.Uid == id).SingleOrDefault();

            var EventDetials = _context.events.Where(m => m.EventId == BookDetails.Eventid).SingleOrDefault();
                 
                var amt = EventDetials.EventAmount;
                var amount = float.Parse(amt);
                // Generate random receipt number for order
                Random randomObj = new Random();
                string transactionId = randomObj.Next(10000000, 100000000).ToString();

                Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("-- razorpay key id --", "-- razorpay secrate key --");
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", amount * 100);  // Amount will in paise
                options.Add("receipt", transactionId);
                options.Add("currency", "INR");
                options.Add("payment_capture", "0"); // 1 - automatic  , 0 - manual
                                                     //options.Add("notes", "-- You can put any notes here --");
                Razorpay.Api.Order orderResponse = client.Order.Create(options);
                string orderId = orderResponse["id"].ToString();

                TxnOrder txnOrder = new TxnOrder()
                {
                    orderId = orderResponse.Attributes["id"],
                    razorpayKey = "-- razorpay key id --",
                    amount = amount,
                    currency = "INR",
                    name = BookDetails.Name,
                    email = BookDetails.Email,
                    contactNumber = BookDetails.Mobile,
                    address = EventDetials.EventId,
                    description = EventDetials.EventId,
                    EventId = EventDetials.EventId,
                    BookingId = BookDetails.Uid,
                    EventName = EventDetials.EventName

                };

                return View(txnOrder);
            
        }


        [HttpPost]
        public ActionResult CompleteBooking(string event_id, string bookUser_id)
        {

            string paymentId = Request.Params["rzp_paymentid"];
            string orderId = Request.Params["rzp_orderid"];
            string EventId = event_id;
            string BookUid = bookUser_id;

            Razorpay.Api.RazorpayClient client = new Razorpay.Api.RazorpayClient("-- razorpay key id --", "-- razorpay secrate key --");

            Razorpay.Api.Payment payment = client.Payment.Fetch(paymentId);

            Dictionary<string, object> options = new Dictionary<string, object>();
            options.Add("amount", payment.Attributes["amount"]);
            Razorpay.Api.Payment paymentCaptured = payment.Capture(options);
            string amt = paymentCaptured.Attributes["amount"];

            if (paymentCaptured.Attributes["status"] == "captured")
            {
                var dt = DateTime.Now;

                Transections transections = new Transections()
                {
                    UserId = BookUid,
                    EventId = EventId,
                    TransectionId = paymentId,
                    TransectionStatus = "captured",
                    TransectionDate = dt.ToString("MM/dd/yyyy")
                };

                _context.transections.Add(transections);
                _context.SaveChanges();

                return RedirectToAction("Success", new { id = transections.UserId });
            }
            else
            {
                return RedirectToAction("Failed");
            }
       
        }

        public ActionResult Success(string id)
        {
            var UserDetails = _context.userBookings.FirstOrDefault(u => u.Uid == id);

            var EventDetails = _context.events.Where(e => e.EventId == UserDetails.Eventid).SingleOrDefault();

            var TransectionDetails = _context.transections.Where(t => t.UserId == UserDetails.Uid).SingleOrDefault();

            TransectionViewModel transectionViewModel = new TransectionViewModel()
            {
                userBooking = UserDetails,
                transections = TransectionDetails,
                events = EventDetails
            };

            return View(transectionViewModel);
        }
       
        public ActionResult Failed()
        {
            return View();
        }
    }
}

