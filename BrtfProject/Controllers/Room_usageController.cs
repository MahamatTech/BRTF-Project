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
    public class Room_usageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Room_usageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Room_usage
        public async Task<IActionResult> Index()
        {
            return View(await _context.Room_usage.ToListAsync());
        }

        // GET: Room_usage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room_usage = await _context.Room_usage
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room_usage == null)
            {
                return NotFound();
            }

            return View(room_usage);
        }

        // GET: Room_usage/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Room_usage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,MiddleName,LastName,Phone,Program,Term,Usertype,IsActive")] Room_usage room_usage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room_usage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room_usage);
        }

        // GET: Room_usage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room_usage = await _context.Room_usage.FindAsync(id);
            if (room_usage == null)
            {
                return NotFound();
            }
            return View(room_usage);
        }

        // POST: Room_usage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,MiddleName,LastName,Phone,Program,Term,Usertype,IsActive")] Room_usage room_usage)
        {
            if (id != room_usage.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room_usage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Room_usageExists(room_usage.ID))
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
            return View(room_usage);
        }

        // GET: Room_usage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room_usage = await _context.Room_usage
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room_usage == null)
            {
                return NotFound();
            }

            return View(room_usage);
        }

        // POST: Room_usage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room_usage = await _context.Room_usage.FindAsync(id);
            _context.Room_usage.Remove(room_usage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Room_usageExists(int id)
        {
            return _context.Room_usage.Any(e => e.ID == id);
        }
    }
}
