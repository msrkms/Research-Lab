using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Research_Lab.Data;
using Research_Lab.Models;

namespace Research_Lab.Controllers
{
    public class LabUseCostsController : Controller
    {
        private readonly ResearchLabContext _context;

        public LabUseCostsController(ResearchLabContext context)
        {
            _context = context;
        }

        // GET: LabUseCosts
        public async Task<IActionResult> Index()
        {
            var researchLabContext = _context.LabUseCosts.Include(l => l.Computer).Include(l => l.appUser);
            return View(await researchLabContext.ToListAsync());
        }

        public async Task<IActionResult> MyBill()
        {
            var researchLabContext = _context.LabUseCosts.Include(l => l.Computer).Include(l => l.appUser);
            return View(await researchLabContext.Where(luc=>luc.appUserID==DataHolder.appUser.AppUserId).ToListAsync());
        }

        // GET: LabUseCosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labUseCost = await _context.LabUseCosts
                .Include(l => l.Computer)
                .Include(l => l.appUser)
                .FirstOrDefaultAsync(m => m.id == id);
            if (labUseCost == null)
            {
                return NotFound();
            }

            return View(labUseCost);
        }

        // GET: LabUseCosts/Create
        public IActionResult Create()
        {
            ViewData["CId"] = new SelectList(_context.Computer, "Cid", "Cid");
            ViewData["appUserID"] = new SelectList(_context.AppUser, "AppUserId", "Email");
            return View();
        }

        // POST: LabUseCosts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,UseDate,hour,minute,totalCost,appUserID,CId")] LabUseCost labUseCost)
        {
            if (ModelState.IsValid)
            {
                var com = await _context.Computer.FindAsync(labUseCost.CId) ;
              //  var lab = await _context.ResearchLab.FindAsync(com.LabId);
                float costpermin = _context.LabCostRates.Where(r => r.Rlid ==com.LabId).FirstOrDefault().costperminitue;
                labUseCost.totalCost = costpermin * ((labUseCost.hour * 60) + labUseCost.minute);
                var date = new SqlParameter("@date", labUseCost.UseDate); 
                var hour = new SqlParameter("@hour", labUseCost.hour); 
                var min = new SqlParameter("@min", labUseCost.minute); 
                var total = new SqlParameter("@total", labUseCost.totalCost); 
                var uid = new SqlParameter("@uid", labUseCost.appUserID);
                var cid = new SqlParameter("@cid", labUseCost.CId); 
                _context.Database.ExecuteSqlCommand("InsertBill @date, @hour,@min,@total,@uid,@cid", date,hour,min,total,uid,cid );
                return RedirectToAction(nameof(Index));
            }
            ViewData["CId"] = new SelectList(_context.Computer, "Cid", "Cid", labUseCost.CId);
            ViewData["appUserID"] = new SelectList(_context.AppUser, "AppUserId", "Email", labUseCost.appUser.Name);
            return View(labUseCost);
        }





        public async Task<IActionResult> GiveDiscount(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labUseCost = await _context.LabUseCosts.FindAsync(id);
            if (labUseCost == null)
            {
                return NotFound();
            }
            ViewData["Cuponlist"]= new SelectList( new List<SelectListItem>{
                  new SelectListItem { Selected = true, Text = "None", Value = "None"},
                  new SelectListItem { Selected = false, Text = "Member", Value = "Member"},
                  new SelectListItem { Selected = false, Text = "Guest", Value = "Guest"},
                  new SelectListItem { Selected = false, Text = "Teacher", Value = "Teacher"},
                 }, "Value", "Text", 1);

            return View(labUseCost);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GiveDiscount(int id, [Bind("id,UseDate,hour,minute,totalCost,appUserID,CId")] LabUseCost labUseCost,String Cupon)
        {
            if (id != labUseCost.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                try
                {
                    var com = _context.Computer.Find(labUseCost.CId);

                    var lcid = new SqlParameter("@id", labUseCost.id);
                    var cupon = new SqlParameter("@Cupon", Cupon);
                    _context.Database.ExecuteSqlCommand("GiveDiscount @id, @Cupon", lcid, cupon);
                    

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabUseCostExists(labUseCost.id))
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
            ViewData["Cuponlist"] = new SelectList(new List<SelectListItem>{
                  new SelectListItem { Selected = true, Text = "None", Value = "None"},
                  new SelectListItem { Selected = false, Text = "Member", Value = "Member"},
                  new SelectListItem { Selected = false, Text = "Guest", Value = "Guest"},
                  new SelectListItem { Selected = false, Text = "Teacher", Value = "Teacher"},
                 }, "Value", "Text", 1);
            return View(labUseCost);
        }








        // GET: LabUseCosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labUseCost = await _context.LabUseCosts.FindAsync(id);
            if (labUseCost == null)
            {
                return NotFound();
            }
            ViewData["CId"] = new SelectList(_context.Computer, "Cid", "Cid", labUseCost.CId);
            ViewData["appUserID"] = new SelectList(_context.AppUser, "AppUserId", "Email", labUseCost.appUserID);
            return View(labUseCost);
        }

        // POST: LabUseCosts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,UseDate,hour,minute,totalCost,appUserID,CId")] LabUseCost labUseCost)
        {
            if (id != labUseCost.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var com = _context.Computer.Find(labUseCost.CId);
                    float costpermin = _context.LabCostRates.Where(r => r.Rlid == com.LabId).FirstOrDefault().costperminitue;
                    labUseCost.totalCost = costpermin * ((labUseCost.hour * 60) + labUseCost.minute);
                    _context.Update(labUseCost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LabUseCostExists(labUseCost.id))
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
            ViewData["CId"] = new SelectList(_context.Computer, "Cid", "Cid", labUseCost.CId);
            ViewData["appUserID"] = new SelectList(_context.AppUser, "AppUserId", "Email", labUseCost.appUserID);
            return View(labUseCost);
        }

        // GET: LabUseCosts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var labUseCost = await _context.LabUseCosts
                .Include(l => l.Computer)
                .Include(l => l.appUser)
                .FirstOrDefaultAsync(m => m.id == id);
            if (labUseCost == null)
            {
                return NotFound();
            }

            return View(labUseCost);
        }

        // POST: LabUseCosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var labUseCost = await _context.LabUseCosts.FindAsync(id);
            _context.LabUseCosts.Remove(labUseCost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LabUseCostExists(int id)
        {
            return _context.LabUseCosts.Any(e => e.id == id);
        }
    }
}
