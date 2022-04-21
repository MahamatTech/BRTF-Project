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
using System.Security.Claims;
using OfficeOpenXml;
using Microsoft.AspNetCore.Http.Features;
using System.IO;
using System.Drawing;
using OfficeOpenXml.Style;
using Microsoft.IdentityModel.Protocols;
using Microsoft.AspNetCore.Http;

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
        public async Task<IActionResult> Index(int? UserId, int? RoomID, int? AreaId, int? page, int? pageSizeID, string actionButton,
            string StartdateTime, string EndDateTime,
            string sortDirection = "asc", string sortField = "Booking")
        {
            IdentityUser IdentityUser = await _userManager.GetUserAsync(User);
            string userEmail = IdentityUser?.Email; // will give the user's Email

            var user = await _context.Users
                .Where(c => c.Email == userEmail)
                .FirstOrDefaultAsync();

            var userFoundId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userFromUserManager = await _userManager.FindByIdAsync(userFoundId);


            string[] sortOptions = new[] { "User", "Area", "Room", "StartdateTime", "EndDateTime" };

            PopulateDropDownLists();

            ViewData["Filtering"] = "";  //Assume not filtering
            ViewBag.Startdatetime = StartdateTime;
            ViewBag.EnddateTime = EndDateTime;
            var bookings = from b in _context.Bookings
                .Include(b => b.Area)
                .Include(b => b.Area.Rooms)
                .Include(b => b.Room)
                .Include(b => b.User)
                select b;
          
            var roles = await _userManager.GetRolesAsync(userFromUserManager);
            if (roles.Count() == 0)
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

            if (RoomID.HasValue)
            {
                bookings = bookings.Where(p => p.RoomID == RoomID);
                ViewData["Filtering"] = " show";
            }

            //bookings = bookings.Where(b => b.User.Email.ToLower() == userFromUserManager.Email.ToLower());

            if (StartdateTime != null)
            {
                bookings = bookings.Where(p => p.StartdateTime >= Convert.ToDateTime(StartdateTime));
                ViewData["Filtering"] = " show";
            }
            if (EndDateTime != null)
            {
                bookings = bookings.Where(p => p.EndDateTime <= Convert.ToDateTime(EndDateTime));
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
                        .OrderByDescending(p => p.RoomID);
                }
                else
                {
                    bookings = bookings
                        .OrderBy(p => p.RoomID);
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
                .Include(b => b.Area.Rooms)

                .Include(b => b.Room)
                .Include(b => b.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (booking == null)
            {
                return NotFound();
            }



            return View(booking);
           // return View(booking);
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
            var userFoundId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userFromUserManager = await _userManager.FindByIdAsync(userFoundId);
            var creatingUser = await _context.Users.Where(u => u.Email.ToLower() == userFromUserManager.Email.ToLower()).FirstOrDefaultAsync();


            var userroom = await _context.Bookings
                .Where(c => c.RoomID == booking.RoomID)
                .Where(d => d.UserId == creatingUser.ID)
                .FirstOrDefaultAsync();

            var droom = await _context.Bookings
                .Include(b => b.User)
                .Where(c => c.RoomID == booking.RoomID)
                .FirstOrDefaultAsync();

            if (userroom != null)
            {
                ModelState.AddModelError("", "This room is already booked by : " + droom.User.FormalName);
            }
            else if (droom != null)
            {

                ModelState.AddModelError("", "This room is already booked by : " + droom.User.FormalName);
               // ModelState.AddModelError(string.Empty, "This rrom is already booked by another user");

            }

            else
            {

                if (ModelState.IsValid)
                {
                    booking.UserId = creatingUser.ID;
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
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

        [Authorize(Roles = "Super-Admin")]
        public IActionResult DownloadBooking()
        {
            var bookings = from b in _context.Bookings
                 .Include(b => b.Area)
                 .Include(b => b.Area.Rooms)
                 .Include(b => b.Room)
                 .Include(b => b.User)
                           orderby b.User.Email descending
                           select new
                           {
                               User = b.User.Email,
                               Area = b.Area.AreaName,
                               Room = b.Room.name,
                               StartDate = b.StartdateTime,
                               EndDate = b.EndDateTime

                           };

            int numRows = bookings.Count();

            if (numRows > 0) //We have data
            {
                //Create a new spreadsheet from scratch.
                using (ExcelPackage excel = new ExcelPackage())
                {

                    var workSheet = excel.Workbook.Worksheets.Add("bookings");

                    //Note: Cells[row, column]
                    workSheet.Cells[3, 1].LoadFromCollection(bookings, true);

                    //Style first column for dates
                    workSheet.Column(4).Style.Numberformat.Format = "yyyy-mm-dd hh:mm AM/PM";
                    workSheet.Column(5).Style.Numberformat.Format = "yyyy-mm-dd hh:mm AM/PM";

                   

                    //Note: You can define a BLOCK of cells: Cells[startRow, startColumn, endRow, endColumn]
                    //Make Date and Patient Bold
                    workSheet.Cells[4, 1, numRows + 3, 2].Style.Font.Bold = true;

                    //Note: these are fine if you are only 'doing' one thing to the range of cells.
                    //Otherwise you should USE a range object for efficiency
                   

                    //Set Style and backgound colour of headings
                    using (ExcelRange headings = workSheet.Cells[3, 1, 3, 7])
                    {
                        headings.Style.Font.Bold = true;
                        var fill = headings.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.LightBlue);
                    }

                    ////Boy those notes are BIG!
                    ////Lets put them in comments instead.
                    //for (int i = 4; i < numRows + 4; i++)
                    //{
                    //    using (ExcelRange Rng = workSheet.Cells[i, 7])
                    //    {
                    //        string[] commentWords = Rng.Value.ToString().Split(' ');
                    //        Rng.Value = commentWords[0] + "...";
                    //        //This LINQ adds a newline every 7 words
                    //        string comment = string.Join(Environment.NewLine, commentWords
                    //            .Select((word, index) => new { word, index })
                    //            .GroupBy(x => x.index / 7)
                    //            .Select(grp => string.Join(" ", grp.Select(x => x.word))));
                    //        ExcelComment cmd = Rng.AddComment(comment, "Apt. Notes");
                    //        cmd.AutoFit = true;
                    //    }
                    //}

                    //Autofit columns
                    workSheet.Cells.AutoFitColumns();
                    //Note: You can manually set width of columns as well
                    //workSheet.Column(7).Width = 10;

                    //Add a title and timestamp at the top of the report
                    workSheet.Cells[1, 1].Value = "Booking Report";
                    using (ExcelRange Rng = workSheet.Cells[1, 1, 1, 6])
                    {
                        Rng.Merge = true; //Merge columns start and end range
                        Rng.Style.Font.Bold = true; //Font should be bold
                        Rng.Style.Font.Size = 18;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    }
                    //Since the time zone where the server is running can be different, adjust to 
                    //Local for us.
                    DateTime utcDate = DateTime.UtcNow;
                    TimeZoneInfo esTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
                    DateTime localDate = TimeZoneInfo.ConvertTimeFromUtc(utcDate, esTimeZone);
                    using (ExcelRange Rng = workSheet.Cells[2, 6])
                    {
                        Rng.Value = "Created: " + localDate.ToShortTimeString() + " on " +
                            localDate.ToShortDateString();
                        Rng.Style.Font.Bold = true; //Font should be bold
                        Rng.Style.Font.Size = 12;
                        Rng.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    }

                    //Ok, time to download the Excel

                    //I usually stream the response back to avoid possible
                    //out of memory errors on the server if you have a large spreadsheet.
                    //NOTE: Since .NET Core 3 most Web Servers disallow sync IO so we
                    //need to temporarily change the setting for the server.
                    //If we can't then we will try to build the file and return a FileContentResult
                    var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
                    if (syncIOFeature != null)
                    {
                        syncIOFeature.AllowSynchronousIO = true;
                        using (var memoryStream = new MemoryStream())
                        {
                            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            Response.Headers["content-disposition"] = "attachment;  filename=Bookings.xlsx";
                            excel.SaveAs(memoryStream);
                            memoryStream.WriteTo(Response.Body);
                        }
                    }
                    else
                    {
                        try
                        {
                            Byte[] theData = excel.GetAsByteArray();
                            string filename = "Bookings.xlsx";
                            string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            return File(theData, mimeType, filename);
                        }
                        catch (Exception)
                        {
                            return NotFound();
                        }
                    }
                }
            }

            return NotFound();
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }

        public IActionResult DownloadExcel()
        {

            var bookings = _context.Bookings
                 .Include(b => b.Area)
                 .Include(b => b.Area.Rooms)
                 .Include(b => b.Room)
                 .Include(b => b.User);

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");
            Sheet.Cells["A1"].Value = "Name";
            Sheet.Cells["B1"].Value = "Department";
            Sheet.Cells["C1"].Value = "Address";
            Sheet.Cells["D1"].Value = "City";
            Sheet.Cells["E1"].Value = "Country";
            int row = 2;
            foreach (var item in bookings)
            {

                Sheet.Cells[string.Format("A{0}", row)].Value = item.User.Email;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Room.name;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Area.AreaName;
                Sheet.Cells[string.Format("D{0}", row)].Value = item.StartdateTime;
                Sheet.Cells[string.Format("E{0}", row)].Value = item.EndDateTime;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
           
            
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.Headers["content-disposition"] = "attachment;  filename=Bookings.xlsx";
            
                
            
                return NoContent();
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





