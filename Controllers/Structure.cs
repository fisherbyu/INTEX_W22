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
    public class Structure : Controller
    {
        private readonly egyptbyuContext _context;

        public Structure(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: Structure
        public async Task<IActionResult> Index()
        {
              return _context.Structures != null ? 
                          View(await _context.Structures.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.Structures'  is null.");
        }

        // GET: Structure/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Structures == null)
            {
                return NotFound();
            }

            var structure = await _context.Structures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (structure == null)
            {
                return NotFound();
            }

            return View(structure);
        }

        // GET: Structure/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Structure/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Value,Structureid")] Structure structure)
        {
            if (ModelState.IsValid)
            {
                _context.Add(structure);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(structure);
        }

        // GET: Structure/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Structures == null)
            {
                return NotFound();
            }

            var structure = await _context.Structures.FindAsync(id);
            if (structure == null)
            {
                return NotFound();
            }
            return View(structure);
        }

        // POST: Structure/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Value,Structureid")] Models.Structure structure)
        {
            if (id != structure.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(structure);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StructureExists(structure.Id))
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
            return View(structure);
        }

        // GET: Structure/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Structures == null)
            {
                return NotFound();
            }

            var structure = await _context.Structures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (structure == null)
            {
                return NotFound();
            }

            return View(structure);
        }

        // POST: Structure/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Structures == null)
            {
                return Problem("Entity set 'egyptbyuContext.Structures'  is null.");
            }
            var structure = await _context.Structures.FindAsync(id);
            if (structure != null)
            {
                _context.Structures.Remove(structure);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StructureExists(long id)
        {
          return (_context.Structures?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
