
using GroupProjectWeb.Models.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectWeb.Storage.Contracts
{
    public interface IReviewStorage
    {
        Task AddReview(ReviewViewModel review);
        Task<ReviewViewModel> GetReview(int reviewId);
        Task<List<ReviewViewModel>> GetAllReviews();
        Task EditReview(ReviewViewModel review);
        Task DeleteReview(int reviewId);
    }
}
