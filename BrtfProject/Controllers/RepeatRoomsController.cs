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
    public class RepeatRoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RepeatRoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: RepeatRooms
        public async Task<IActionResult> Index()
        {
            return View(await _context.RepeatRoom.ToListAsync());
        }

        // GET: RepeatRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repeatRoom = await _context.RepeatRoom
                .FirstOrDefaultAsync(m => m.id == id);
            if (repeatRoom == null)
            {
                return NotFound();
            }

            return View(repeatRoom);
        }

        // GET: RepeatRooms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RepeatRooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,RoomId,Duration,EndDate")] RepeatRoom repeatRoom)
        {
            if (ModelState.IsValid)
            {
                _context.Add(repeatRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(repeatRoom);
        }

        // GET: RepeatRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repeatRoom = await _context.RepeatRoom.FindAsync(id);
            if (repeatRoom == null)
            {
                return NotFound();
            }
            return View(repeatRoom);
        }

        // POST: RepeatRooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,RoomId,Duration,EndDate")] RepeatRoom repeatRoom)
        {
            if (id != repeatRoom.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(repeatRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RepeatRoomExists(repeatRoom.id))
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
            return View(repeatRoom);
        }

        // GET: RepeatRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var repeatRoom = await _context.RepeatRoom
                .FirstOrDefaultAsync(m => m.id == id);
            if (repeatRoom == null)
            {
                return NotFound();
            }

            return View(repeatRoom);
        }

        // POST: RepeatRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var repeatRoom = await _context.RepeatRoom.FindAsync(id);
            _context.RepeatRoom.Remove(repeatRoom);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RepeatRoomExists(int id)
        {
            return _context.RepeatRoom.Any(e => e.id == id);
        }
    }
}
