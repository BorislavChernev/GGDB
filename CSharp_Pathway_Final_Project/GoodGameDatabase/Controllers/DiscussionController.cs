using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Discussion;
using Library.Controllers;
using Microsoft.AspNetCore.Mvc;
using PagedList;
using System.Dynamic;

namespace GoodGameDatabase.Web.Controllers
{
    public class DiscussionController : BaseController
    {
        private readonly ILogger<DiscussionController> logger;
        private readonly IDiscussionService discussionService;
        public DiscussionController(ILogger<DiscussionController> logger, IDiscussionService discussionService)
        {
            this.logger = logger;
            this.discussionService = discussionService;
        }

        [HttpGet]
        public async Task<IActionResult> All(int? page)
        {
            int pageSize = 12;
            int pageNumber = page ?? 1;

            try
            {
                ICollection<AllDiscussionViewModel> viewModels = await discussionService.GetAllAsync();

                int totalViewModels = viewModels.Count;
                int totalPages = (int)Math.Ceiling((double)totalViewModels / pageSize);

                bool hasPreviousPage = pageNumber > 1;
                bool hasNextPage = pageNumber < totalPages;

                PagedViewModel pagedViewModel = new PagedViewModel
                {
                    Action = "All",
                    Controller = "Discussion",
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
                this.logger.LogError(ex, "An error occurred while fetching all discussions.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int discussionId)
        {
            try
            {
                DiscussionDetailsViewModel viewModel
                    = await this.discussionService.GetDetailsByIdAsync(discussionId);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching discussion details by id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Join(int discussionId)
        {
            try
            {
                Guid userId = Guid.Parse(this.GetUserId());

                await discussionService.JoinUserByIdAsync(discussionId, userId);

                return RedirectToAction("Details", "Discussion", new { discussionId });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while joining discussion by id.");

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
                this.logger.LogError(ex, "An error occurred while trying to create a discussion.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        public async Task<IActionResult> Create(Discussion discussion)
        {
            try
            {
                Guid userId = Guid.Parse(this.GetUserId());

                discussion.CreatorId = userId;

                int discussionId = await this.discussionService.CreateNewAsync(discussion);

                return RedirectToAction("Details", "Discussion", new { discussionId });
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while creating a discussion.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }
    }
}
