using Asp.netMVCQLBanVeXeKhach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Asp.netMVCQLBanVeXeKhach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TuyenXeController : Controller
    {
        private readonly QLDatVeContext _context;

        public TuyenXeController(QLDatVeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var model = _context.TuyenXes
                .Include(t => t.MaBenXeNavigation)
                .Include(t => t.MaBenXeDenNavigation)
                .ToList();
            return View(model);
        }
        public IActionResult them()
        {
            ViewBag.BenXes = new SelectList(_context.BenXes, "MaBenXe", "TenBenXe");
            ViewBag.BenXeDens = new SelectList(_context.BenXeDens, "MaBenXeDen", "TenBenXeDen");
            return View();
        }

        [HttpPost]
        public IActionResult them(TuyenXe tuyenXe)
        {
            if (ModelState.IsValid)
            {
                _context.TuyenXes.Add(tuyenXe);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

         
            ViewBag.BenXes = new SelectList(_context.BenXes, "MaBenXe", "TenBenXe");
            ViewBag.BenXeDens = new SelectList(_context.BenXeDens, "MaBenXeDen", "TenBenXeDen");
            return View(tuyenXe);
        }

     
        public IActionResult Edit(int id)
        {
            var tuyenXe = _context.TuyenXes.Find(id);
            if (tuyenXe == null)
            {
                return NotFound();
            }

            // Lấy danh sách bến xe và bến xe đến
            ViewBag.BenXes = new SelectList(_context.BenXes, "MaBenXe", "TenBenXe", tuyenXe.MaBenXe);
            ViewBag.BenXeDens = new SelectList(_context.BenXes, "MaBenXe", "TenBenXe", tuyenXe.MaBenXeDen);
            return View(tuyenXe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, TuyenXe tuyenXe)
        {
            if (id != tuyenXe.MaTuyen)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tuyenXe);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.TuyenXes.Any(e => e.MaTuyen == id))
                    {
                        return NotFound();
                    }
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            // Truyền lại danh sách nếu có lỗi
            ViewBag.BenXes = new SelectList(_context.BenXes, "MaBenXe", "TenBenXe", tuyenXe.MaBenXe);
            ViewBag.BenXeDens = new SelectList(_context.BenXes, "MaBenXe", "TenBenXe", tuyenXe.MaBenXeDen);
            return View(tuyenXe);
        }

        // Xóa tuyến xe
        public IActionResult Delete(int id)
        {
            var tuyenXe = _context.TuyenXes.Find(id);
            if (tuyenXe != null)
            {
                _context.TuyenXes.Remove(tuyenXe);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
