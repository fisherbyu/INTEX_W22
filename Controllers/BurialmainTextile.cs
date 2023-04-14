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
    public class BurialmainTextile : Controller
    {
        private readonly egyptbyuContext _context;

        public BurialmainTextile(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: BurialmainTextile
        public async Task<IActionResult> Index()
        {
              return _context.BurialmainTextiles != null ? 
                          View(await _context.BurialmainTextiles.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.BurialmainTextiles'  is null.");
        }

        // GET: BurialmainTextile/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.BurialmainTextiles == null)
            {
                return NotFound();
            }

            var burialmainTextile = await _context.BurialmainTextiles
                .FirstOrDefaultAsync(m => m.MainBurialmainid == id);
            if (burialmainTextile == null)
            {
                return NotFound();
            }

            return View(burialmainTextile);
        }

        // GET: BurialmainTextile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BurialmainTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainBurialmainid,MainTextileid")] BurialmainTextile burialmainTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burialmainTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(burialmainTextile);
        }

        // GET: BurialmainTextile/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.BurialmainTextiles == null)
            {
                return NotFound();
            }

            var burialmainTextile = await _context.BurialmainTextiles.FindAsync(id);
            if (burialmainTextile == null)
            {
                return NotFound();
            }
            return View(burialmainTextile);
        }

        // POST: BurialmainTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainBurialmainid,MainTextileid")] BYU_EGYPT_INTEX.Models.BurialmainTextile burialmainTextile)
        {
            if (id != burialmainTextile.MainBurialmainid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burialmainTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BurialmainTextileExists(burialmainTextile.MainBurialmainid))
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
            return View(burialmainTextile);
        }

        // GET: BurialmainTextile/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.BurialmainTextiles == null)
            {
                return NotFound();
            }

            var burialmainTextile = await _context.BurialmainTextiles
                .FirstOrDefaultAsync(m => m.MainBurialmainid == id);
            if (burialmainTextile == null)
            {
                return NotFound();
            }

            return View(burialmainTextile);
        }

        // POST: BurialmainTextile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.BurialmainTextiles == null)
            {
                return Problem("Entity set 'egyptbyuContext.BurialmainTextiles'  is null.");
            }
            var burialmainTextile = await _context.BurialmainTextiles.FindAsync(id);
            if (burialmainTextile != null)
            {
                _context.BurialmainTextiles.Remove(burialmainTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BurialmainTextileExists(long id)
        {
          return (_context.BurialmainTextiles?.Any(e => e.MainBurialmainid == id)).GetValueOrDefault();
        }
    }
}
