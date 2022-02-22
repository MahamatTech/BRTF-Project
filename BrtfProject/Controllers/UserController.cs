using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Models;
using Microsoft.AspNetCore.Authorization;
using BrtfProject.Utilities;
using BrtfProject.Data;

namespace BrtfProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //Specialized controller just used to allow an 
        //Authenticated user to maintain their own  account details.

        private readonly BrtfDbContext _context;

        public UserController(BrtfDbContext context)
        {
            _context = context;
        }

        // GET: UserAccount
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Details));
        }

        // GET: UserAccount/Details/5
        public async Task<IActionResult> Details()
        {

            var user = await _context.Users
               .Where(c => c.Email == User.Identity.Name)
               .FirstOrDefaultAsync();
            if (user == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View(user);
        }

        // GET: UserAccount/Create
        public IActionResult Create()
        {
            ViewData["ProgramTermId"] = new SelectList(_context.ProgramTerms, "ID", "ProgramInfo");
            return View();
        }

        // POST: UserAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentID,FirstName,MiddleName,LastName,ProgramTermId,Email")] User user)
        {
            user.Email = User.Identity.Name;
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Details));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(user);
        }

        // GET: UserAccount/Edit/5
        public async Task<IActionResult> Edit()
        {
            ViewData["ProgramTermId"] = new SelectList(_context.ProgramTerms, "ID", "ProgramInfo");

            var user = await _context.Users
                .Where(c => c.Email == User.Identity.Name)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return RedirectToAction(nameof(Create));
            }
            return View(user);
        }

        // POST: UserAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var userToUpdate = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<User>(userToUpdate, "",
                c => c.FirstName, c => c.MiddleName, c => c.LastName,  c => c.StudentID, c => c.ProgramTermId, c => c.Purge))
            {
                try
                {
                    _context.Update(userToUpdate);
                    await _context.SaveChangesAsync();
                    UpdateUserNameCookie(userToUpdate.FullName);
                    return RedirectToAction(nameof(Details));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. The record you attempted to edit "
                                + "was modified by another user after you received your values.  You need to go back and try your edit again.");
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Something went wrong in the database.");
                }
            }
            return View(userToUpdate);

        }

        //// GET: UserAccount/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        //// POST: UserAccount/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private void UpdateUserNameCookie(string userName)
        {
            CookieHelper.CookieSet(HttpContext, "userName", userName, 960);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
