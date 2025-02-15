using Microsoft.AspNetCore.Mvc;

namespace Asp.netMVCQLBanVeXeKhach.Controllers
{
    public class HomeAdminController : Controller
    {   
        public IActionResult Index()
        {
            return View();
        }
    }
}
