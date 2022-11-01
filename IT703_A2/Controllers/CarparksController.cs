using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using IT703_A2.Data;
using IT703_A2.Models;

namespace IT703_A2.Controllers
{
    [Authorize]
    public class CarparksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarparksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Carparks
        public async Task<IActionResult> Index()
        {
              return View(await _context.Carparks.ToListAsync());
        }

        // GET: Carparks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Carparks == null)
            {
                return NotFound();
            }

            var carpark = await _context.Carparks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carpark == null)
            {
                return NotFound();
            }

            return View(carpark);
        }

        // GET: Carparks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Carparks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IsAvailable,Block")] Carpark carpark)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carpark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carpark);
        }

        // GET: Carparks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Carparks == null)
            {
                return NotFound();
            }

            var carpark = await _context.Carparks.FindAsync(id);
            if (carpark == null)
            {
                return NotFound();
            }
            return View(carpark);
        }

        // POST: Carparks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,IsAvailable,Block")] Carpark carpark)
        {
            if (id != carpark.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carpark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarparkExists(carpark.Id))
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
            return View(carpark);
        }

        // GET: Carparks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Carparks == null)
            {
                return NotFound();
            }

            var carpark = await _context.Carparks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carpark == null)
            {
                return NotFound();
            }

            return View(carpark);
        }

        // POST: Carparks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Carparks == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Carparks'  is null.");
            }
            var carpark = await _context.Carparks.FindAsync(id);
            if (carpark != null)
            {
                _context.Carparks.Remove(carpark);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarparkExists(string id)
        {
          return _context.Carparks.Any(e => e.Id == id);
        }
    }
}
