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
    public class ResearchLabsController : Controller
    {
        private readonly ResearchLabContext _context;

        public ResearchLabsController(ResearchLabContext context)
        {
            _context = context;
        }

        // GET: ResearchLabs
        public async Task<IActionResult> Index()
        {
            var researchLabContext = _context.ResearchLab.Include(r => r.LabAssistantNavigation);
            return View(await researchLabContext.ToListAsync());
        }

        // GET: ResearchLabs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchLab = await _context.ResearchLab
                .Include(r => r.LabAssistantNavigation)
                .FirstOrDefaultAsync(m => m.Rlid == id);
            if (researchLab == null)
            {
                return NotFound();
            }

            return View(researchLab);
        }

        // GET: ResearchLabs/Create
        public IActionResult Create()
        {
            ViewData["LabAssistant"] = new SelectList(_context.AppUser.Where(u=>u.RoleId==DataHolder.LabAssistant), "AppUserId", "Email");
            return View();
        }

        // POST: ResearchLabs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Rlid,LabName,LabLoction,LabAssistant")] ResearchLab researchLab)
        {
            if (ModelState.IsValid)
            {
                _context.Add(researchLab);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LabAssistant"] = new SelectList(_context.AppUser.Where(u => u.RoleId == DataHolder.LabAssistant), "AppUserId", "Email", researchLab.LabAssistant);
            return View(researchLab);
        }

        // GET: ResearchLabs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchLab = await _context.ResearchLab.FindAsync(id);
            if (researchLab == null)
            {
                return NotFound();
            }
            ViewData["LabAssistant"] = new SelectList(_context.AppUser.Where(u => u.RoleId == DataHolder.LabAssistant), "AppUserId", "Email", researchLab.LabAssistant);
            return View(researchLab);
        }

        // POST: ResearchLabs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Rlid,LabName,LabLoction,LabAssistant")] ResearchLab researchLab)
        {
            if (id != researchLab.Rlid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(researchLab);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResearchLabExists(researchLab.Rlid))
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
            ViewData["LabAssistant"] = new SelectList(_context.AppUser.Where(u => u.RoleId == DataHolder.LabAssistant), "AppUserId", "Email", researchLab.LabAssistant);
            return View(researchLab);
        }

        // GET: ResearchLabs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var researchLab = await _context.ResearchLab
                .Include(r => r.LabAssistantNavigation)
                .FirstOrDefaultAsync(m => m.Rlid == id);
            if (researchLab == null)
            {
                return NotFound();
            }

            return View(researchLab);
        }

        // POST: ResearchLabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var researchLab = await _context.ResearchLab.FindAsync(id);
            _context.ResearchLab.Remove(researchLab);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResearchLabExists(int id)
        {
            return _context.ResearchLab.Any(e => e.Rlid == id);
        }
    }
}
