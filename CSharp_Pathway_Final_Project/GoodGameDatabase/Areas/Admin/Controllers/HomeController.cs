using Microsoft.AspNetCore.Mvc;

namespace GoodGameDatabase.Web.Areas.Admin.Controllers
{
    public class HomeController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
