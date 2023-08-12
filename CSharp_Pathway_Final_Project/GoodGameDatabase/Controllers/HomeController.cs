using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Discussion;
using GoodGameDatabase.Web.ViewModels.Game;
using Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using static GoodGameDatabase.Web.Areas.Admin.AdminConstants;

namespace GoodGameDatabase.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;
        private readonly IGameService gameService;
        private readonly IDiscussionService discussionService;

        public HomeController(ILogger<HomeController> logger, IGameService gameService, IDiscussionService discussionService)
        {
            this.logger = logger;
            this.gameService = gameService;
            this.discussionService = discussionService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                if (User.IsInRole(AdminRoleName))
                {
                    return RedirectToAction("Index", "Home", new { Area = AdminAreaName });
                }

                dynamic model = new ExpandoObject();

                if (this.User.IsInRole(AdminRoleName))
                {
                    return RedirectToAction("Index", "Home", new { area = "Admin" });
                }

                ICollection<BestSixGameViewModel> bestSevenGames = await gameService.GetBestGamesAsync();
                ICollection<AllDiscussionViewModel> bestThreeDiscussions = await discussionService.GetBestThreeDiscussionsAsync();

                model.BestGame = bestSevenGames.First();
                model.BestSixGames = bestSevenGames.Skip(1).ToArray();
                model.BestThreeDiscussions = bestThreeDiscussions.ToArray();

                return View(model);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while returning Home/Index view.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while returning privacy view.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }
    }
}