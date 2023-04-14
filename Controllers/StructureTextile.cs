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
    public class StructureTextile : Controller
    {
        private readonly egyptbyuContext _context;

        public StructureTextile(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: StructureTextile
        public async Task<IActionResult> Index()
        {
              return _context.StructureTextiles != null ? 
                          View(await _context.StructureTextiles.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.StructureTextiles'  is null.");
        }

        // GET: StructureTextile/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.StructureTextiles == null)
            {
                return NotFound();
            }

            var structureTextile = await _context.StructureTextiles
                .FirstOrDefaultAsync(m => m.MainStructureid == id);
            if (structureTextile == null)
            {
                return NotFound();
            }

            return View(structureTextile);
        }

        // GET: StructureTextile/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: StructureTextile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MainStructureid,MainTextileid")] StructureTextile structureTextile)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structureTextile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(structureTextile);
        }

        // GET: StructureTextile/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.StructureTextiles == null)
            {
                return NotFound();
            }

            var structureTextile = await _context.StructureTextiles.FindAsync(id);
            if (structureTextile == null)
            {
                return NotFound();
            }
            return View(structureTextile);
        }

        // POST: StructureTextile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MainStructureid,MainTextileid")] Models.StructureTextile structureTextile)
        {
            if (id != structureTextile.MainStructureid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structureTextile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructureTextileExists(structureTextile.MainStructureid))
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
            return View(structureTextile);
        }

        // GET: StructureTextile/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.StructureTextiles == null)
            {
                return NotFound();
            }

            var structureTextile = await _context.StructureTextiles
                .FirstOrDefaultAsync(m => m.MainStructureid == id);
            if (structureTextile == null)
            {
                return NotFound();
            }

            return View(structureTextile);
        }

        // POST: StructureTextile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.StructureTextiles == null)
            {
                return Problem("Entity set 'egyptbyuContext.StructureTextiles'  is null.");
            }
            var structureTextile = await _context.StructureTextiles.FindAsync(id);
            if (structureTextile != null)
            {
                _context.StructureTextiles.Remove(structureTextile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructureTextileExists(long id)
        {
          return (_context.StructureTextiles?.Any(e => e.MainStructureid == id)).GetValueOrDefault();
        }
    }
}
