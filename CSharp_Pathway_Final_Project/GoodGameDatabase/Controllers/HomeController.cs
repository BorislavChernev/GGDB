using GoodGameDatabase.Services.Data;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Discussion;
using GoodGameDatabase.Web.ViewModels.Game;
using Library.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using static GoodGameDatabase.Web.Areas.Admin.AdminConstants;

namespace GoodGameDatabase.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGameService gameService;
        private readonly IDiscussionService discussionService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IGameService gameService, IDiscussionService discussionService)
        {
            _logger = logger;
            this.gameService = gameService;
            this.discussionService = discussionService;
        }

        public async Task<IActionResult> Index()
        {
            dynamic model = new ExpandoObject();

            if (this.User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            ICollection<BestSixGameViewModel> bestSevenGames;
            ICollection<AllDiscussionViewModel> bestThreeDiscussions;
            try
            {
                bestSevenGames = await gameService.GetBestSixGamesAsync();
                bestThreeDiscussions = await discussionService.GetBestThreeDiscussionsAsync();
            }
            catch (Exception)
            {
                return this.BadRequest("Something went wrong try again later!");
            }

            model.BestGame = bestSevenGames.First();
            model.BestSixGames = bestSevenGames.Skip(1).ToArray();
            model.BestThreeDiscussions = bestThreeDiscussions.ToArray();

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}