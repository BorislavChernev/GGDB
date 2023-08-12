using GoodGameDatabase.Services.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Areas.Admin.Controllers
{
    public class DiscussionController : AdminController
    {
        private readonly ILogger<DiscussionController> logger;
        private readonly IDiscussionService discussionService;
        public DiscussionController(ILogger<DiscussionController> logger, IDiscussionService discussionService)
        {
            this.logger = logger;
            this.discussionService = discussionService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.discussionService.DeleteDiscussionByIdAsync(id);

                return RedirectToAction("All", "Discussion");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while trying to delete a discussion by it's id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }
    }
}
