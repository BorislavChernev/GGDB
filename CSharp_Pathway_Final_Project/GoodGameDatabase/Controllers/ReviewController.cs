using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Review;
using Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;

namespace GoodGameDatabase.Web.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly ILogger<ReviewController> logger;
        private readonly IReviewService reviewService;

        public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService)
        {
            this.logger = logger;
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> CreateNew(Review review)
        {
            try
            {
                string id = GetUserId();
                review.AuthorId = Guid.Parse(id);

                await reviewService.CreateNewReviewAsync(review);

                return RedirectToAction("Details", "Game", new { Id = review.GameId });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while creating a new review.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while returning Review/Index view.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> AllGameReview(int id)
        {
            try
            {
                ICollection<GameReviewViewModel> reviews
                    = await this.reviewService.GetAllGameReviewsByIdAsync(id);

                dynamic model = new ExpandoObject();
                
                model.Reviews = reviews;
                model.GameId = id;

                return View("AllGameReview", model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all games.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }
    }
}
