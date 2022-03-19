using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Data;
using BrtfProject.Models;
using BrtfProject.Utilities;

namespace BrtfProject.Controllers
{
    public class UserGroupsController : Controller
    {
        private readonly BrtfDbContext _context;

        public UserGroupsController(BrtfDbContext context)
        {
            _context = context;
        }

        // GET: UserGroups
        public async Task<IActionResult> Index(string SearchUserGroupName, int? page, int? pageSizeID, string actionButton)
        {
            var userGroups = from r in _context.UserGroups
                .AsNoTracking()
                        select r;
            if (!String.IsNullOrEmpty(SearchUserGroupName))
            {
                userGroups = userGroups.Where(p => p.UserGroupName.ToUpper().Contains(SearchUserGroupName.ToUpper()));
            }

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<UserGroup>.CreateAsync(userGroups.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }
        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        // GET: UserGroups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGroup = await _context.UserGroups
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userGroup == null)
            {
                return NotFound();
            }

            return View(userGroup);
        }

        // GET: UserGroups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserGroups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserGroupName")] UserGroup userGroup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userGroup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userGroup);
        }

        // GET: UserGroups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGroup = await _context.UserGroups.FindAsync(id);
            if (userGroup == null)
            {
                return NotFound();
            }
            return View(userGroup);
        }

        // POST: UserGroups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserGroupName")] UserGroup userGroup)
        {
            if (id != userGroup.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userGroup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserGroupExists(userGroup.ID))
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
            return View(userGroup);
        }

        // GET: UserGroups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userGroup = await _context.UserGroups
                .FirstOrDefaultAsync(m => m.ID == id);
            if (userGroup == null)
            {
                return NotFound();
            }

            return View(userGroup);
        }

        // POST: UserGroups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userGroup = await _context.UserGroups.FindAsync(id);
            _context.UserGroups.Remove(userGroup);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserGroupExists(int id)
        {
            return _context.UserGroups.Any(e => e.ID == id);
        }
    }
}
