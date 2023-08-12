using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using static GoodGameDatabase.Web.Areas.Admin.AdminConstants;

namespace GoodGameDatabase.Web.Areas.Admin.Controllers
{
    [Area(AdminAreaName)]
    [Authorize(Policy = AdminPolicyName)]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
