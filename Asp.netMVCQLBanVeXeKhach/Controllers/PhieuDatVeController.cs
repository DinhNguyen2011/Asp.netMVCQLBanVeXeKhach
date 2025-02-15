using Asp.netMVCQLBanVeXeKhach.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Asp.netMVCQLBanVeXeKhach.Controllers
{
    public class PhieuDatVeController : Controller
    {
        private readonly QLDatVeContext _context;

        public PhieuDatVeController(QLDatVeContext context)
        {
            _context = context;
        }

   
        public IActionResult Index()
        {
            var phieuDatVeList = _context.PhieuDatVes.ToList();
            return View(phieuDatVeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PhieuDatVe phieuDatVe)
        {
            if (ModelState.IsValid)
            {
                _context.PhieuDatVes.Add(phieuDatVe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(phieuDatVe);
        }

      
        public IActionResult Edit(int id)
        {
            var phieuDatVe = _context.PhieuDatVes.Find(id);
            if (phieuDatVe == null)
            {
                return NotFound();
            }
            return View(phieuDatVe);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PhieuDatVe phieuDatVe)
        {
            if (id != phieuDatVe.MaPhieu)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Update(phieuDatVe);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(phieuDatVe);
        }

        
        public IActionResult Delete(int id)
        {
            var phieuDatVe = _context.PhieuDatVes.Find(id);
            if (phieuDatVe == null)
            {
                return NotFound();
            }
            return View(phieuDatVe);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var phieuDatVe = _context.PhieuDatVes.Find(id);
            if (phieuDatVe != null)
            {
                _context.PhieuDatVes.Remove(phieuDatVe);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
