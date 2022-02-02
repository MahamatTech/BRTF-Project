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
    public class Room_UsageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Room_UsageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Room_Usage
        public async Task<IActionResult> Index()
        {
            return View(await _context.Room_Usage.ToListAsync());
        }

        // GET: Room_Usage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room_Usage = await _context.Room_Usage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room_Usage == null)
            {
                return NotFound();
            }

            return View(room_Usage);
        }

        // GET: Room_Usage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Room_Usage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Room_Id,Student_Id,StartLevel,LastLevel")] Room_Usage room_Usage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room_Usage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room_Usage);
        }

        // GET: Room_Usage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room_Usage = await _context.Room_Usage.FindAsync(id);
            if (room_Usage == null)
            {
                return NotFound();
            }
            return View(room_Usage);
        }

        // POST: Room_Usage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Room_Id,Student_Id,StartLevel,LastLevel")] Room_Usage room_Usage)
        {
            if (id != room_Usage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room_Usage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Room_UsageExists(room_Usage.Id))
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
            return View(room_Usage);
        }

        // GET: Room_Usage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room_Usage = await _context.Room_Usage
                .FirstOrDefaultAsync(m => m.Id == id);
            if (room_Usage == null)
            {
                return NotFound();
            }

            return View(room_Usage);
        }

        // POST: Room_Usage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room_Usage = await _context.Room_Usage.FindAsync(id);
            _context.Room_Usage.Remove(room_Usage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Room_UsageExists(int id)
        {
            return _context.Room_Usage.Any(e => e.Id == id);
        }
    }
}
