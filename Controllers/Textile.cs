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
    public class Textile : Controller
    {
        private readonly egyptbyuContext _context;

        public Textile(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: Textile
        public async Task<IActionResult> Index()
        {
              return _context.Textiles != null ? 
                          View(await _context.Textiles.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.Textiles'  is null.");
        }

        // GET: Textile/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Textiles == null)
            {
                return NotFound();
            }

            var textile = await _context.Textiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (textile == null)
            {
                return NotFound();
            }

            return View(textile);
        }

        // GET: Textile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Textile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Locale,Textileid,Description,Burialnumber,Estimatedperiod,Sampledate,Photographeddate,Direction")] Textile textile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(textile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(textile);
        }

        // GET: Textile/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Textiles == null)
            {
                return NotFound();
            }

            var textile = await _context.Textiles.FindAsync(id);
            if (textile == null)
            {
                return NotFound();
            }
            return View(textile);
        }

        // POST: Textile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Locale,Textileid,Description,Burialnumber,Estimatedperiod,Sampledate,Photographeddate,Direction")] BYU_EGYPT_INTEX.Models.Textile textile)
        {
            if (id != textile.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(textile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TextileExists(textile.Id))
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
            return View(textile);
        }

        // GET: Textile/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Textiles == null)
            {
                return NotFound();
            }

            var textile = await _context.Textiles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (textile == null)
            {
                return NotFound();
            }

            return View(textile);
        }

        // POST: Textile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Textiles == null)
            {
                return Problem("Entity set 'egyptbyuContext.Textiles'  is null.");
            }
            var textile = await _context.Textiles.FindAsync(id);
            if (textile != null)
            {
                _context.Textiles.Remove(textile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TextileExists(long id)
        {
          return (_context.Textiles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
