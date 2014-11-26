using GroupProjectWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebRole1.Controllers
{
    public class RestaurantsController : Controller
    {

        List<Restaurant> dummyRestaurants;

        public RestaurantsController()
        {
            dummyRestaurants = new List<Restaurant>()
            {
                new Restaurant() { RestaurantId=1, Name ="Restaurant 1", Address= "Adress1", Phone="070404040", ImageURL ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg"},
                new Restaurant() { RestaurantId=2, Name ="Restaurant 2", Address= "Adress2", Phone="070404040", ImageURL ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg"},
                new Restaurant() { RestaurantId=3, Name ="Restaurant 3", Address= "Adress3", Phone="070404040", ImageURL ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg"},
            };
        }

        // GET: Restaurant
        public ActionResult Index()
        {
            return View(dummyRestaurants);
        }

        // GET: Restaurant/Details/5
        public ActionResult Details(int id)
        {
            var restaurant = dummyRestaurants.Single(rest => rest.RestaurantId == id);
            return View(restaurant);
        }

        // GET: Restaurant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Restaurant/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurant/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Restaurant/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Restaurant/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Restaurant/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
