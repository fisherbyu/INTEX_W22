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
    public class Color : Controller
    {
        private readonly egyptbyuContext _context;

        public Color(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: Color
        public async Task<IActionResult> Index()
        {
              return _context.Colors != null ? 
                          View(await _context.Colors.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.Colors'  is null.");
        }

        // GET: Color/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // GET: Color/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Color/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Colorid")] Color color)
        {
            if (ModelState.IsValid)
            {
                _context.Add(color);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(color);
        }

        // GET: Color/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors.FindAsync(id);
            if (color == null)
            {
                return NotFound();
            }
            return View(color);
        }

        // POST: Color/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Value,Colorid")] Models.Color color)
        {
            if (id != color.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(color);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ColorExists(color.Id))
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
            return View(color);
        }

        // GET: Color/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Colors == null)
            {
                return NotFound();
            }

            var color = await _context.Colors
                .FirstOrDefaultAsync(m => m.Id == id);
            if (color == null)
            {
                return NotFound();
            }

            return View(color);
        }

        // POST: Color/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Colors == null)
            {
                return Problem("Entity set 'egyptbyuContext.Colors'  is null.");
            }
            var color = await _context.Colors.FindAsync(id);
            if (color != null)
            {
                _context.Colors.Remove(color);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ColorExists(long id)
        {
          return (_context.Colors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
