using GoodGameDatabase.Data.Model;
using GoodGameDatabase.Services.Data.Contracts;
using GoodGameDatabase.Web.ViewModels.Guide;
using Library.Controllers;

using Microsoft.AspNetCore.Mvc;

using System.Dynamic;
using PagedList;

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
        public async Task<IActionResult> All(int? page)
        {
            int pageSize = 6;
            int pageNumber = page ?? 1;

            try
            {
                ICollection<AllGuideViewModel> viewModels = await guideService.GetAllGuidesAsync();

                int totalViewModels = viewModels.Count;
                int totalPages = (int)Math.Ceiling((double)totalViewModels / pageSize);

                bool hasPreviousPage = pageNumber > 1;
                bool hasNextPage = pageNumber < totalPages;

                PagedViewModel pagedViewModel = new PagedViewModel
                {
                    Action = "All",
                    Controller = "Guide",
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
                this.logger.LogError(ex, "An error occurred while fetching all guides.");

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
                this.logger.LogError(ex, "An error occurred while returning Guide/CreateNew view.");

                return View("ErrorPage", "Something went wrong. Try again later!");
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

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                GuideDetailsViewModel viewModel = await guideService.GetGuideDetailsByIdAsync(id);

                return View(viewModel);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "An error occurred while returning discussion details view.");

                return View("ErrorPage", "Something went wrong. Try again later!");
            }
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
