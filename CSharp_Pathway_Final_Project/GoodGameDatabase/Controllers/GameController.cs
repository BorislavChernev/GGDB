using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Game;
using Library.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameService gameService;
        public GameController(IGameService gameService)
        {
            this.gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            ICollection<AllGameViewModel> allGames;
            try
            {
                allGames = await gameService.GetAllAsync();
            }
            catch (Exception)
            {
                return this.BadRequest("Something went wrong try again later!");
            }

            return View(allGames);
        }
    }
}