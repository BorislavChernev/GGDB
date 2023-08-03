using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using Library.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IReviewService reviewService;

        public ReviewController(IReviewService reviewService)
        {
            this.reviewService = reviewService;
        }

        public async Task<IActionResult> CreateNew(Review review)
        {
            string id = GetUserId();

            await reviewService.Create(id, review);

            return RedirectToAction("Details", "Game", new {Id = review.GameId});
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
