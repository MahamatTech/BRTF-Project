using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Data;
using BrtfProject.Models;
using System;

namespace BrtfProject.Controllers
{
    public class BookingsController : Controller
    {
        private readonly BrtfDbContext _context;

        public BookingsController(BrtfDbContext context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index()
        {
            var brtfDbContext = _context.Bookings.Include(b => b.Area).Include(b => b.Room).Include(b => b.User);
            return View(await brtfDbContext.ToListAsync());
        }

        // GET: Bookings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Area)
                .Include(b => b.Room)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "ID", "name");
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName");
            ViewData["UserId"] = new SelectList(_context.Rooms, "ID", "capacity");
            ViewData["UserId"] = new SelectList(_context.Users, "ID", "Email");



            PopulateDropDownLists();

            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Booking booking)
        {
            if (ModelState.IsValid)
            {
                if(booking.Area.FunctionalRules.MaxHours != 0)
                {
                    //ok so this is a little confusing so i'll comment to explain
                    //To start, we create a TimeSpan to get the time between the startdate and the enddate.
                    //Next we make a string of the maxhours added on to 0.
                    //The reason for the 0. is because TimeSpan puts days and hours together, meaning 1 day and 2 hours is equal to 1.02.
                    //So if we just want hours, we need to get rid of the 0.
                    //Then we convert the string we just made into a nullable TimeSpan
                    //Finally, compare the difference of the start and end date to the max hours of the RoomRules and whoila, a comparison done.
                    TimeSpan? difference = booking.StartdateTime - booking.EndDateTime;
                    string con = "0." + booking.Area.FunctionalRules.MaxHours.ToString();
                    TimeSpan? convert = TimeSpan.Parse(con);
                    if (difference > convert)
                    {
                        string msg = booking.Area.AreaName + " has a maximum allowed booking of " + booking.Area.FunctionalRules.MaxHours + " hours at a time. Please lower your booking hours.";
                        ViewData["msg"] = msg;
                    }
                    else
                    {
                        _context.Add(booking);
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(_context.Rooms, "ID", "name", booking.RoomID);
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName", booking.AreaId);
            ViewData["UserId"] = new SelectList(_context.Rooms, "ID", "capacity", booking.UserId);
            ViewData["UserId"] = new SelectList(_context.Users, "ID", "Email", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            ViewData["RoomId"] = new SelectList(_context.Rooms, "ID", "name", booking.RoomID);
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName", booking.AreaId);
            ViewData["UserId"] = new SelectList(_context.Rooms, "ID", "capacity", booking.UserId);
            ViewData["UserId"] = new SelectList(_context.Users, "ID", "Email", booking.UserId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Booking booking)
        {
            if (id != booking.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.ID))
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
            ViewData["RoomId"] = new SelectList(_context.Rooms, "ID", "name", booking.RoomID);
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName", booking.AreaId);
            ViewData["UserId"] = new SelectList(_context.Rooms, "ID", "capacity", booking.UserId);
            ViewData["UserId"] = new SelectList(_context.Users, "ID", "Email", booking.UserId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .Include(b => b.Area)
                .Include(b => b.Room)
                .Include(b => b.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.ID == id);
        }

        private SelectList AreaSelectList(int?  selectedAreaId)
        {
            return new SelectList(_context.Areas
                .OrderBy(a => a.ID), "ID", "AreaName", selectedAreaId);
        }


        private SelectList RoomsSelectList(int? AreaId, int? selectedAreaId)
        {
            //The ProvinceID has been added so we can filter by it.

            
            var query = 
                from r in _context.Rooms.Include(r => r.Area)
                        select r;
            if (AreaId.HasValue)
            {
                query = query.Where(a => a.AreaId == AreaId);
            }
            return new SelectList(query.OrderBy(a => a.ID), "ID", "Room", selectedAreaId);
        }

        private void PopulateDropDownLists(Booking booking = null)
        {
            ViewData["AreaId"] = AreaSelectList(booking?.AreaId);
            ViewData["ID"] = RoomsSelectList(booking?.AreaId, booking?.ID);
        }

        [HttpGet]
        public JsonResult GetRooms(int? ID)
        {
            return Json(RoomsSelectList(ID, null));
        }



    }
}





