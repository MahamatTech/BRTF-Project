using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Models;
using CanadaGames.Data;
using BrtfProject.Data;

namespace BrtfProject.Controllers
{
    public class UserIdentityController : Controller
    {
        private readonly BrtfDbContext _context;
        private readonly ApplicationDbContext _identityContext;

        public UserIdentityController(BrtfDbContext context, ApplicationDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        // GET: UserIdentity
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        //// GET: UserIdentity/Details/5
        //public async Task<IActionResult> Details(int? id)
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

        // GET: UserIdentity/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserIdentity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, bool Purge)
        {
            var userToUpdate = await _context.Users
                .FirstOrDefaultAsync(u => u.ID == id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            if(userToUpdate.Purge == false & Purge == true)
            {
                await DeleteIdentityUser(userToUpdate.Email);
            }

            if (await TryUpdateModelAsync<User>(userToUpdate, "", 
                u => u.FirstName, u => u.LastName, c => c.StudentID, c => c.Term, c => c.Purge))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(userToUpdate);
        }

        private async Task DeleteIdentityUser(string Email)
        {
            var userToDelete = await _identityContext.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
            if(userToDelete != null)
            {
                _identityContext.Users.Remove(userToDelete);
                await _identityContext.SaveChangesAsync();
            }
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
