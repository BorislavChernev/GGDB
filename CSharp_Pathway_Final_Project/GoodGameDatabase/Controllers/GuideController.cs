using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Guide;
using Library.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Controllers
{
    public class GuideController : BaseController
    {
        private readonly ILogger<GuideController> logger;
        private readonly IGuideService guideService;
        public GuideController(ILogger<GuideController> logger, IGuideService guideService)
        {
            this.logger = logger;
            this.guideService = guideService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            try
            {
                ICollection<AllGuideViewModel> allGuides = await guideService.GetAllGuidesAsync();

                return View(allGuides);

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while fetching all guides.");

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
                this.logger.LogError(ex, "An error occurred while returning Guide/CreateNew view.");

                return BadRequest("Something went wrong. Try again later!");
            }
        }

        public async Task<IActionResult> Create(Guide guide)
        {
            try
            {
                Guid userId = Guid.Parse(this.GetUserId());

                guide.WriterId = userId;

                int guideId = await this.guideService.CreateNewGuideAsync(guide);

                return RedirectToAction("All", "Guide");
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while creating a guide.");

                return BadRequest("Something went wrong. Try again later!");
            }
        }
    }
}
