using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Game;
using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Areas.Admin.Controllers
{
    public class GameController : AdminController
    {
        private readonly ILogger<GameController> logger;
        private readonly IGameService gameService;

        public GameController(ILogger<GameController> logger, IGameService gameService)
        {
            this.logger = logger;
            this.gameService = gameService;
        }

        public async Task<IActionResult> CreateNew()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while returning Game/CreateNew view.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        public async Task<IActionResult> Create(Game game)
        {
            try
            {
                await this.gameService.CreateNewGameAsync(game);

                return RedirectToAction("Details", new { id = game.Id, area = "Identity" });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while creating a game.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.gameService.DeleteGameByIdAsync(id);

                return RedirectToAction("All", "Game");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while trying to delete a game by it's id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

    }
}
