using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Models;
using BrtfProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;

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
        [Authorize(Roles = "Admin, Super-Admin")]
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
        [Authorize(Roles = "Admin, Super-Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["ProgramTermId"] = new SelectList(_context.ProgramTerms, "ID", "ProgramInfo");

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
        [Authorize(Roles = "Admin, Super-Admin")]
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
                u => u.FirstName, u => u.LastName, u => u.StudentID, u => u.ProgramTermId, u => u.Purge))
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
        [HttpPost]
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
        {
            //Note: This is a very basic example and has 
            //no ERROR HANDLING.  It also assumes that
            //duplicate values are allowed, both in the 
            //uploaded data and the DbSet.
            ExcelPackage excel;
            if(theExcel != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await theExcel.CopyToAsync(memoryStream);
                    excel = new ExcelPackage(memoryStream);
                }
                var workSheet = excel.Workbook.Worksheets[0];
                var start = workSheet.Dimension.Start;
                var end = workSheet.Dimension.End;

                //Start a new list to hold imported objects
                List<User> users = new List<User>();

                for (int row = start.Row; row <= end.Row; row++)
                {
                    if (workSheet.Cells[row, 1].Text != "" && workSheet.Cells[row, 2].Text != "" && workSheet.Cells[row, 3].Text != "" && workSheet.Cells[row, 4].Text != "" && workSheet.Cells[row, 7].Text != "" && workSheet.Cells[row, 10].Text != "")
                    {
                        // Row by row...
                        User a = new User
                        {
                            StudentID = workSheet.Cells[row, 1].Text,
                            FirstName = workSheet.Cells[row, 2].Text,
                            MiddleName = workSheet.Cells[row, 3].Text,
                            LastName = workSheet.Cells[row, 4].Text,
                            Email = workSheet.Cells[row, 7].Text
                        };
                        users.Add(a);
                    }
                }
                _context.Users.AddRange(users);
                _context.SaveChanges();
                
            }
            return RedirectToAction("Index", "UserIdentity");
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
