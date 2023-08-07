using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Guide;
using Library.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Controllers
{
    public class GuideController : BaseController
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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateNew()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> Create(Guide guide)
        {
            Guid userId = Guid.Parse(this.GetUserId());

            guide.WriterId = userId;

            int guideId = await this.guideService.CreateNewAsync(guide);

            return RedirectToAction("All", "Guide");
        }
    }
}
