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
    public class ComputersController : Controller
    {
        private readonly ResearchLabContext _context;

        public ComputersController(ResearchLabContext context)
        {
            _context = context;
        }

        // GET: Computers
        public async Task<IActionResult> Index()
        {
            var researchLabContext = _context.Computer.Include(c => c.Lab);
            return View(await researchLabContext.ToListAsync());
        }


        public IActionResult SelectLab()
        {
            ViewData["Rlid"] = new SelectList(_context.ResearchLab, "Rlid", "LabName");
            return View();
        }

        [HttpPost]
        public IActionResult SelectLab([Bind("Rlid,LabName,LabLoction,LabAssistant")] ResearchLab researchLab)
        {

            if (researchLab.Rlid == 0)
            {
                return RedirectToAction(nameof(SelectLab));
            }
            else
            {
                return Redirect("/Computers/LabAssitantComputer/" + researchLab.Rlid);
            }


        }

        public async Task<IActionResult> LabAssitantComputer(int? id )
        {
            if (id == null)
            {
                return RedirectToAction(nameof(SelectLab));
            }
            var computerContext = _context.Computer.Where(c=>c.LabId==id).Include(c=>c.Lab);
            return View(await computerContext.ToListAsync());
        }


        public async Task<IActionResult> EditbyLabAsistant(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            var com = await  _context.Computer
                .Include(c => c.Lab)
                .FirstOrDefaultAsync(m => m.Cid == id);

            ViewData["LabId"] = new SelectList(_context.ResearchLab.Where(rl=>rl.Rlid==com.LabId), "Rlid", "LabName", computer.LabId);
            return View(computer);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditbyLabAsistant(int id, [Bind("Cid,IsAvailable,LabId")] Computer computer)
        {
            if (id != computer.Cid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.Cid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SelectLab));
            }
            return Redirect("/AppUsers/LabAssistant/");
        }





        // GET: Computers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .Include(c => c.Lab)
                .FirstOrDefaultAsync(m => m.Cid == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // GET: Computers/Create
        public IActionResult Create()
        {
            ViewData["LabId"] = new SelectList(_context.ResearchLab, "Rlid", "LabName");
            return View();
        }

        // POST: Computers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Cid,IsAvailable,LabId")] Computer computer,String create,String create5)
        {
            if (!String.IsNullOrEmpty(create5))
            {
                int isAvailable = 0;
                if (computer.IsAvailable)
                {
                    isAvailable = 1;
                }
                _context.Database.ExecuteSqlCommand("InsertFiveComputer @LabId={0}, @IsAvailable={1}", computer.LabId, isAvailable);

                return RedirectToAction(nameof(Index));

            } else if(!String.IsNullOrEmpty(create)){
                if (ModelState.IsValid)
                {
                    _context.Add(computer);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }

            }
            ViewData["LabId"] = new SelectList(_context.ResearchLab, "Rlid", "LabName", computer.LabId);
            return View(computer);
        }

        // GET: Computers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer.FindAsync(id);
            if (computer == null)
            {
                return NotFound();
            }
            ViewData["LabId"] = new SelectList(_context.ResearchLab, "Rlid", "LabName", computer.LabId);
            return View(computer);
        }

        // POST: Computers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Cid,IsAvailable,LabId")] Computer computer)
        {
            if (id != computer.Cid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(computer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComputerExists(computer.Cid))
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
            ViewData["LabId"] = new SelectList(_context.ResearchLab, "Rlid", "LabName", computer.LabId);
            return View(computer);
        }

        // GET: Computers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var computer = await _context.Computer
                .Include(c => c.Lab)
                .FirstOrDefaultAsync(m => m.Cid == id);
            if (computer == null)
            {
                return NotFound();
            }

            return View(computer);
        }

        // POST: Computers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var computer = await _context.Computer.FindAsync(id);
            _context.Computer.Remove(computer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComputerExists(int id)
        {
            return _context.Computer.Any(e => e.Cid == id);
        }
    }
}
