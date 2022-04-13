using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Data;
using BrtfProject.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using BrtfProject.Utilities;
using Microsoft.AspNetCore.Authorization;

namespace BrtfProject.Controllers
{
    [Authorize]
    public class RoomsController : Controller
    {
        private readonly BrtfDbContext _context;
        public static bool isenable;
        public RoomsController(BrtfDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Admin, Super-Admin")]
        // GET: Rooms
        public async Task<IActionResult> Index(string SearchString, int? AreaId, int? page, int? pageSizeID, string actionButton, string sortDirection = "asc", string sortField = "Room")
        {
            string[] sortOptions = new[] { "Room", "Area", "Enabled?", "Capacity" };

            PopulateDropDownLists();

            ViewData["Filtering"] = "";  //Assume not filtering

            var rooms =from r in _context.Rooms
                .Include(r => r.Area)
                select r;
            if (AreaId.HasValue)
            {
                rooms = rooms.Where(p => p.AreaId == AreaId);
                ViewData["Filtering"] = " show";
            }
            
            if (!String.IsNullOrEmpty(SearchString))
            {
                rooms = rooms.Where(p => p.name.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = " show";
            }

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

            if (sortField == "Area")
            {
                if (sortDirection == "asc")
                {
                    rooms= rooms
                        .OrderBy(p => p.Area.AreaName);
                }
                else
                {
                    rooms = rooms
                        .OrderByDescending(p => p.Area.AreaName);
                }
            }
            else if (sortField == "Room")
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms
                        .OrderBy(p => p.name);
                }
                else
                {
                    rooms = rooms
                        .OrderByDescending(p => p.name);
                }
            }
            else if (sortField == "Enabled?")
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms
                        .OrderBy(p => p.IsEnable);
                }
                else
                {
                    rooms = rooms
                        .OrderByDescending(p => p.IsEnable);
                }
            }
            else if (sortField == "Capacity")
            {
                if (sortDirection == "asc")
                {
                    rooms = rooms
                        .OrderBy(p => p.capacity);
                }
                else
                {
                    rooms = rooms
                        .OrderByDescending(p => p.capacity);
                }
            }
            //Set sort for next time
            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;

            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<Room>.CreateAsync(rooms.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }
        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }

        [Authorize(Roles = "Admin, Super-Admin")]
        // GET: Rooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Area)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // GET: Rooms/Create
        [Authorize(Roles = "Super-Admin")]
        public IActionResult Create()
        {
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> Create([Bind("ID,name,description,IsEnable,capacity,EMail,RepeatEndDate,AreaId")] Room room)
        {
            if (ModelState.IsValid)
            {
                _context.Add(room);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName", room.AreaId);
            return View(room);
        }

        // GET: Rooms/Edit/5
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return NotFound();
            }
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName", room.AreaId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,name,description,IsEnable,capacity,EMail,RepeatEndDate,AreaId")] Room room)
        {
            if (id != room.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(room);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomExists(room.ID))
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
            ViewData["AreaId"] = new SelectList(_context.Areas, "ID", "AreaName", room.AreaId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = await _context.Rooms
                .Include(r => r.Area)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
        {
            //Note: This is a very basic example and has 
            //no ERROR HANDLING.  It also assumes that
            //duplicate values are allowed, both in the 
            //uploaded data and the DbSet.
            ExcelPackage excel;
            using (var memoryStream = new MemoryStream())
            {
                await theExcel.CopyToAsync(memoryStream);
                excel = new ExcelPackage(memoryStream);
            }
            var workSheet = excel.Workbook.Worksheets[0];
            var start = workSheet.Dimension.Start;
            var end = workSheet.Dimension.End;

            //Start a new list to hold imported objects
            List<Room> rooms = new List<Room>();

           

            for (int row = start.Row + 1; row <= end.Row; row++)
            {
                
                if (workSheet.Cells[row, 1].Text != "" && workSheet.Cells[row, 2].Text != "" && workSheet.Cells[row, 3].Text != "" && workSheet.Cells[row, 4].Text != "" && workSheet.Cells[row, 5].Text != "" )
                { // Row by row...
                    
                    if(workSheet.Cells[row, 5].Text.ToLower() == "true")
                    {
                        isenable = true;
                    }
                    else if(workSheet.Cells[row, 5].Text.ToLower() == "false")
                    {
                        isenable = false;
                    }
                    Room a = new Room
                    {
                        name = workSheet.Cells[row, 1].Text,
                        capacity =Convert.ToInt32(workSheet.Cells[row, 3].Text),
                        AreaId = Convert.ToInt32(workSheet.Cells[row, 4].Text),
                        IsEnable = isenable
                    };

                    rooms.Add(a);
                }
            }
            _context.Rooms.AddRange(rooms);
            _context.SaveChanges();
            return RedirectToAction("Index","Rooms");
        }

        private SelectList AreaSelectList(int? selectedId)
        {
            return new SelectList(_context.Areas
                .OrderBy(d => d.AreaName), "ID", "AreaName", selectedId);
        }
        private void PopulateDropDownLists(Room room = null)
        {
            ViewData["AreaId"] = AreaSelectList(room?.AreaId);
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.ID == id);
        }
    }
}
