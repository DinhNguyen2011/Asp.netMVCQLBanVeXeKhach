using Microsoft.AspNetCore.Mvc;

namespace Asp.netMVCQLBanVeXeKhach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
