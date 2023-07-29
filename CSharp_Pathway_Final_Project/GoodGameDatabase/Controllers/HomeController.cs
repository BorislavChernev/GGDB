using Library.Controllers;
using Microsoft.AspNetCore.Mvc;

using static GoodGameDatabase.Web.Areas.Admin.AdminConstants;

namespace GoodGameDatabase.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (this.User.IsInRole(AdminRoleName))
            {
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}