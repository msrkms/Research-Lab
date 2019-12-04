using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Research_Lab.Data;
using Research_Lab.Models;

namespace Research_Lab.Controllers
{
    public class LabCostRatesController : Controller
    {
        private readonly ResearchLabContext _context;

        public LabCostRatesController(ResearchLabContext context)
        {
            _context = context;
        }

        // GET: LabCostRates
        public async Task<IActionResult> Index()
        {
            var researchLabContext = _context.LabCostRates.Include(l => l.ResearchLab);
            return View(await researchLabContext.ToListAsync());
        }

        // GET: LabCostRates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labCostRate = await _context.LabCostRates
                .Include(l => l.ResearchLab)
                .FirstOrDefaultAsync(m => m.id == id);
            if (labCostRate == null)
            {
                return NotFound();
            }

            return View(labCostRate);
        }

        // GET: LabCostRates/Create
        public IActionResult Create()
        {
            ViewData["Rlid"] = new SelectList(_context.ResearchLab, "Rlid", "LabName");
            return View();
        }

        // POST: LabCostRates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,Rlid,costperminitue")] LabCostRate labCostRate)
        {
            if (ModelState.IsValid)
            {
                _context.Add(labCostRate);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Rlid"] = new SelectList(_context.ResearchLab, "Rlid", "LabName", labCostRate.Rlid);
            return View(labCostRate);
        }

        // GET: LabCostRates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labCostRate = await _context.LabCostRates.FindAsync(id);
            if (labCostRate == null)
            {
                return NotFound();
            }
            ViewData["Rlid"] = new SelectList(_context.ResearchLab, "Rlid", "LabName", labCostRate.Rlid);
            return View(labCostRate);
        }

        // POST: LabCostRates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,Rlid,costperminitue")] LabCostRate labCostRate)
        {
            if (id != labCostRate.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(labCostRate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabCostRateExists(labCostRate.id))
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
            ViewData["Rlid"] = new SelectList(_context.ResearchLab, "Rlid", "LabName", labCostRate.Rlid);
            return View(labCostRate);
        }

        // GET: LabCostRates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labCostRate = await _context.LabCostRates
                .Include(l => l.ResearchLab)
                .FirstOrDefaultAsync(m => m.id == id);
            if (labCostRate == null)
            {
                return NotFound();
            }

            return View(labCostRate);
        }

        // POST: LabCostRates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labCostRate = await _context.LabCostRates.FindAsync(id);
            _context.LabCostRates.Remove(labCostRate);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabCostRateExists(int id)
        {
            return _context.LabCostRates.Any(e => e.id == id);
        }
    }
}
