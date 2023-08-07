using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Game;
using GoodGameDatabase.Web.ViewModels.Review;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Library.Controllers;
using System.Dynamic;
using PagedList;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        public async Task<IActionResult> All(int? page)
        {
            int pageSize = 4;
            int pageNumber = page ?? 1;

            ICollection<AllGameViewModel> allGames;
            try
            {
                allGames = await gameService.GetAllAsync();
            }
            catch (Exception)
            {
                return this.BadRequest("Something went wrong. Try again later!");
            }

            int totalGames = allGames.Count;
            int totalPages = (int)Math.Ceiling((double)totalGames / pageSize);

            bool hasPreviousPage = pageNumber > 1;
            bool hasNextPage = pageNumber < totalPages;

            PagedGameViewModel pagedViewModel = new PagedGameViewModel
            {
                Games = allGames.ToPagedList(pageNumber, pageSize).ToList(),
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalGames = totalGames,
                TotalPages = totalPages,
                HasPreviousPage = hasPreviousPage,
                HasNextPage = hasNextPage
            };

            return View(pagedViewModel);
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
            Guid userId = Guid.Parse(this.GetUserId());

            await this.gameService.Like(gameId, userId);

            return RedirectToAction("Details", new { id = gameId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Wish(int gameId)
        {
            Guid userId = Guid.Parse(this.GetUserId());

            await this.gameService.Wish(gameId, userId);

            return RedirectToAction("Details", new { id = gameId });
        }

        [AllowAnonymous]
        public async Task<IActionResult> Liked()
        {
            Guid userId = Guid.Parse(this.GetUserId());

            ICollection<AllGameViewModel> likedGames
                = await this.gameService.GetAllLikedGamesByUserIdAsync(userId);

            return View(likedGames);
        }
     
        [AllowAnonymous]
        public async Task<IActionResult> Rated()
        {
            Guid userId = Guid.Parse(this.GetUserId());

            ICollection<AllGameViewModel> ratedGames
                = await this.gameService.GetAllRatedGamesByUserIdAsync(userId);

            return View(ratedGames);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Wished()
        {
            Guid userId = Guid.Parse(this.GetUserId());

            ICollection<AllGameViewModel> wishedGames
                = await this.gameService.GetAllWishedGamesByUserIdAsync(userId);

            return View(wishedGames);
        }
    }
}