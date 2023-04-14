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
    public class ColorTextile : Controller
    {
        private readonly egyptbyuContext _context;

        public ColorTextile(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: ColorTextile
        public async Task<IActionResult> Index()
        {
              return _context.ColorTextiles != null ? 
                          View(await _context.ColorTextiles.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.ColorTextiles'  is null.");
        }

        // GET: ColorTextile/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.ColorTextiles == null)
            {
                return NotFound();
            }

            var colorTextile = await _context.ColorTextiles
                .FirstOrDefaultAsync(m => m.MainColorid == id);
            if (colorTextile == null)
            {
                return NotFound();
            }

            return View(colorTextile);
        }

        // GET: ColorTextile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ColorTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainColorid,MainTextileid")] ColorTextile colorTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(colorTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(colorTextile);
        }

        // GET: ColorTextile/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.ColorTextiles == null)
            {
                return NotFound();
            }

            var colorTextile = await _context.ColorTextiles.FindAsync(id);
            if (colorTextile == null)
            {
                return NotFound();
            }
            return View(colorTextile);
        }

        // POST: ColorTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainColorid,MainTextileid")] BYU_EGYPT_INTEX.Models.ColorTextile colorTextile)
        {
            if (id != colorTextile.MainColorid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(colorTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorTextileExists(colorTextile.MainColorid))
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
            return View(colorTextile);
        }

        // GET: ColorTextile/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.ColorTextiles == null)
            {
                return NotFound();
            }

            var colorTextile = await _context.ColorTextiles
                .FirstOrDefaultAsync(m => m.MainColorid == id);
            if (colorTextile == null)
            {
                return NotFound();
            }

            return View(colorTextile);
        }

        // POST: ColorTextile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.ColorTextiles == null)
            {
                return Problem("Entity set 'egyptbyuContext.ColorTextiles'  is null.");
            }
            var colorTextile = await _context.ColorTextiles.FindAsync(id);
            if (colorTextile != null)
            {
                _context.ColorTextiles.Remove(colorTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorTextileExists(long id)
        {
          return (_context.ColorTextiles?.Any(e => e.MainColorid == id)).GetValueOrDefault();
        }
    }
}
