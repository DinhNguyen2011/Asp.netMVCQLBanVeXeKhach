using Asp.netMVCQLBanVeXeKhach.Models;
using Microsoft.AspNetCore.Mvc;

namespace Asp.netMVCQLBanVeXeKhach.Controllers
{
    public class ThongKeController : Controller
    {
        private QLDatVeContext db = new QLDatVeContext();
        [HttpGet]
        public IActionResult ThongKeDoanhThu(DateTime? startDate, DateTime? endDate)
        {
       
            var phieuDatVes = db.PhieuDatVes.AsQueryable();

  
            if (startDate.HasValue)
            {
                phieuDatVes = phieuDatVes.Where(p => p.NgayDat >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                phieuDatVes = phieuDatVes.Where(p => p.NgayDat <= endDate.Value);
            }

         
            var tongDoanhThu = phieuDatVes.Sum(p => p.TongTien) ?? 0;

          
            ViewBag.TongDoanhThu = tongDoanhThu;
            ViewBag.StartDate = startDate?.ToString("yyyy-MM-dd");
            ViewBag.EndDate = endDate?.ToString("yyyy-MM-dd");

            return View(phieuDatVes.ToList());
        }

    }
}
