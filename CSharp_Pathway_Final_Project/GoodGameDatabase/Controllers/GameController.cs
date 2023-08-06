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

            GameDetailsViewModel game = await this.gameService
                .GetDetailsByIdAsync(id);

            ICollection<GameReviewViewModel> reviews = await this.reviewService.GetGameReviews(game.Id);

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
                bestFiveGames = await this.gameService.GetBestFiveAsync();
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

            await this.gameService.Edit(id, viewModel);

            return RedirectToAction("Details", new {id});
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
            await this.gameService.Create(game);

            return RedirectToAction("Details", new { id = game.Id });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Rate(int gameId, int rating)
        {
            string userId = this.GetUserId();

            await this.gameService.Rate(gameId, rating, userId);

            return RedirectToAction("Details", new { id = gameId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Like(int gameId)
        {
            string userId = this.GetUserId();

            await this.gameService.Like(gameId, userId);

            return RedirectToAction("Details", new { id = gameId });
        }
    }
}