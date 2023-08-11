using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Game;
using GoodGameDatabase.Web.ViewModels.Review;

using Microsoft.AspNetCore.Mvc;

using Library.Controllers;
using System.Dynamic;
using PagedList;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GoodGameDatabase.Web.Controllers
{
    public class GameController : BaseController
    {
        private readonly ILogger<GameController> logger;
        private readonly IGameService gameService;
        private readonly IReviewService reviewService;
        public GameController(ILogger<GameController> logger, IGameService gameService, IReviewService reviewService)
        {
            this.logger = logger;
            this.gameService = gameService;
            this.reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int? page)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;

            try
            {
                ICollection<AllGameViewModel> viewModels
                    = await gameService.GetAllGamesAsync();

                int totalViewModels = viewModels.Count;
                int totalPages = (int)Math.Ceiling((double)totalViewModels / pageSize);

                bool hasPreviousPage = pageNumber > 1;
                bool hasNextPage = pageNumber < totalPages;

                PagedViewModel pagedViewModel = new PagedViewModel
                {
                    Action = "All",
                    Controller = "Game",
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalViewModels = totalViewModels,
                    TotalPages = totalPages,
                    HasPreviousPage = hasPreviousPage,
                    HasNextPage = hasNextPage
                };

                dynamic dynamicViewModel = new ExpandoObject();

                dynamicViewModel.ViewModels = viewModels.ToPagedList(pageNumber, pageSize).ToList();
                dynamicViewModel.PageViewModel = pagedViewModel;

                return View(dynamicViewModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all games.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            dynamic model = new ExpandoObject();


            try
            {
                GameDetailsViewModel game = await this.gameService
                    .GetGameDetailsByIdAsync(id);

                ICollection<GameReviewViewModel> reviews = await this.reviewService.GetAllGameReviewsByIdAsync(game.Id);

                model.Game = game;
                model.Reviews = reviews;

                return View(game);

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all game reviews by given gameId.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> BestFive()
        {
            try
            {
                ICollection<BestSixGameViewModel> bestFiveGames = await this.gameService.GetBestGamesAsync();

                return View(bestFiveGames);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching best games.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }


        public async Task<IActionResult> Edit(int id, EditGameViewModel viewModel)
        {
            try
            {
                await this.gameService.EditGameByIdAsync(id, viewModel);

                return RedirectToAction("Details", new { id });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while editing a game by its id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
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

                return RedirectToAction("Details", new { id = game.Id });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while creating a game.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }


        public async Task<IActionResult> Rate(int gameId, int rating)
        {
            try
            {
                string userId = this.GetUserId();

                await this.gameService.RateGameByIdAsync(gameId, rating, userId);

                return RedirectToAction("Details", new { id = gameId });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while rating a game by it's id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }


        public async Task<IActionResult> Like(int gameId)
        {
            try
            {
                Guid userId = Guid.Parse(this.GetUserId());

                await this.gameService.LikeGameByIdAsync(gameId, userId);



                return RedirectToAction("Details", new { id = gameId });

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while liking a game by it's id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }


        public async Task<IActionResult> Wish(int gameId)
        {
            try
            {
                Guid userId = Guid.Parse(this.GetUserId());

                await this.gameService.WishGameByIdAsync(gameId, userId);

                return RedirectToAction("Details", new { id = gameId });

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while wishing a game by it's id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Liked(int? page)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;

            try
            {
                Guid userId = Guid.Parse(this.GetUserId());

                ICollection<AllGameViewModel> viewModels
                    = await this.gameService.GetAllLikedGamesByUserIdAsync(userId);

                int totalViewModels = viewModels.Count;
                int totalPages = (int)Math.Ceiling((double)totalViewModels / pageSize);

                bool hasPreviousPage = pageNumber > 1;
                bool hasNextPage = pageNumber < totalPages;

                PagedViewModel pagedViewModel = new PagedViewModel
                {
                    Action = "Liked",
                    Controller = "Game",
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalViewModels = totalViewModels,
                    TotalPages = totalPages,
                    HasPreviousPage = hasPreviousPage,
                    HasNextPage = hasNextPage
                };

                dynamic dynamicViewModel = new ExpandoObject();

                dynamicViewModel.ViewModels = viewModels.ToPagedList(pageNumber, pageSize).ToList();
                dynamicViewModel.PageViewModel = pagedViewModel;

                return View(dynamicViewModel);

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all liked games by user id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Rated(int? page)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;

            try
            {
                Guid userId = Guid.Parse(this.GetUserId());

                ICollection<AllGameViewModel> viewModels
                    = await this.gameService.GetAllRatedGamesByUserIdAsync(userId);

                int totalViewModels = viewModels.Count;
                int totalPages = (int)Math.Ceiling((double)totalViewModels / pageSize);

                bool hasPreviousPage = pageNumber > 1;
                bool hasNextPage = pageNumber < totalPages;

                PagedViewModel pagedViewModel = new PagedViewModel
                {
                    Action = "Rated",
                    Controller = "Game",
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalViewModels = totalViewModels,
                    TotalPages = totalPages,
                    HasPreviousPage = hasPreviousPage,
                    HasNextPage = hasNextPage
                };

                dynamic dynamicViewModel = new ExpandoObject();

                dynamicViewModel.ViewModels = viewModels.ToPagedList(pageNumber, pageSize).ToList();
                dynamicViewModel.PageViewModel = pagedViewModel;

                return View(dynamicViewModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all rated games by user id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Wished(int? page)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;

            try
            {
                Guid userId = Guid.Parse(this.GetUserId());

                ICollection<AllGameViewModel> viewModels
                    = await this.gameService.GetAllWishedGamesByUserIdAsync(userId);

                int totalViewModels = viewModels.Count;
                int totalPages = (int)Math.Ceiling((double)totalViewModels / pageSize);

                bool hasPreviousPage = pageNumber > 1;
                bool hasNextPage = pageNumber < totalPages;

                PagedViewModel pagedViewModel = new PagedViewModel
                {
                    Action = "Wished",
                    Controller = "Game",
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalViewModels = totalViewModels,
                    TotalPages = totalPages,
                    HasPreviousPage = hasPreviousPage,
                    HasNextPage = hasNextPage
                };

                dynamic dynamicViewModel = new ExpandoObject();

                dynamicViewModel.ViewModels = viewModels.ToPagedList(pageNumber, pageSize).ToList();
                dynamicViewModel.PageViewModel = pagedViewModel;

                return View(dynamicViewModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all wished games by user id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        public async Task<IActionResult> Delete (int id)
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