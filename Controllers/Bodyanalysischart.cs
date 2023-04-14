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
    public class Bodyanalysischart : Controller
    {
        private readonly egyptbyuContext _context;

        public Bodyanalysischart(egyptbyuContext context)
        {
            _context = context;
        }

        // GET: Bodyanalysischart
        public async Task<IActionResult> Index()
        {
              return _context.Bodyanalysischarts != null ? 
                          View(await _context.Bodyanalysischarts.ToListAsync()) :
                          Problem("Entity set 'egyptbyuContext.Bodyanalysischarts'  is null.");
        }

        // GET: Bodyanalysischart/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Bodyanalysischarts == null)
            {
                return NotFound();
            }

            var bodyanalysischart = await _context.Bodyanalysischarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bodyanalysischart == null)
            {
                return NotFound();
            }

            return View(bodyanalysischart);
        }

        // GET: Bodyanalysischart/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bodyanalysischart/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Femur,Humerusheaddiameter,Squamossuture,CariesPeriodontalDisease,MedicalIpRamus,Perservationindex,Gonion,Humeruslength,Femurlength,Lambdoidsuture,Ventralarc,Tootheruptionageestimation,Nuchalcrest,Estimatestature,Notes,Osteophytosis,Subpubicangle,Robust,Femurheaddiameter,Sciaticnotch,Supraorbitalridges,Orbitedge,Toothattrition,Sphenooccipitalsynchondrosis,Parietalblossing,Observations,Humerus")] Bodyanalysischart bodyanalysischart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bodyanalysischart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bodyanalysischart);
        }

        // GET: Bodyanalysischart/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Bodyanalysischarts == null)
            {
                return NotFound();
            }

            var bodyanalysischart = await _context.Bodyanalysischarts.FindAsync(id);
            if (bodyanalysischart == null)
            {
                return NotFound();
            }
            return View(bodyanalysischart);
        }

        // POST: Bodyanalysischart/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Femur,Humerusheaddiameter,Squamossuture,CariesPeriodontalDisease,MedicalIpRamus,Perservationindex,Gonion,Humeruslength,Femurlength,Lambdoidsuture,Ventralarc,Tootheruptionageestimation,Nuchalcrest,Estimatestature,Notes,Osteophytosis,Subpubicangle,Robust,Femurheaddiameter,Sciaticnotch,Supraorbitalridges,Orbitedge,Toothattrition,Sphenooccipitalsynchondrosis,Parietalblossing,Observations,Humerus")] Models.Bodyanalysischart bodyanalysischart)
        {
            if (id != bodyanalysischart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bodyanalysischart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BodyanalysischartExists(bodyanalysischart.Id))
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
            return View(bodyanalysischart);
        }

        // GET: Bodyanalysischart/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Bodyanalysischarts == null)
            {
                return NotFound();
            }

            var bodyanalysischart = await _context.Bodyanalysischarts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bodyanalysischart == null)
            {
                return NotFound();
            }

            return View(bodyanalysischart);
        }

        // POST: Bodyanalysischart/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Bodyanalysischarts == null)
            {
                return Problem("Entity set 'egyptbyuContext.Bodyanalysischarts'  is null.");
            }
            var bodyanalysischart = await _context.Bodyanalysischarts.FindAsync(id);
            if (bodyanalysischart != null)
            {
                _context.Bodyanalysischarts.Remove(bodyanalysischart);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BodyanalysischartExists(long id)
        {
          return (_context.Bodyanalysischarts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
