using AutoMapper;
using Common.Helpers;
using Domain;
using GroupProjectWeb.Models.Review;
using GroupProjectWeb.Storage.Contracts;
using Microsoft.ServiceBus.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupProjectWeb.Storage.Implementations.Reviews
{
    public class StorageService : IReviewStorage
    {
        public StorageService()
        {
            Mapper.CreateMap<RestaurantReview, ReviewViewModel>();
            Mapper.CreateMap<ReviewViewModel, RestaurantReview>();
        }

        public async Task AddReview(ReviewViewModel reviewFromView)
        {
            if (string.IsNullOrEmpty(reviewFromView.RestaurantReviewId))
            {
                reviewFromView.RestaurantReviewId = Guid.NewGuid().ToString();
            }

            var review = new RestaurantReview(reviewFromView.RestaurantReviewId)
            {
                RestaurantReviewId = reviewFromView.RestaurantReviewId,
                RestaurantId = reviewFromView.RestaurantId,
                RestaurantName = reviewFromView.RestaurantName,
                Grade = reviewFromView.Grade,
                PostedDate = DateTime.Now,
                Description = reviewFromView.Description,
                Reviewer = reviewFromView.Reviewer
            };

            await SendToStorage(review, "Add", review.PartitionKey);
        }

        public async Task UpdateReview(ReviewViewModel reviewFromView)
        {
            var review = new RestaurantReview(reviewFromView.RestaurantReviewId)
            {
                RestaurantReviewId = reviewFromView.RestaurantReviewId,
                RestaurantId = reviewFromView.RestaurantId,
                RestaurantName = reviewFromView.RestaurantName,
                Grade = reviewFromView.Grade,
                PostedDate = DateTime.Now,
                Description = reviewFromView.Description,
                Reviewer = reviewFromView.Reviewer
            };

            await SendToStorage(review, "Update", review.PartitionKey);
        }

        public async Task<ReviewViewModel> GetReview(string reviewId)
        {
            var review = await AzureStorageHelper.GetEntityFromStorage<RestaurantReview>(
                "Reviews", "Review", reviewId);

            var viewModel = Mapper.Map<ReviewViewModel>(review);

            return viewModel;
        }

        public async Task<List<ReviewViewModel>> GetAllReviews()
        {
            var allReviews = await AzureStorageHelper.GetEntitiesFromStorage<RestaurantReview>("Reviews");

            var reviewsToView = Mapper.Map<List<ReviewViewModel>>(allReviews);

            return reviewsToView;
        }

        public async Task DeleteReview(string reviewId)
        {
            await SendToStorage(reviewId, "Delete", "Review");
        }

        private Task SendToStorage<T>(T itemToStorage, string request, string type)
        {
            return Task.Run(() =>
            {
                var bMessage = new BrokeredMessage(itemToStorage);
                bMessage.Properties["Request"] = request;
                bMessage.Properties["Type"] = type;

                if (request.Equals("Delete"))
                {
                    bMessage.Properties["Id"] = itemToStorage;
                }

                var client = ServiceBusQueueHelper.Client;
                ServiceBusQueueHelper.Client.Send(bMessage);
            });
        }
    }
}
