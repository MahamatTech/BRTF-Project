using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Data;
using BrtfProject.Models;

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
        public async Task<IActionResult> Index()
        {
            var brtfDbContext = _context.RoomRules.Include(r => r.Area);
            return View(await brtfDbContext.ToListAsync());
        }

        // GET: RoomRules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomRules = await _context.RoomRules
                .Include(r => r.Area)
                .FirstOrDefaultAsync(m => m.id == id);
            if (roomRules == null)
            {
                return NotFound();
            }

            return View(roomRules);
        }

        // GET: RoomRules/Create
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Areas.OrderBy(a => a.AreaName), "ID", "AreaName");
            return View();
        }

        // POST: RoomRules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,rule,AreaId")] RoomRules roomRules)
        {
            if (ModelState.IsValid)
            {
                _context.Add(roomRules);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.Areas.OrderBy(a => a.AreaName), "ID", "AreaName", roomRules.AreaId);
            return View(roomRules);
        }

        // GET: RoomRules/Edit/5
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
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName", roomRules.AreaId);
            return View(roomRules);
        }

        // POST: RoomRules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,StartHour,EndHour,MaxHours,AreaId")] RoomRules roomRules)
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
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName", roomRules.AreaId);
            return View(roomRules);
        }

        // GET: RoomRules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomRules = await _context.RoomRules
                .Include(r => r.Area)
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
