using System.Diagnostics;
using Asp.netMVCQLBanVeXeKhach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Asp.netMVCQLBanVeXeKhach.Controllers
{
    public class HomeController : Controller
    {
        private readonly QLDatVeContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(QLDatVeContext context, ILogger<HomeController> logger)
        {
            db = context;
            _logger = logger;
        }

        public IActionResult Index(string diemDi = "", string diemDen = "", DateTime? ngayDi = null)
        {

            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;
            ViewData["Title"] = "Trang Chủ";

            var tgian = DateTime.Now;
            ViewBag.DanhSachDiemDi = db.TuyenXes.Select(t => t.DiemDi).Distinct().ToList();
            ViewBag.DanhSachDiemDen = db.TuyenXes.Select(t => t.DiemDen).Distinct().ToList();
            ViewBag.DiemDi = diemDi;
            ViewBag.DiemDen = diemDen;
            ViewBag.NgayDi = ngayDi;

            if (string.IsNullOrEmpty(diemDi) || string.IsNullOrEmpty(diemDen) || ngayDi == null)
            {
                ViewBag.ChuyenXes = new List<ChuyenXe>();
                return View();
            }

            var tuyenXe = db.TuyenXes
                .Include(t => t.MaBenXeNavigation)
                .Include(t => t.MaBenXeDenNavigation)
                .FirstOrDefault(t => t.DiemDi == diemDi && t.DiemDen == diemDen);
            
            if (tuyenXe == null)
            {
                ViewBag.Message = "Không tìm thấy tuyến xe phù hợp.";
                ViewBag.ChuyenXes = new List<ChuyenXe>();
                return View();
            }

            var chuyenXes = db.ChuyenXes
                .Include(cx => cx.BienSoXeNavigation)
                .ThenInclude(x => x.IdLoaiNavigation)
                .Include(cx => cx.VeXes)
                .Where(cx => cx.MaTuyen == tuyenXe.MaTuyen &&
                             cx.ThoiDiemKhoiHanh.HasValue &&
                             cx.ThoiDiemKhoiHanh.Value.Date == ngayDi.Value.Date&& cx.ThoiDiemKhoiHanh>=tgian)
                .ToList();


            if (chuyenXes.Count == 0)
            {
                ViewBag.Message = "Không có chuyến xe vào ngày đã chọn.";
            }

            ViewBag.TuyenXe = tuyenXe;
            ViewBag.ChuyenXes = chuyenXes;

            return View();
        }

        public IActionResult SelectSeat(int maChuyen)
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;
            ViewData["Title"] = "Chọn ghế";

    
            var chuyenXe = db.ChuyenXes
                .Include(cx => cx.BienSoXeNavigation)
                .ThenInclude(x => x.IdLoaiNavigation)
                .FirstOrDefault(cx => cx.MaChuyen == maChuyen);

            if (chuyenXe == null || chuyenXe.BienSoXeNavigation?.IdLoaiNavigation == null)
            {
                TempData["Error"] = "Chuyến xe hoặc loại xe không tồn tại.";
                return RedirectToAction("Index");
            }

          
            var loaixe = chuyenXe.BienSoXeNavigation.IdLoaiNavigation;
            var soGhe = loaixe.Soghe;

           
            var danhSachGhe = db.Vitrighes
                .Where(ghe => ghe.Bienso == chuyenXe.BienSoXe)
                .Select(ghe => new
                {
                    ghe.IdVitri,
                    ghe.Tenvitri,
                    ghe.Trangthai
                })
                .ToList();
      
          
            ViewBag.MaChuyen = maChuyen;
            ViewBag.ChuyenXe = chuyenXe;
            ViewBag.SoGhe = soGhe; 
            ViewBag.GheDaDat = danhSachGhe.Where(ghe => ghe.Trangthai == true).Select(ghe => ghe.IdVitri).ToList(); 
            ViewBag.DanhSachGhe = danhSachGhe;

            return View("SelectSeat");
        }


        //Gửi danh sách ID ghế đã chọn về controller dưới dạng List<int>.
        [HttpPost]
        public IActionResult BookTickets(int maChuyen, string selectedSeats, string tenKhachHang, string soDienThoai, string email, string ghiChu)
        {

            var seatIds = selectedSeats.Split(',').Select(int.Parse).ToList(); // Chuyển thành danh sách ID ghế

        
            var vitri = db.Vitrighes
                .Where(g => seatIds.Contains(g.IdVitri) && g.Trangthai != true)
                .ToList();

            if (vitri.Count != seatIds.Count)
            {
                TempData["Error"] = "Một hoặc nhiều ghế bạn chọn đã được đặt trước.";
                return RedirectToAction("SelectSeat", new { maChuyen = maChuyen });
            }

            
            var phieuDatVe = db.PhieuDatVes.FirstOrDefault(p => p.Email == email && p.TrangThai == "Chưa xác nhận");
            if (phieuDatVe == null)
            {
                phieuDatVe = new PhieuDatVe
                {
                    Email = email,
                    NgayDat = DateTime.Now,
                    TongTien = 0,
                    TrangThai = "Đã đặt"
                };
                db.PhieuDatVes.Add(phieuDatVe);
                db.SaveChanges();
            }

           
            foreach (var seat in vitri)
            {
                var veXe = new VeXe
                {
                    MaPhieu = phieuDatVe.MaPhieu,
                    MaChuyen = maChuyen,
                    IdVitri = seat.IdVitri,
                    TenKh = tenKhachHang,
                    Email = email,
                    GhiChu = ghiChu,
                    TenVe = db.ChuyenXes.FirstOrDefault(cx => cx.MaChuyen == maChuyen)?.TenChuyenXe,
                    TrangThai = "Chưa thanh toán"
                };
                db.VeXes.Add(veXe);

              
                seat.Trangthai = true;
            }

            var giaVe = db.ChuyenXes.FirstOrDefault(cx => cx.MaChuyen == maChuyen)?.GiaVe ?? 0;
            phieuDatVe.TongTien += vitri.Count * giaVe;

            db.SaveChanges();

            TempData["ShowModal"] = true;
            return RedirectToAction("SelectSeat", new { maChuyen = maChuyen });
        }

        public IActionResult ThongTinVe()
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;

           
            string? userEmail = HttpContext.Session.GetString("UserInfo");

            if (userEmail == null)
            {
                TempData["Error"] = "Vui lòng đăng nhập để xem thông tin đặt vé.";
                return RedirectToAction("Login", "Account");
            }
            var user = JsonConvert.DeserializeObject<NguoiDung>(userEmail);
            string email = user?.Email ?? "";

           
            var bookings = db.PhieuDatVes
                .Where(p => p.Email == email)
                .OrderByDescending(p => p.NgayDat)
                .ToList();

            return View(bookings);
        }
        public IActionResult Details(int id)
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;

            // Kiểm tra Session
            string? userInfo = HttpContext.Session.GetString("UserInfo");

            if (userInfo == null)
            {
                TempData["Error"] = "Vui lòng đăng nhập để xem chi tiết phiếu đặt vé.";
                return RedirectToAction("Login", "Account");
            }

           
            var booking = db.PhieuDatVes
                .Include(p => p.VeXes) 
                .FirstOrDefault(p => p.MaPhieu == id);

            if (booking == null)
            {
                TempData["Error"] = "Phiếu đặt vé không tồn tại.";
                return RedirectToAction("ChiTietVe");
            }

            return View(booking);
        }
        public IActionResult KhuyenMai1()
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;

            

            return View();
        }
        public IActionResult KhuyenMai2()
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;

            return View();
        }
        public IActionResult KhuyenMai3()
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;

            return View();
        }
        public IActionResult Sale12_12()
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;

            return View();
        }
        public IActionResult SanVe2025()
        {
            var userName = HttpContext.Session.GetString("UserName");
            ViewData["UserName"] = userName;

            return View();
        }
    }
}

