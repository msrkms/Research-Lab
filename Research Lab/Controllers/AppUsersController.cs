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
    public class AppUsersController : Controller
    {
        private readonly ResearchLabContext _context;

        public AppUsersController(ResearchLabContext context)
        {
            _context = context;
        }

        // GET: AppUsers
        public async Task<IActionResult> Index()
        {
            
            
               return await navigate();
            
         

        }


        public async Task<IActionResult> Login()
        {
            if (DataHolder.appUser != null)
            {

                return await navigate();
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("Email,Password")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                var User = await _context.AppUser
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.Email == appUser.Email);

                if (User.Password == appUser.Password)
                {
                    DataHolder.appUser = User;
                  return  await navigate();
                }
            }

            return RedirectToAction(nameof(Login));
            
        }

        private async Task<IActionResult>  navigate()
        {
            if (DataHolder.appUser != null)
            {
                if (DataHolder.appUser.RoleId == DataHolder.Student)
                {
                    return RedirectToAction(nameof(Profile));
                }
                else if (DataHolder.appUser.RoleId == DataHolder.Admin)
                {
                    return RedirectToAction(nameof(Admin));
                }
                else if (DataHolder.appUser.RoleId == DataHolder.LabAssistant)
                {
                    return RedirectToAction(nameof(LabAssistant));
                }
            }
            

            return RedirectToAction(nameof(Login));
        }


        public async Task<IActionResult> Registration()
        {
            if (DataHolder.appUser != null)
            {

                return RedirectToAction(nameof(Profile));
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("AppUserId,Name,Email,Phone,RoleId,IsVerified,Password")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                
                var usr = _context.AppUser.FirstOrDefault(u => u.Email == appUser.Email);
                try
                {
                    if (usr!=null)
                    {
                        return RedirectToAction(nameof(Registration));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                

                _context.Add(appUser);
                await _context.SaveChangesAsync();
            
                }

            return RedirectToAction(nameof(Login));
        }





        public async Task<IActionResult> AddLabAssistant()
        {
            if (DataHolder.appUser != null)
            {

                return RedirectToAction(nameof(Profile));
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddLabAssistant([Bind("Name,Email,Phone,RoleId,IsVerified,Password")] AppUser appUser)
        {




            var usr = _context.AppUser.FirstOrDefault(u => u.Email == appUser.Email);
            if (usr != null)
            {
                return RedirectToAction(nameof(Admin));
            }

            _context.Add(appUser);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Admin));

        }

        public async Task<IActionResult> ShowAllLabAssistant()
        {
            var labassistant=_context.AppUser.Where(u=>u.RoleId==DataHolder.LabAssistant).ToList();
            return View(labassistant);
        }

        public async Task<IActionResult> Admin()
        {
            if (DataHolder.appUser == null)
            {
                return RedirectToAction(nameof(Login));
            }

            return View();
        }



        public async Task<IActionResult> Profile()
        {
            if (DataHolder.appUser == null)
            {

                return RedirectToAction(nameof(Login));
            }

            if (!DataHolder.appUser.IsVerified)
            {
                return RedirectToAction(nameof(NeedVerify));
            }
            return View(DataHolder.appUser);
        }

        public async Task<IActionResult> LabAssistant()
        {
            if (DataHolder.appUser == null)
            {

                return RedirectToAction(nameof(Login));
            }
            
            return View();
        }

        public async Task<IActionResult> NeedVerify()
        {
            if (DataHolder.appUser == null)
            {

                return RedirectToAction(nameof(Login));
            }

            return View();
        }


        public async Task<IActionResult> VerifyUser()
        {
            if(DataHolder.appUser == null)
            {
                return RedirectToAction(nameof(VerifyUser));
            }

            var users = _context.AppUser.Where(u => u.IsVerified == false).ToList();
            ViewData["User"] = new SelectList(users, "AppUserId", "Email");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> VerifyUser([Bind("AppUserId,Email")] AppUser appUser)
        {
            
            if (ModelState.IsValid)
            {
                var usr = _context.AppUser.FirstOrDefault(u => u.AppUserId == appUser.AppUserId);
                if (usr.AppUserId == appUser.AppUserId)
                {
                    usr.IsVerified = true;
                }
                _context.Update(usr);
                await _context.SaveChangesAsync();
            }
            
          //  return Ok(new { u = appUser.AppUserId });
            return RedirectToAction(nameof(Login));
        }



        public async Task<IActionResult> Logout()
        {
            DataHolder.appUser = null;

            return RedirectToAction(nameof(Login));
        }


        






        // GET: AppUsers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUser
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AppUserId == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // GET: AppUsers/Create
        public IActionResult Create()
        {
            ViewData["RoleId"] = new SelectList(_context.UserRole, "RoleId", "RoleType");
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppUserId,Name,Email,Phone,RoleId,IsVerified")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appUser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.UserRole, "RoleId", "RoleType", appUser.RoleId);
            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUser.FindAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }
            ViewData["RoleId"] = new SelectList(_context.UserRole, "RoleId", "RoleType", appUser.RoleId);
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppUserId,Name,Email,Phone,RoleId,IsVerified")] AppUser appUser)
        {
            if (id != appUser.AppUserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appUser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppUserExists(appUser.AppUserId))
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
            ViewData["RoleId"] = new SelectList(_context.UserRole, "RoleId", "RoleType", appUser.RoleId);
            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _context.AppUser
                .Include(a => a.Role)
                .FirstOrDefaultAsync(m => m.AppUserId == id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appUser = await _context.AppUser.FindAsync(id);
            _context.AppUser.Remove(appUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppUserExists(int id)
        {
            return _context.AppUser.Any(e => e.AppUserId == id);
        }
    }
}
