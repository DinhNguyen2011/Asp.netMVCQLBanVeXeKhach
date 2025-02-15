using Asp.netMVCQLBanVeXeKhach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Asp.netMVCQLBanVeXeKhach.Controllers
{
    public class ViTriGheController : Controller
    {
        private readonly QLDatVeContext _context;

        public ViTriGheController(QLDatVeContext context)
        {
            _context = context;
        }

        // GET: ViTriGhe
        public IActionResult Index()
        {
            var vitriGhes = _context.Vitrighes.Include(v => v.BiensoNavigation).ToList();
            return View(vitriGhes);
        }

        public IActionResult Details(int id)
        {
            var vitriGhe = _context.Vitrighes
                .Include(v => v.BiensoNavigation)
                .FirstOrDefault(v => v.IdVitri == id);

            if (vitriGhe == null)
            {
                return NotFound();
            }

            return View(vitriGhe);
        }


        public IActionResult Create()
        {
                 ViewData["BiensoList"] = new SelectList(_context.Xes, "Bienso", "Bienso");

            return View();
        }

        // POST: ViTriGhe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("IdVitri,Bienso,Tenvitri,Trangthai")] Vitrighe vitriGhe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vitriGhe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            ViewData["BiensoList"] = new SelectList(_context.Xes, "Bienso", "Bienso", vitriGhe.Bienso);
            return View(vitriGhe);
        }


    
        //vị trí ghế k có sửa bởi vì vị trí ghế hoạt động dựa vào đặt vé ở chuyến xe
       
        

 
        public IActionResult Delete(int id)
        {
            var vitriGhe = _context.Vitrighes.Find(id);
            if (vitriGhe == null)
            {
                return NotFound();
            }

            return View(vitriGhe);
        }

 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var vitriGhe = _context.Vitrighes.Find(id);
            if (vitriGhe != null)
            {
                _context.Vitrighes.Remove(vitriGhe);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VitrigheExists(int id)
        {
            return _context.Vitrighes.Any(e => e.IdVitri == id);
        }
    }
}
