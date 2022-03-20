using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Data;
using BrtfProject.Models;
using Microsoft.AspNetCore.Authorization;
using BrtfProject.Utilities;

namespace BrtfProject.Controllers
{
    public class ProgramTermsController : Controller
    {
        private readonly BrtfDbContext _context;

        public ProgramTermsController(BrtfDbContext context)
        {
            _context = context;
        }

        // GET: ProgramTerms
       [Authorize]
        public async Task<IActionResult> Index(string SearchStringInfo,
            int? page, int? pageSizeID, string actionButton, string SearchStringTerm,string SearchProgramLevel,
            string SearchProgramCode,int? UserGroupId)
        {
            PopulateDropDownLists();
            var terms = from r in _context.ProgramTerms
                        .Include(p=> p.UserGroup)
                 .AsNoTracking()
                        select r;
            if (!String.IsNullOrEmpty(SearchStringInfo))
            {
                terms = terms.Where(p => p.ProgramInfo.ToUpper().Contains(SearchStringInfo.ToUpper()));
            }
            if (!String.IsNullOrEmpty(SearchProgramCode))
            {
                terms = terms.Where(p => p.ProgramCode.ToUpper().Contains(SearchProgramCode.ToUpper()));
            }

            if (UserGroupId.HasValue)
            {
                terms = terms.Where(p => p.UserGroupId == UserGroupId);
            }

            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<ProgramTerm>.CreateAsync(terms.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
           // return View(await _context.ProgramTerms.ToListAsync());
        }
        
       
        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        // GET: ProgramTerms/Details/5
       [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programTerm = await _context.ProgramTerms
                .Include(r => r.UserGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (programTerm == null)
            {
                return NotFound();
            }

            return View(programTerm);
        }

        // GET: ProgramTerms/Create
       [Authorize]
        public IActionResult Create()
        {
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "ID", "UserGroupName");
            return View();
        }

        // POST: ProgramTerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize]
        public async Task<IActionResult> Create(ProgramTerm programTerm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(programTerm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "ID", "UserGroupName");
            return View(programTerm);
        }

        // GET: ProgramTerms/Edit/5
       [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "ID", "UserGroupName");
            if (id == null)
            {
                return NotFound();
            }

            var programTerm = await _context.ProgramTerms.FindAsync(id);
            if (programTerm == null)
            {
                return NotFound();
            }
            return View(programTerm);
        }

        // POST: ProgramTerms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       [Authorize]
        public async Task<IActionResult> Edit(int id, ProgramTerm programTerm)
        {
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "ID", "UserGroupName");
            if (id != programTerm.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(programTerm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProgramTermExists(programTerm.ID))
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
            return View(programTerm);
        }

        // GET: ProgramTerms/Delete/5
       [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var programTerm = await _context.ProgramTerms
                .Include(r => r.UserGroup)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (programTerm == null)
            {
                return NotFound();
            }

            return View(programTerm);
        }

        // POST: ProgramTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var programTerm = await _context.ProgramTerms.FindAsync(id);
            _context.ProgramTerms.Remove(programTerm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProgramTermExists(int id)
        {
            return _context.ProgramTerms.Any(e => e.ID == id);
        }


        private SelectList UserGroupSelectList(int? selectedId)
        {
            return new SelectList(_context.UserGroups
                .OrderBy(d => d.UserGroupName), "ID", "UserGroupName", selectedId);
        }
        private void PopulateDropDownLists(UserGroup userGroup = null)
        {
            ViewData["UserGroupId"] = UserGroupSelectList(userGroup?.ID);
        }
    }
}
