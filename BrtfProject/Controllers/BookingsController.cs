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
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BrtfProject.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        private readonly BrtfDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public BookingsController(BrtfDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(int? UserId, int? AreaId, int? page, int? pageSizeID, string actionButton, string sortDirection = "asc", string sortField = "Booking")
        {
            IdentityUser IdentityUser = await _userManager.GetUserAsync(User);
            string userEmail = IdentityUser?.Email; // will give the user's Email

            var user = await _context.Users
                .Where(c => c.Email == userEmail)
                .FirstOrDefaultAsync();


            string[] sortOptions = new[] { "User", "Area", "Room", "StartdateTime", "EndDateTime"};

            PopulateDropDownLists();

            ViewData["Filtering"] = "";  //Assume not filtering

            var bookings = from b in _context.Bookings
                .Include(b => b.Area)
                .Include(b => b.Room)
                .Include(b => b.User)
                           select b;

            if ((user.Email != "") && (user.Email != "admin1@outlook.com"))
            {
                bookings = bookings.Where(p => p.User.Email == user.Email);
            }


            if (UserId.HasValue)
            {
                bookings = bookings.Where(p => p.UserId == UserId);
                ViewData["Filtering"] = " show";
            }
            if (AreaId.HasValue)
            {
                bookings = bookings.Where(p => p.AreaId == AreaId);
                ViewData["Filtering"] = " show";
            }

            

            //Before we sort, see if we have called for a change of filtering or sorting
            if (!String.IsNullOrEmpty(actionButton)) //Form Submitted so lets sort!
            {
                page = 1;

                if (actionButton != "Filter")//Change of sort is requested
                {
                    if (actionButton == sortField) //Reverse order on same field
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;//Sort by the button clicked
                }
            }

            if (sortField == "User")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings
                        .OrderByDescending(p => p.User.Email);
                }
                else
                {
                    bookings = bookings
                        .OrderBy(p => p.User.Email);
                }
            }
            else if (sortField == "Area")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings
                        .OrderByDescending(p => p.Area.AreaName);
                }
                else
                {
                    bookings = bookings
                        .OrderBy(p => p.Area.AreaName);
                }
            }
            else if (sortField == "Room")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings
                        .OrderByDescending(p => p.Room.name);
                }
                else
                {
                    bookings = bookings
                        .OrderBy(p => p.Room.name);
                }
            }
            else if (sortField == "StartdateTime")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings
                        .OrderByDescending(p => p.StartdateTime);
                }
                else
                {
                    bookings = bookings
                        .OrderBy(p => p.StartdateTime);
                }
            }
            else if (sortField == "EndDateTime")
            {
                if (sortDirection == "asc")
                {
                    bookings = bookings
                        .OrderByDescending(p => p.EndDateTime);
                }
                else
                {
                    bookings = bookings
                        .OrderBy(p => p.EndDateTime);
                }
            }

            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Booking>.CreateAsync(bookings.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
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
        public async Task<IActionResult> Create([Bind("UserId,AreaId,RoomID,SpecialNote,StartdateTime,EndDateTime")] Booking booking)
        {
            ViewData["RoomId"] = new SelectList(_context.Rooms, "ID", "name", booking.RoomID);
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName", booking.AreaId);
            ViewData["UserId"] = new SelectList(_context.Rooms, "ID", "capacity", booking.UserId);
            ViewData["UserId"] = new SelectList(_context.Users, "ID", "Email", booking.UserId);
            var userroom = await _context.Bookings
                .Where(c => c.RoomID == booking.RoomID)
                .Where(d => d.UserId == booking.UserId)
                .FirstOrDefaultAsync();

            var droom = await _context.Bookings
                .Where(c => c.RoomID == booking.RoomID)
                .FirstOrDefaultAsync();
            if (userroom != null)
            {
                ModelState.AddModelError("", "Duplicate booking of same Room and User exists ");
            }
            else if (droom != null)
            {
                string msg = "Duplicate booking of same Room exists with User : " + droom.User.FormalName;
                ModelState.AddModelError("", "Duplicate booking of same Room exists with User : " + droom.User.FormalName);
            }

            else
            {

                try
                {
                    if (ModelState.IsValid)
                    {
                        var FindRoom = await _context.Rooms.FirstOrDefaultAsync(m => m.ID == booking.RoomID);
                        if (FindRoom.MaxHours != 0)
                        {
                            TimeSpan? difference = booking.EndDateTime - booking.StartdateTime;
                            TimeSpan? convert = new TimeSpan(FindRoom.MaxHours, 0, 0);
                            if (difference > convert)
                            {
                                string msg = FindRoom.name + " has a maximum allowed booking of " + FindRoom.MaxHours + " hours at a time. Please lower your booking hours.";
                                ViewData["msg"] = msg;
                            }
                            else
                            {
                                _context.Add(booking);
                                await _context.SaveChangesAsync();
                                return RedirectToAction(nameof(Index));
                            }
                        }
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
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

        private SelectList UserSelectList(int? selectedId)
        {
            return new SelectList(_context.Users
                .OrderBy(d => d.Email), "ID", "Email", selectedId);
        }


        

        private SelectList AreaSelectList(int? selectedAreaId)
        {
            return new SelectList(_context.Areas
                .OrderBy(a => a.ID), "ID", "AreaName", selectedAreaId);
        }


        private SelectList RoomsSelectList(int? AreaId, int? selectedAreaId)
        {
            //The ProvinceID has been added so we can filter by it.


            var query = from c in _context.Rooms.Include(c => c.Area)
                        where c.AreaId == AreaId.GetValueOrDefault()
                        select c;
            return new SelectList(query.OrderBy(p => p.name), "ID", "name", selectedAreaId);
        }

        private void PopulateDropDownLists(Booking booking = null)
        {
            ViewData["AreaId"] = AreaSelectList(booking?.AreaId);
            ViewData["UserId"] = UserSelectList(booking?.UserId);
            ViewData["RoomID"] = RoomsSelectList(booking?.AreaId, booking?.RoomID);
        }

        [HttpGet]
        public JsonResult GetRooms(int? ID)
        {
            return Json(RoomsSelectList(ID, null));
        }



    }
}





