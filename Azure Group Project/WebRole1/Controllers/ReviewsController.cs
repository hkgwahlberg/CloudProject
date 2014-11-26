using GroupProjectWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebRole1.Controllers
{
    public class ReviewsController : Controller
    {
        List<RestaurantReview> dummyReviews;
        List<Restaurant> dummyRestaurants;

        public ReviewsController()
        {
            dummyRestaurants = new List<Restaurant>()
            {
                new Restaurant() { RestaurantId=1, Name ="Restaurant 1", Address= "Adress1", Phone="070404040", ImageURL ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg"},
                new Restaurant() { RestaurantId=2, Name ="Restaurant 2", Address= "Adress2", Phone="070404040", ImageURL ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg"},
                new Restaurant() { RestaurantId=3, Name ="Restaurant 3", Address= "Adress3", Phone="070404040", ImageURL ="http://www.blayney.nsw.gov.au/Images/UserUploadedImages/488/Sams%20Restaurant%20Thumbnail%20243x243.jpg"},
            };

            dummyReviews = new List<RestaurantReview> { 
                new RestaurantReview { RestaurantReviewId = 1, Restaurant = dummyRestaurants[0],  PostedDate = DateTime.Now, Grade = 1, Review = "Sjukt dåligt!", Reviewer = "Hannah" }, 
                new RestaurantReview { RestaurantReviewId = 2, Restaurant = dummyRestaurants[1], PostedDate = DateTime.Now, Grade = 3, Review = "Helt ok!", Reviewer = "Hannah" },
                new RestaurantReview { RestaurantReviewId = 3, Restaurant = dummyRestaurants[2], PostedDate = DateTime.Now, Grade = 5, Review = "Sjukt bra", Reviewer = "Hannah" }};
           
        }
        // GET: Review
        public ActionResult Index()
        {
             return View(dummyReviews);
        }

        // GET: Review/Details/5
        public ActionResult Details(int id)
        {
            var review = dummyReviews.Single(rev => rev.RestaurantReviewId == id);
            return View(review);
        }

        // GET: Review/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Review/Create
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

        // GET: Review/Edit/5
        public ActionResult Edit(int id)
        {
            //autofill form
            var review = dummyReviews.Single(rev => rev.RestaurantReviewId == id);
            return View(review);
        }

        // POST: Review/Edit/5
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

        // GET: Review/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Review/Delete/5
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
