using GoodGameDatabase.Services.Data;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Discussion;
using GoodGameDatabase.Web.ViewModels.Game;
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            DiscussionDetailsViewModel viewModel = await discussionService
                .GetDetailsByIdAsync(id);

            return View(viewModel);
        }
    }
}
