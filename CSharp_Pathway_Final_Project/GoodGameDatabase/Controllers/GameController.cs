using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Game;
using Library.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

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
        [AllowAnonymous]
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            GameDetailsViewModel viewModel = await gameService
                .GetDetailsByIdAsync(id);

            return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> BestFive()
        {
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

        [AllowAnonymous]
        public async Task<IActionResult> Edit(int id, EditGameViewModel viewModel)
        {

            await gameService.Edit(id, viewModel);

            return RedirectToAction("Details", new {id = id});
            //return View(viewModel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNew()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Create(Game game)
        {
            await gameService.Create(game);

            return RedirectToAction("Details", new { id = game.Id });
        }
    }
}