using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        private readonly IGameService gameService;
        private readonly IDiscussionService discussionService;
        private readonly IGuideService guideService;

        public HomeController(IGameService gameService, IDiscussionService discussionService, IGuideService guideService)
        {
            this.gameService = gameService;
            this.discussionService = discussionService;
            this.guideService = guideService;
        }

        [HttpGet("{action}")]
        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminStatisticsViewModel
            {
                TotalGames = await gameService.GetTotalGamesCount(),
                TotalDiscussions = await discussionService.GetTotalDiscussionsCount(),
                TotalGuides = await guideService.GetTotalGuidesCount(),
            };

            return View(viewModel);
        }
    }
}
