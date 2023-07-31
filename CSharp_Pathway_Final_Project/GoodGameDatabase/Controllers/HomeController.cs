using GoodGameDatabase.Services.Data;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Game;
using Library.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static GoodGameDatabase.Web.Areas.Admin.AdminConstants;

namespace GoodGameDatabase.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGameService gameService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IGameService gameService)
        {
            _logger = logger;
            this.gameService = gameService;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            ICollection<BestFiveGameViewModel> bestFiveGames;
            try
            {
                bestFiveGames = await gameService.GetBestFiveAsync();
            }
            catch (Exception)
            {
                return this.BadRequest("Something went wrong try again later!");
            }

            return View(bestFiveGames);
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}