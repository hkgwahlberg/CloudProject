using GroupProjectWeb.Storage.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroupProjectWeb.Models.Review;
namespace GroupProjectWeb.Storage.Implementations.Reviews
{
    public class MockStorage : IReviewStorage
    {
        List<ReviewViewModel> reviews;
        public MockStorage()
        {
            reviews = new List<ReviewViewModel> 
            { 
             new ReviewViewModel { RestaurantReviewId = "1", PostedDate = DateTime.Now, RestaurantName = "Restaurant 1", Reviewer = "Reviewer 1", Grade = 1, Description = "Skitkasst" },
             new ReviewViewModel { RestaurantReviewId = "2", PostedDate = DateTime.Now, RestaurantName = "Restaurant 1", Reviewer = "Reviewer 2", Grade = 3, Description = "OK" },
             new ReviewViewModel { RestaurantReviewId = "3", PostedDate = DateTime.Now, RestaurantName = "Restaurant 1", Reviewer = "Reviewer 3", Grade = 5, Description = "Skirbra" },
             new ReviewViewModel { RestaurantReviewId = "4", PostedDate = DateTime.Now, RestaurantName = "Restaurant 2", Reviewer = "Reviewer 1", Grade = 2, Description = "Skitkasst" },
             new ReviewViewModel { RestaurantReviewId = "5", PostedDate = DateTime.Now, RestaurantName = "Restaurant 2", Reviewer = "Reviewer 2", Grade = 4, Description = "Skitbra" },
             new ReviewViewModel { RestaurantReviewId = "6", PostedDate = DateTime.Now, RestaurantName = "Restaurant 2", Reviewer = "Reviewer 3", Grade = 5, Description = "Skitbra" },
             new ReviewViewModel { RestaurantReviewId = "7", PostedDate = DateTime.Now, RestaurantName = "Restaurant 3", Reviewer = "Reviewer 1", Grade = 1, Description = "Skitkasst" },
             new ReviewViewModel { RestaurantReviewId = "8", PostedDate = DateTime.Now, RestaurantName = "Restaurant 3", Reviewer = "Reviewer 2", Grade = 3, Description = "OK" },
             new ReviewViewModel { RestaurantReviewId = "9", PostedDate = DateTime.Now, RestaurantName = "Restaurant 3", Reviewer = "Reviewer 3", Grade = 4, Description = "Skitbra" },
            };

        }
        public Task AddReview(ReviewViewModel review)
        {
            return Task.Run(() =>
            {
                reviews.Add(review);
            });
        }

        public Task<ReviewViewModel> GetReview(string reviewId)
        {
            return Task.Run(() =>
            {
                var review = reviews
                    .SingleOrDefault(rev => rev.RestaurantReviewId == reviewId);
                return review;
            });
        }

        public Task<List<ReviewViewModel>> GetAllReviews()
        {
            return Task.Run(() =>
            {
                return reviews;
            });
        }

        public Task UpdateReview(ReviewViewModel editedReview)
        {
            return Task.Run(() =>
            {
                var review = reviews
                    .Find(rev => rev.RestaurantReviewId == editedReview.RestaurantReviewId);
                review = editedReview;
            });
        }

        public Task DeleteReview(string reviewId)
        {
            return Task.Run(() =>
           {
               var review = reviews.Find(rev => rev.RestaurantReviewId == reviewId);
               reviews.Remove(review);
           });
        }
    }
}
