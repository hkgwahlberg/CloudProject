using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebRole1.Models;

namespace WebRole1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult RestaurantList()
        {
            var rlist = new List<Restaurant>()
            {
                new Restaurant() {Address = "ad", Grade = 5, Name = "Hej", Review = "fjks njgkrn f"},
                new Restaurant() {Address = "ad", Grade = 5, Name = "Hej", Review = "fjks njgkrn f"},
                new Restaurant() {Address = "ad", Grade = 5, Name = "Hej", Review = "fjks njgkrn f"}
            };
            return View(rlist);
        }

        [HttpPost]
        public ActionResult CreateRestaurant(string name, string address, string review, int grade)
        {
            return View();
        }
    }
}