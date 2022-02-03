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
    public class ReservationRoomDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReservationRoomDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ReservationRoomDetails
        public async Task<IActionResult> Index()
        {
            return View(await _context.ReservationRoomDetails.ToListAsync());
        }

        // GET: ReservationRoomDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationRoomDetails = await _context.ReservationRoomDetails
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservationRoomDetails == null)
            {
                return NotFound();
            }

            return View(reservationRoomDetails);
        }

        // GET: ReservationRoomDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ReservationRoomDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,ReservationId,NumberOfRoomsBooked,ReservationStatus")] ReservationRoomDetails reservationRoomDetails)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reservationRoomDetails);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(reservationRoomDetails);
        }

        // GET: ReservationRoomDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationRoomDetails = await _context.ReservationRoomDetails.FindAsync(id);
            if (reservationRoomDetails == null)
            {
                return NotFound();
            }
            return View(reservationRoomDetails);
        }

        // POST: ReservationRoomDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ReservationId,NumberOfRoomsBooked,ReservationStatus")] ReservationRoomDetails reservationRoomDetails)
        {
            if (id != reservationRoomDetails.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservationRoomDetails);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationRoomDetailsExists(reservationRoomDetails.id))
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
            return View(reservationRoomDetails);
        }

        // GET: ReservationRoomDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservationRoomDetails = await _context.ReservationRoomDetails
                .FirstOrDefaultAsync(m => m.id == id);
            if (reservationRoomDetails == null)
            {
                return NotFound();
            }

            return View(reservationRoomDetails);
        }

        // POST: ReservationRoomDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reservationRoomDetails = await _context.ReservationRoomDetails.FindAsync(id);
            _context.ReservationRoomDetails.Remove(reservationRoomDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationRoomDetailsExists(int id)
        {
            return _context.ReservationRoomDetails.Any(e => e.id == id);
        }
    }
}
