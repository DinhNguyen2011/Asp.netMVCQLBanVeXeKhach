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
    public class VitrighesController : Controller
    {
        private readonly QLDatVeContext _context;

        public VitrighesController(QLDatVeContext context)
        {
            _context = context;
        }

        // GET: Vitrighes
        public async Task<IActionResult> Index()
        {
            var qLDatVeContext = _context.Vitrighes.Include(v => v.BiensoNavigation);
            return View(await qLDatVeContext.ToListAsync());
        }

        // GET: Vitrighes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Vitrighes == null)
            {
                return NotFound();
            }

            var vitrighe = await _context.Vitrighes
                .Include(v => v.BiensoNavigation)
                .FirstOrDefaultAsync(m => m.IdVitri == id);
            if (vitrighe == null)
            {
                return NotFound();
            }

            return View(vitrighe);
        }

        // GET: Vitrighes/Create
        public IActionResult Create()
        {
            ViewData["Bienso"] = new SelectList(_context.Xes, "Bienso", "Bienso");
            return View();
        }

        // POST: Vitrighes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVitri,Bienso,Tenvitri,Trangthai")] Vitrighe vitrighe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vitrighe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Bienso"] = new SelectList(_context.Xes, "Bienso", "Bienso", vitrighe.Bienso);
            return View(vitrighe);
        }

        // GET: Vitrighes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Vitrighes == null)
            {
                return NotFound();
            }

            var vitrighe = await _context.Vitrighes.FindAsync(id);
            if (vitrighe == null)
            {
                return NotFound();
            }
            ViewData["Bienso"] = new SelectList(_context.Xes, "Bienso", "Bienso", vitrighe.Bienso);
            return View(vitrighe);
        }

        // POST: Vitrighes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVitri,Bienso,Tenvitri,Trangthai")] Vitrighe vitrighe)
        {
            if (id != vitrighe.IdVitri)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vitrighe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VitrigheExists(vitrighe.IdVitri))
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
            ViewData["Bienso"] = new SelectList(_context.Xes, "Bienso", "Bienso", vitrighe.Bienso);
            return View(vitrighe);
        }

        // GET: Vitrighes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Vitrighes == null)
            {
                return NotFound();
            }

            var vitrighe = await _context.Vitrighes
                .Include(v => v.BiensoNavigation)
                .FirstOrDefaultAsync(m => m.IdVitri == id);
            if (vitrighe == null)
            {
                return NotFound();
            }

            return View(vitrighe);
        }

        // POST: Vitrighes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Vitrighes == null)
            {
                return Problem("Entity set 'QLDatVeContext.Vitrighes'  is null.");
            }
            var vitrighe = await _context.Vitrighes.FindAsync(id);
            if (vitrighe != null)
            {
                _context.Vitrighes.Remove(vitrighe);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VitrigheExists(int id)
        {
          return (_context.Vitrighes?.Any(e => e.IdVitri == id)).GetValueOrDefault();
        }
    }
}
