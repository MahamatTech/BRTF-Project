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
            int? page, int? pageSizeID, string actionButton, string SearchStringTerm)
        {
            var terms = from r in _context.ProgramTerms
                 .AsNoTracking()
                        select r;
            if (!String.IsNullOrEmpty(SearchStringInfo))
            {
                terms = terms.Where(p => p.ProgramInfo.ToUpper().Contains(SearchStringInfo.ToUpper()));
            }
            if (!String.IsNullOrEmpty(SearchStringTerm))
            {
                terms = terms.Where(p => p.Term.ToUpper().Contains(SearchStringTerm.ToUpper()));
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
            return View(programTerm);
        }

        // GET: ProgramTerms/Edit/5
       [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
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
    }
}
