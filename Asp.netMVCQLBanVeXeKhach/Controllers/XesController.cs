using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Asp.netMVCQLBanVeXeKhach.Models;

namespace Asp.netMVCQLBanVeXeKhach.Controllers
{
    public class XesController : Controller
    {
        private readonly QLDatVeContext _context;

        public XesController(QLDatVeContext context)
        {
            _context = context;
        }

        // GET: Xes
        public async Task<IActionResult> Index()
        {
            var qLDatVeContext = _context.Xes.Include(x => x.IdLoaiNavigation);
            return View(await qLDatVeContext.ToListAsync());
        }

        // GET: Xes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes
                .Include(x => x.IdLoaiNavigation)
                .FirstOrDefaultAsync(m => m.Bienso == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // GET: Xes/Create
        public IActionResult Create()
        {
            ViewData["IdLoai"] = new SelectList(_context.Loaixes, "IdLoai", "IdLoai");
            return View();
        }

        // POST: Xes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bienso,IdLoai,Tenxe,HinhAnh")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(xe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLoai"] = new SelectList(_context.Loaixes, "IdLoai", "IdLoai", xe.IdLoai);
            return View(xe);
        }

        // GET: Xes/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            ViewData["IdLoai"] = new SelectList(_context.Loaixes, "IdLoai", "IdLoai", xe.IdLoai);
            return View(xe);
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Bienso,IdLoai,Tenxe,HinhAnh")] Xe xe)
        {
            if (id != xe.Bienso)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(xe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.Bienso))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdLoai"] = new SelectList(_context.Loaixes, "IdLoai", "IdLoai", xe.IdLoai);
            return View(xe);
        }

        // GET: Xes/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Xes == null)
            {
                return NotFound();
            }

            var xe = await _context.Xes
                .Include(x => x.IdLoaiNavigation)
                .FirstOrDefaultAsync(m => m.Bienso == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Xes == null)
            {
                return Problem("Entity set 'QLDatVeContext.Xes'  is null.");
            }
            var xe = await _context.Xes.FindAsync(id);
            if (xe != null)
            {
                _context.Xes.Remove(xe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(string id)
        {
          return (_context.Xes?.Any(e => e.Bienso == id)).GetValueOrDefault();
        }
    }
}
