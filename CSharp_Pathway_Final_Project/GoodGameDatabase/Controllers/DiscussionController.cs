using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Discussion;
using Library.Controllers;
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
    }
}
