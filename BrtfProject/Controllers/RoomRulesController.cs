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
    public class RoomRulesController : Controller
    {
        private readonly BrtfDbContext _context;

        public RoomRulesController(BrtfDbContext context)
        {
            _context = context;
        }

        // GET: RoomRules
     [Authorize]
        public async Task<IActionResult> Index(string SearchString,
            int? page, int? pageSizeID, string actionButton, int? RoomId)
        {
            PopulateDropDownLists();
            var roomRules = from r in _context.RoomRules
                            .Include(p=>p.Room)
                 .AsNoTracking()
                        select r;
            if (RoomId.HasValue)
            {
                roomRules = roomRules.Where(p => p.RoomId == RoomId);
            }
            if (!String.IsNullOrEmpty(SearchString))
            {
                roomRules = roomRules.Where(p => p.RuleName.ToUpper().Contains(SearchString.ToUpper()));
            }
            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<RoomRules>.CreateAsync(roomRules.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
            //return View(await _context.RoomRules.ToListAsync());
        }
        private void PopulateDropDownLists()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms.Where(p => p.IsEnable == true), "ID", "name");
        }
      
        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        // GET: RoomRules/Details/5
       [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomRules = await _context.RoomRules
                .FirstOrDefaultAsync(m => m.id == id);
            if (roomRules == null)
            {
                return NotFound();
            }

            return View(roomRules);
        }

        // GET: RoomRules/Create
     [Authorize]
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "ID", "name");
            return View();
        }

        // POST: RoomRules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     [Authorize]
        public async Task<IActionResult> Create([Bind("id,RoomId,RuleName,RuleDescription")] RoomRules roomRules)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomRules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms.Where(p => p.IsEnable == true), "ID", "name",roomRules.RoomId);
            return View(roomRules);
        }

        // GET: RoomRules/Edit/5
     [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomRules = await _context.RoomRules.FindAsync(id);
            if (roomRules == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms.Where(p => p.IsEnable == true), "ID", "name", roomRules.RoomId);

            return View(roomRules);
        }

        // POST: RoomRules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
     [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("id,RoomId,RuleName,RuleDescription")] RoomRules roomRules)
        {
            if (id != roomRules.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(roomRules);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomRulesExists(roomRules.id))
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
            return View(roomRules);
        }

        // GET: RoomRules/Delete/5
     [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomRules = await _context.RoomRules
                .FirstOrDefaultAsync(m => m.id == id);
            if (roomRules == null)
            {
                return NotFound();
            }

            return View(roomRules);
        }

        // POST: RoomRules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomRules = await _context.RoomRules.FindAsync(id);
            _context.RoomRules.Remove(roomRules);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomRulesExists(int id)
        {
            return _context.RoomRules.Any(e => e.id == id);
        }
    }
}
