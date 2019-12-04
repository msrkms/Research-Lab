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
    public class BookingInfoesController : Controller
    {
        private readonly ResearchLabContext _context;

        public BookingInfoesController(ResearchLabContext context)
        {
            _context = context;
        }

        // GET: BookingInfoes
        public async Task<IActionResult> Index()
        {
            var researchLabContext = _context.BookingInfo.Include(b => b.AppUser).Include(b => b.C);
            return View(await researchLabContext.ToListAsync());
        }


        public async Task<IActionResult> BookingHistory()
        {
            var researchLabContext = _context.BookingInfo.Include(b => b.AppUser).Include(b => b.C).Include(b=>b.C.Lab);
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
                return Redirect("/BookingInfoes/BookAComputer/" + researchLab.Rlid);
            }
            
            
        }



        public async Task<IActionResult> BookAComputer(int? id)
        {

          //  return Ok(new { j = id });
            
            if (id == null)
            {
                return RedirectToAction(nameof(SelectLab));
            }
            
            ViewData["AppUserId"] = new SelectList(_context.AppUser.Where(u=>u.AppUserId==DataHolder.appUser.AppUserId), "AppUserId", "Email");
            ViewData["Cid"] = new SelectList(_context.Computer.Where(c => c.LabId == id && c.IsAvailable == true).ToList(), "Cid", "Cid");
            return View();
            
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BookAComputer([Bind("Biid,Cid,AppUserId,BookingDate,BookingStartTime,BookingEndTime")] BookingInfo bookingInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingInfo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(MyBooking));
        }



        public async Task<IActionResult> MyBooking()
        {


            if (DataHolder.appUser == null)
            {
                return Redirect("/AppUsers/Login/");
            }

            var mybookings = _context.BookingInfo.Include(bi=>bi.C).Include(bi=>bi.C.Lab).Where(bi => bi.AppUserId ==DataHolder.appUser.AppUserId);
            return View(await mybookings.ToListAsync());

        }


        public IActionResult SelectLabSearchComputer()
        {
            ViewData["Rlid"] = new SelectList(_context.ResearchLab, "Rlid", "LabName");
            return View();
        }

        [HttpPost]
        public IActionResult SelectLabSearchComputer([Bind("Rlid,LabName,LabLoction,LabAssistant")] ResearchLab researchLab)
        {

            if (researchLab.Rlid == 0)
            {
                return RedirectToAction(nameof(SelectLabSearchComputer));
            }
            else
            {
                return Redirect("/BookingInfoes/ShowAvailableComputers/" + researchLab.Rlid);
            }


        }


        public async Task<IActionResult> ShowAvailableComputers(int? id)
        {


            if (id == null)
            {
                return RedirectToAction(nameof(SelectLabSearchComputer));
            }

            var researchLabContext = _context.Computer.Where(c=>c.LabId==id);
            return View(await researchLabContext.ToListAsync());

        }




        public async Task<IActionResult> GetcomputerList(int? id)
        {
            var computerlist = new SelectList(_context.Computer.Where(c=>c.LabId==id && c.IsAvailable==true).ToList(),"Cid","Cid");
            return Json(computerlist);
        }



        // GET: BookingInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingInfo = await _context.BookingInfo
                .Include(b => b.AppUser)
                .Include(b => b.C)
                .FirstOrDefaultAsync(m => m.Biid == id);
            if (bookingInfo == null)
            {
                return NotFound();
            }

            return View(bookingInfo);
        }

        // GET: BookingInfoes/Create
        public IActionResult Create()
        {
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "AppUserId", "Email");
            ViewData["Cid"] = new SelectList(_context.Computer, "Cid", "Cid");
            return View();
        }

        // POST: BookingInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Biid,Cid,AppUserId,BookingDate,BookingStartTime,BookingEndTime")] BookingInfo bookingInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "AppUserId", "Email", bookingInfo.AppUserId);
            ViewData["Cid"] = new SelectList(_context.Computer, "Cid", "Cid", bookingInfo.Cid);
            return View(bookingInfo);
        }

        // GET: BookingInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingInfo = await _context.BookingInfo.FindAsync(id);
            if (bookingInfo == null)
            {
                return NotFound();
            }
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "AppUserId", "Email", bookingInfo.AppUserId);
            ViewData["Cid"] = new SelectList(_context.Computer, "Cid", "Cid", bookingInfo.Cid);
            return View(bookingInfo);
        }

        // POST: BookingInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Biid,Cid,AppUserId,BookingDate,BookingStartTime,BookingEndTime")] BookingInfo bookingInfo)
        {
            if (id != bookingInfo.Biid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingInfoExists(bookingInfo.Biid))
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
            ViewData["AppUserId"] = new SelectList(_context.AppUser, "AppUserId", "Email", bookingInfo.AppUserId);
            ViewData["Cid"] = new SelectList(_context.Computer, "Cid", "Cid", bookingInfo.Cid);
            return View(bookingInfo);
        }

        // GET: BookingInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingInfo = await _context.BookingInfo
                .Include(b => b.AppUser)
                .Include(b => b.C)
                .FirstOrDefaultAsync(m => m.Biid == id);
            if (bookingInfo == null)
            {
                return NotFound();
            }

            return View(bookingInfo);
        }

        // POST: BookingInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingInfo = await _context.BookingInfo.FindAsync(id);
            _context.BookingInfo.Remove(bookingInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingInfoExists(int id)
        {
            return _context.BookingInfo.Any(e => e.Biid == id);
        }
    }
}
