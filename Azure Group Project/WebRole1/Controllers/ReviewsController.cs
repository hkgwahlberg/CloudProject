using Domain;
using GroupProjectWeb.Models.Restaurant;
using GroupProjectWeb.Models.Review;
using GroupProjectWeb.Storage.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace GroupProjectWeb.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewStorage storage;

        public ReviewsController(IReviewStorage storage)
        {
            this.storage = storage;
        }

        // GET: Review
        public async Task<ActionResult> Index()
        {
            var reviews = await storage.GetAllReviews();
            return View(reviews);
        }

        // GET: Review/Details/5
        public async Task<ActionResult> Details(int id)
        {
            var review = await storage.GetReview(id);
            return View(review);
        }

        // GET: Review/Create
        public ActionResult Create(string restaurantName, int restaurantId)
        {
            var review = new ReviewViewModel
            {
                RestaurantId = restaurantId,
                RestaurantName = restaurantName,
                PostedDate = DateTime.Now
            };
            return View(review);
        }

        // POST: Review/Create
        [HttpPost]
        public async Task<ActionResult> Create(ReviewViewModel review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }
            await storage.AddReview(review);

            return RedirectToAction("Index");
        }

        // GET: Review/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var review = await storage.GetReview(id);
            return View(review);
        }

        // POST: Review/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(ReviewViewModel review)
        {
            if (!ModelState.IsValid)
            {
                return View(review);
            }
            await storage.EditReview(review);

            return RedirectToAction("Index");
        }

        // GET: Review/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var review = await storage.GetReview(id);
            return View(review);
        }

        // POST: Review/Delete/5
        [HttpPost]
        public async Task<ActionResult> Delete(ReviewViewModel review)
        {
            try
            {
                await storage.DeleteReview(review.RestaurantReviewId);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
