using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Game;
using GoodGameDatabase.Web.ViewModels.Review;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Library.Controllers;
using System.Dynamic;

namespace GoodGameDatabase.Web.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameService gameService;
        private readonly IReviewService reviewService;
        public GameController(IGameService gameService, IReviewService reviewService)
        {
            this.gameService = gameService;
            this.reviewService = reviewService;
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
            dynamic model = new ExpandoObject();

            GameDetailsViewModel game = await gameService
                .GetDetailsByIdAsync(id);

            ICollection<GameReviewViewModel> reviews = await reviewService.GetAllGameReviews();

            model.Game = game;
            model.Reviews = reviews;

            return View(model);
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