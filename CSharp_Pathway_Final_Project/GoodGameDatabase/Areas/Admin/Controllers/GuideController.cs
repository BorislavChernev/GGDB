using GoodGameDatabase.Services.Data.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Areas.Admin.Controllers
{
    public class GuideController : Controller
    {
        private readonly ILogger<GuideController> logger;
        private readonly IGuideService guideService;
        public GuideController(ILogger<GuideController> logger, IGuideService guideService)
        {
            this.logger = logger;
            this.guideService = guideService;
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await this.guideService.DeleteGuideByIdAsync(id);

                return RedirectToAction("All", "Guide");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while trying to delete a guide by it's id.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }
    }
}
