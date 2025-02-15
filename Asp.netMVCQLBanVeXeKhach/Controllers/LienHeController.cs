using Microsoft.AspNetCore.Mvc;

namespace Asp.netMVCQLBanVeXeKhach.Controllers
{
    public class LienHeController : Controller
    {
        public IActionResult Index()
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;
            return View();
        }

        [HttpPost]
        public IActionResult SubmitContact(string name, string email, string phone, string subject, string message)
        {
            
            ViewBag.Message = "Thông tin liên hệ đã được gửi thành công!";
            return RedirectToAction("Index");
        }
    }
}