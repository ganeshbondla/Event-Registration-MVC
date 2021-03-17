using SchoolEvent.Models;
using SchoolEvent.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SchoolEvent.Controllers
{
    public class AccountController : Controller
    {
        SchoolContext context = new SchoolContext();
        // GET: Account
        public ActionResult Index()
        {
            if(Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
           
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountLogin accountLogin)
        {
            if(ModelState.IsValid)
            {
                AccountLogin account = new AccountLogin()
                {
                    Username = accountLogin.Username,
                    Password = accountLogin.Password
                };

                var check = context.accountLogins.Where(a => a.Username == account.Username).ToList();
                
                if(check.Count() == 0)
                {
                    ViewData["LoginError"] = "No Account Found on This Username";
                    return View();
                }
                else
                {
                    var userLogin = context.accountLogins.Where(a => a.Username == account.Username && a.Password == account.Password).SingleOrDefault();

                    if(userLogin != null)
                    {
                        Session["username"] = userLogin.Username;

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewData["LoginError"] = "Invalid Username or Password";
                        return View();
                    }
                }
            }

            ViewData["LoginError"] = "Server Error!";
            return View();
        }

        public ActionResult Events()
        {
            if(Session["username"] != null)
            {
                var listEvents = context.events.ToList();
                return View(listEvents);
            }
            else
            {
                return RedirectToAction("Login"); 
            }
        }

        public ActionResult Transections()
        {
            if(Session["username"] != null)
            {
                var txns = context.transections.ToList();
                return View(txns);
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        public ActionResult TxnDetails(string id)
        {
            if (Session["username"] != null)
            {

                var Transections = context.transections.Where(t => t.TransectionId == id).SingleOrDefault();
                var UserDetails = context.userBookings.Where(u => u.Uid == Transections.UserId).SingleOrDefault();
                var EventDetails = context.events.Where(e => e.EventId == Transections.EventId).SingleOrDefault();

                TransectionViewModel transectionViewModel = new TransectionViewModel()
                {
                    transections = Transections,
                    events = EventDetails,
                    userBooking = UserDetails
                };

                return View(transectionViewModel);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public ActionResult CreateEvent()
        {
            if (Session["username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }     
        }

        [HttpPost]
        public ActionResult CreateEvent(Events events)
        {
            if (Session["username"] != null)
            {
                
                    Random random = new Random();

                    Events evnt = new Events()
                    {
                        EventName = events.EventName,
                        EventDate = events.EventDate,
                        EventAmount = events.EventAmount,
                        EventId = random.Next(10000, 99999).ToString()
                    };

                    context.events.Add(evnt);
                    context.SaveChanges();

                    return RedirectToAction("Events");
                
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        [HttpGet]
        public ActionResult EventEdit(int id)
        {
            if (Session["username"] != null)
            {
                var getDetails = context.events.Where(e => e.Id == id).SingleOrDefault();
                return View(getDetails);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        [HttpPost]
        public ActionResult EventEdit(Events events)
        {
            if (Session["username"] != null)
            {
                if(ModelState.IsValid)
                {
                    var getDetails = context.events.Where(e => e.EventId == events.EventId).SingleOrDefault();

                    getDetails.EventName = events.EventName;
                    getDetails.EventDate = events.EventDate;
                    getDetails.EventAmount = events.EventAmount;
                
                    context.SaveChanges();

                    return RedirectToAction("Events");
                }
                else
                {
                    ViewData["ErrorMsg"] = "ModelState is Not Valid!";
                    return View();
                }
                
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult EventDelete(int id)
        {
            if (Session["username"] != null)
            {
                var del = context.events.Where(e => e.Id == id).SingleOrDefault();
                context.events.Remove(del);
                context.SaveChanges();
                return RedirectToAction("Events");
            }
            else
            {
                return RedirectToAction("Login");
            }
            
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}