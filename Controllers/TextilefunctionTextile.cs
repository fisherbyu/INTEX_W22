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
    public class TextilefunctionTextile : Controller
    {
        private readonly egyptbyuContext _context;

        public TextilefunctionTextile(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: TextilefunctionTextile
        public async Task<IActionResult> Index()
        {
              return _context.TextilefunctionTextiles != null ? 
                          View(await _context.TextilefunctionTextiles.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.TextilefunctionTextiles'  is null.");
        }

        // GET: TextilefunctionTextile/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TextilefunctionTextiles == null)
            {
                return NotFound();
            }

            var textilefunctionTextile = await _context.TextilefunctionTextiles
                .FirstOrDefaultAsync(m => m.MainTextilefunctionid == id);
            if (textilefunctionTextile == null)
            {
                return NotFound();
            }

            return View(textilefunctionTextile);
        }

        // GET: TextilefunctionTextile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TextilefunctionTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainTextilefunctionid,MainTextileid")] TextilefunctionTextile textilefunctionTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(textilefunctionTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(textilefunctionTextile);
        }

        // GET: TextilefunctionTextile/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TextilefunctionTextiles == null)
            {
                return NotFound();
            }

            var textilefunctionTextile = await _context.TextilefunctionTextiles.FindAsync(id);
            if (textilefunctionTextile == null)
            {
                return NotFound();
            }
            return View(textilefunctionTextile);
        }

        // POST: TextilefunctionTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainTextilefunctionid,MainTextileid")] BYU_EGYPT_INTEX.Models.TextilefunctionTextile textilefunctionTextile)
        {
            if (id != textilefunctionTextile.MainTextilefunctionid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(textilefunctionTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TextilefunctionTextileExists(textilefunctionTextile.MainTextilefunctionid))
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
            return View(textilefunctionTextile);
        }

        // GET: TextilefunctionTextile/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TextilefunctionTextiles == null)
            {
                return NotFound();
            }

            var textilefunctionTextile = await _context.TextilefunctionTextiles
                .FirstOrDefaultAsync(m => m.MainTextilefunctionid == id);
            if (textilefunctionTextile == null)
            {
                return NotFound();
            }

            return View(textilefunctionTextile);
        }

        // POST: TextilefunctionTextile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TextilefunctionTextiles == null)
            {
                return Problem("Entity set 'egyptbyuContext.TextilefunctionTextiles'  is null.");
            }
            var textilefunctionTextile = await _context.TextilefunctionTextiles.FindAsync(id);
            if (textilefunctionTextile != null)
            {
                _context.TextilefunctionTextiles.Remove(textilefunctionTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TextilefunctionTextileExists(long id)
        {
          return (_context.TextilefunctionTextiles?.Any(e => e.MainTextilefunctionid == id)).GetValueOrDefault();
        }
    }
}
