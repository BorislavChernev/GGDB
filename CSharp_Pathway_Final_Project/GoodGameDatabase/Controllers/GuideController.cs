using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Guide;
using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Controllers
{
    public class GuideController : Controller
    {
        private readonly IGuideService guideService;
        public GuideController(IGuideService guideService)
        {
            this.guideService = guideService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            ICollection<AllGuideViewModel> allGuides;
            try
            {
                allGuides = await guideService.GetAllAsync();
            }
            catch (Exception)
            {
                return this.BadRequest("Something went wrong try again later!");
            }

            return View(allGuides);
        }
    }
}
