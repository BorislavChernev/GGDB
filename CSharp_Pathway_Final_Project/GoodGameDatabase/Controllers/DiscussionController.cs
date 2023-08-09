using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Discussion;
using Library.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> All()
        {
            try
            {
                ICollection<AllDiscussionViewModel> allDiscussions = await discussionService.GetAllAsync();

                return View(allDiscussions);

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all discussions.");

                return BadRequest("Something went wrong. Try again later!");
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

                return BadRequest("Something went wrong. Try again later!");
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

                return BadRequest("Something went wrong. Try again later!");
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

                return BadRequest("Something went wrong. Try again later!");
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

                return BadRequest("Something went wrong. Try again later!");
            }
        }
    }
}
