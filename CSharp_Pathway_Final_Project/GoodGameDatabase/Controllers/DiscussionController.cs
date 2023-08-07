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
        private readonly IDiscussionService discussionService;
        public DiscussionController(IDiscussionService discussionService)
        {
            this.discussionService = discussionService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            ICollection<AllDiscussionViewModel> allDiscussions;
            try
            {
                allDiscussions = await discussionService.GetAllAsync();
            }
            catch (Exception)
            {
                return this.BadRequest("Something went wrong try again later!");
            }

            return View(allDiscussions);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int discussionId)
        {
            DiscussionDetailsViewModel model
                = await this.discussionService.GetDetailsByIdAsync(discussionId);

            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Join(int discussionId)
        {
            Guid userId = Guid.Parse(this.GetUserId());

            await discussionService.JoinUserByIdAsync(discussionId, userId);

            return RedirectToAction("Details", "Discussion", new { discussionId });
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNew()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Create(Discussion discussion)
        {
            Guid userId = Guid.Parse(this.GetUserId());

            discussion.CreatorId = userId;

            int discussionId = await this.discussionService.CreateNewAsync(discussion);

            return RedirectToAction("Details", "Discussion", new { discussionId });
        }
    }
}
