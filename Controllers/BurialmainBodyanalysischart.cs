using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BYU_EGYPT_INTEX.Models;
using Microsoft.AspNetCore.Authorization;

namespace BYU_EGYPT_INTEX.Controllers
{
    [Authorize(Roles = "Administrator, User")]
    public class BurialmainBodyanalysischart : Controller
    {
        private readonly egyptbyuContext _context;

        public BurialmainBodyanalysischart(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: BurialmainBodyanalysischart
        public async Task<IActionResult> Index()
        {
              return _context.BurialmainBodyanalysischarts != null ? 
                          View(await _context.BurialmainBodyanalysischarts.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.BurialmainBodyanalysischarts'  is null.");
        }

        // GET: BurialmainBodyanalysischart/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.BurialmainBodyanalysischarts == null)
            {
                return NotFound();
            }

            var burialmainBodyanalysischart = await _context.BurialmainBodyanalysischarts
                .FirstOrDefaultAsync(m => m.MainBurialmainid == id);
            if (burialmainBodyanalysischart == null)
            {
                return NotFound();
            }

            return View(burialmainBodyanalysischart);
        }

        // GET: BurialmainBodyanalysischart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BurialmainBodyanalysischart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainBurialmainid,MainBodyanalysischartid")] BurialmainBodyanalysischart burialmainBodyanalysischart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burialmainBodyanalysischart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(burialmainBodyanalysischart);
        }

        // GET: BurialmainBodyanalysischart/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.BurialmainBodyanalysischarts == null)
            {
                return NotFound();
            }

            var burialmainBodyanalysischart = await _context.BurialmainBodyanalysischarts.FindAsync(id);
            if (burialmainBodyanalysischart == null)
            {
                return NotFound();
            }
            return View(burialmainBodyanalysischart);
        }

        // POST: BurialmainBodyanalysischart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainBurialmainid,MainBodyanalysischartid")] Models.BurialmainBodyanalysischart burialmainBodyanalysischart)
        {
            if (id != burialmainBodyanalysischart.MainBurialmainid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burialmainBodyanalysischart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BurialmainBodyanalysischartExists(burialmainBodyanalysischart.MainBurialmainid))
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
            return View(burialmainBodyanalysischart);
        }

        // GET: BurialmainBodyanalysischart/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.BurialmainBodyanalysischarts == null)
            {
                return NotFound();
            }

            var burialmainBodyanalysischart = await _context.BurialmainBodyanalysischarts
                .FirstOrDefaultAsync(m => m.MainBurialmainid == id);
            if (burialmainBodyanalysischart == null)
            {
                return NotFound();
            }

            return View(burialmainBodyanalysischart);
        }

        // POST: BurialmainBodyanalysischart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.BurialmainBodyanalysischarts == null)
            {
                return Problem("Entity set 'egyptbyuContext.BurialmainBodyanalysischarts'  is null.");
            }
            var burialmainBodyanalysischart = await _context.BurialmainBodyanalysischarts.FindAsync(id);
            if (burialmainBodyanalysischart != null)
            {
                _context.BurialmainBodyanalysischarts.Remove(burialmainBodyanalysischart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BurialmainBodyanalysischartExists(long id)
        {
          return (_context.BurialmainBodyanalysischarts?.Any(e => e.MainBurialmainid == id)).GetValueOrDefault();
        }
    }
}
