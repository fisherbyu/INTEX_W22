using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BYU_EGYPT_INTEX.Models;

namespace BYU_EGYPT_INTEX.Controllers
{
    public class BurialmainController : Controller
    {
        private readonly egyptbyuContext _context;

        public BurialmainController(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: Burialmain
        public async Task<IActionResult> Index()
        {
              return _context.Burialmains != null ? 
                          View(await _context.Burialmains.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.Burialmains'  is null.");
        }

        // GET: Burialmain/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Burialmains == null)
            {
                return NotFound();
            }

            var burialmain = await _context.Burialmains
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burialmain == null)
            {
                return NotFound();
            }

            return View(burialmain);
        }

        // GET: Burialmain/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Burialmain/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Squarenorthsouth,Headdirection,Sex,Northsouth,Depth,Eastwest,Adultsubadult,Facebundles,Southtohead,Preservation,Fieldbookpage,Squareeastwest,Goods,Text,Wrapping,Haircolor,Westtohead,Samplescollected,Area,Burialid,Length,Burialnumber,Dataexpertinitials,Westtofeet,Ageatdeath,Southtofeet,Excavationrecorder,Photos,Hair,Burialmaterials,Dateofexcavation,Fieldbookexcavationyear,Clusternumber,Shaftnumber")] Burialmain burialmain)
        {
            if (ModelState.IsValid)
            {
                _context.Add(burialmain);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(burialmain);
        }

        // GET: Burialmain/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Burialmains == null)
            {
                return NotFound();
            }

            var burialmain = await _context.Burialmains.FindAsync(id);
            if (burialmain == null)
            {
                return NotFound();
            }
            return View(burialmain);
        }

        // POST: Burialmain/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Squarenorthsouth,Headdirection,Sex,Northsouth,Depth,Eastwest,Adultsubadult,Facebundles,Southtohead,Preservation,Fieldbookpage,Squareeastwest,Goods,Text,Wrapping,Haircolor,Westtohead,Samplescollected,Area,Burialid,Length,Burialnumber,Dataexpertinitials,Westtofeet,Ageatdeath,Southtofeet,Excavationrecorder,Photos,Hair,Burialmaterials,Dateofexcavation,Fieldbookexcavationyear,Clusternumber,Shaftnumber")] Burialmain burialmain)
        {
            if (id != burialmain.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(burialmain);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BurialmainExists(burialmain.Id))
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
            return View(burialmain);
        }

        // GET: Burialmain/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Burialmains == null)
            {
                return NotFound();
            }

            var burialmain = await _context.Burialmains
                .FirstOrDefaultAsync(m => m.Id == id);
            if (burialmain == null)
            {
                return NotFound();
            }

            return View(burialmain);
        }

        // POST: Burialmain/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Burialmains == null)
            {
                return Problem("Entity set 'egyptbyuContext.Burialmains'  is null.");
            }
            var burialmain = await _context.Burialmains.FindAsync(id);
            if (burialmain != null)
            {
                _context.Burialmains.Remove(burialmain);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BurialmainExists(long id)
        {
          return (_context.Burialmains?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
