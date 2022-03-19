using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Models;
using BrtfProject.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using Microsoft.AspNetCore.Http.Features;
using BrtfProject.Utilities;

namespace BrtfProject.Controllers
{
    public class UserIdentityController : Controller
    {
        private readonly BrtfDbContext _context;
        private readonly ApplicationDbContext _identityContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserIdentityController(BrtfDbContext context, ApplicationDbContext identityContext, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _identityContext = identityContext;
            _userManager = userManager;
        }

        // GET: UserIdentity
        [Authorize(Roles = "Admin, Super-Admin")]
        public async Task<IActionResult> Index(string actionButton,  string SearchFirstName,string SearchLastName,string SearchProgramTerm,
           string SearchEmail , int? page, int? pageSizeID, string sortField = "User", string sortDirection = "asc")
        {
            string[] sortOptions = new[] { "Student ID", "Full Name", "Email" };

            var users = (from u in _context.Users
                .Include(u => u.ProgramTerm)
                select u).AsNoTracking();


            if (!String.IsNullOrEmpty(SearchFirstName))
            {
                users = users.Where(p => p.FirstName.ToUpper().Contains(SearchFirstName.ToUpper()));
            }
            if (!String.IsNullOrEmpty(SearchLastName))
            {
                users = users.Where(p => p.LastName.ToUpper().Contains(SearchLastName.ToUpper()));
            }
            if (!String.IsNullOrEmpty(SearchProgramTerm))
            {
                users = users.Where(p => p.ProgramTerm.ProgramInfo.ToUpper().Contains(SearchProgramTerm.ToUpper()));
            }
            if (!String.IsNullOrEmpty(SearchEmail))
            {
                users = users.Where(p => p.Email.ToUpper().Contains(SearchEmail.ToUpper()));
            }




            if (!String.IsNullOrEmpty(actionButton))
            {
                if (sortOptions.Contains(actionButton))
                {
                    if(actionButton == sortField)
                    {
                        sortDirection = sortDirection == "asc" ? "desc" : "asc";
                    }
                    sortField = actionButton;
                }
            }

            if (sortField == "Student ID")
            {
                if (sortDirection == "asc")
                {
                    users = users.OrderBy(u => u.StudentID);
                }
                else
                {
                    users = users.OrderByDescending(u => u.StudentID);
                }
            }
            else if (sortField == "Full Name")
            {
                if (sortDirection == "asc")
                {
                    users = users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName).ThenBy(u => u.MiddleName);
                }
                else
                {
                    users = users.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName).ThenByDescending(u => u.MiddleName);
                }
            }
            else
            {
                if (sortDirection == "asc")
                {
                    users = users.OrderBy(u => u.Email);
                }
                else
                {
                    users = users.OrderByDescending(u => u.Email);
                }
            }

            ViewData["sortField"] = sortField;
            ViewData["sortDirection"] = sortDirection;


            //Handle Paging
            int pageSize = PageSizeHelper.SetPageSize(HttpContext, pageSizeID, ControllerName());
            ViewData["pageSizeID"] = PageSizeHelper.PageSizeList(pageSize);
            var pagedData = await PaginatedList<User>.CreateAsync(users.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }
        private string ControllerName()
        {
            return this.ControllerContext.RouteData.Values["controller"].ToString();
        }
        //// GET: UserIdentity/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var user = await _context.Users
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}

        // GET: UserIdentity/Edit/5
        [Authorize(Roles = "Admin, Super-Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["ProgramTermId"] = new SelectList(_context.ProgramTerms, "ID", "ProgramInfo");

            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: UserIdentity/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Super-Admin")]
        public async Task<IActionResult> Edit(int id, bool Purge)
        {
            var userToUpdate = await _context.Users
                .FirstOrDefaultAsync(u => u.ID == id);

            if (userToUpdate == null)
            {
                return NotFound();
            }

            if(userToUpdate.Purge == false & Purge == true)
            {
                await DeleteIdentityUser(userToUpdate.Email);
            }

            if (await TryUpdateModelAsync<User>(userToUpdate, "", 
                u => u.FirstName, u => u.LastName, u => u.StudentID, u => u.ProgramTermId, u => u.Purge))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(userToUpdate);
        }

        private async Task DeleteIdentityUser(string Email)
        {
            var userToDelete = await _identityContext.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
            if(userToDelete != null)
            {
                _identityContext.Users.Remove(userToDelete);
                await _identityContext.SaveChangesAsync();
            }
        }
        [HttpPost]

        [Authorize(Roles = "Admin, Super-Admin")]
        public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
        {
            ExcelPackage excel;
            if(theExcel != null)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await theExcel.CopyToAsync(memoryStream);
                    excel = new ExcelPackage(memoryStream);
                }
                var workSheet = excel.Workbook.Worksheets[0];
                var start = workSheet.Dimension.Start;
                var end = workSheet.Dimension.End;

                //Start a new list to hold imported objects
                List<User> users = new List<User>();

                for (int row = start.Row; row <= end.Row; row++)
                {
                    //Check if columns 1, 2, 4, 7, and 10 are empty
                    //These columns are important as they are required fields
                    //If columns are empty, the program skips to the next row
                    if (workSheet.Cells[row, 1].Text != "" && workSheet.Cells[row, 2].Text != "" && workSheet.Cells[row, 4].Text != "" && workSheet.Cells[row, 7].Text != "" && workSheet.Cells[row, 10].Text != "")
                    {
                        if (workSheet.Cells[row, 1].Text.Contains("ID") | workSheet.Cells[row, 2].Text.Contains("Name") | workSheet.Cells[row, 7].Text.Contains("Email") | workSheet.Cells[row, 1].Text.Contains("Term"))
                        {
                            continue;
                        }
                        else
                        {
                            //Create a string for the Program Term spot
                            //Take the Program Term and find it using firstordefaultasync
                            //Then assign that Program ID to the user
                            string PTerm = workSheet.Cells[row, 10].Text;
                            ProgramTerm b = await _context.ProgramTerms.FirstOrDefaultAsync(u => u.Term == PTerm);
                            int PId;
                            try
                            {
                                PId = b.ID;
                            }
                            catch
                            {
                                PId = 1;
                            }

                            //Creation of the user using the uploaded data
                            User a = new User
                            {
                                StudentID = Int32.Parse(workSheet.Cells[row, 1].Text),
                                FirstName = workSheet.Cells[row, 2].Text,
                                MiddleName = workSheet.Cells[row, 3].Text,
                                LastName = workSheet.Cells[row, 4].Text,
                                Email = workSheet.Cells[row, 7].Text,
                                ProgramTermId = PId
                            };

                            //This section is for checking the user after they have been initialized.
                            //badCheck is used as a way to determine if the user can or cannot be put in.
                            //If the user's ID or Email is already in the system, they are not let in.
                            //If the user's Email is not from niagara college, they are not let in.
                            bool badCheck = false;

                            foreach (User u in _context.Users)
                            {
                                if (u.StudentID == a.StudentID | u.Email == a.Email)
                                {
                                    badCheck = true;
                                }
                            }

                            if (!a.Email.Contains("@ncstudents.niagaracollege.ca"))
                            {
                                badCheck = true;
                            }

                            if (badCheck != true)
                            {
                                try
                                {
                                    users.Add(a);
                                    var user = new IdentityUser { UserName = a.Email, Email = a.Email };
                                    var result = await _userManager.CreateAsync(user, "password");
                                }
                                catch
                                {
                                    return NotFound();
                                }
                            }
                        }
                    }
                }
                _context.Users.AddRange(users);
                _context.SaveChanges();
                
            }
            return RedirectToAction("Index", "UserIdentity");
        }

        [Authorize(Roles = "Admin, Super-Admin")]
        public IActionResult DownloadTemplate()
        {
            using (ExcelPackage excel = new ExcelPackage())
            {
                var workSheet = excel.Workbook.Worksheets.Add("User Template");

                workSheet.Cells[1, 1].Value = "Student ID";
                workSheet.Cells[1, 2].Value = "First Name";
                workSheet.Cells[1, 3].Value = "Middle Name";
                workSheet.Cells[1, 4].Value = "Last Name";
                workSheet.Cells[1, 7].Value = "Email";
                workSheet.Cells[1, 10].Value = "Program Term";

                var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
                if (syncIOFeature != null)
                {
                    syncIOFeature.AllowSynchronousIO = true;
                    using (var memoryStream = new MemoryStream())
                    {
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.Headers["content-disposition"] = "attachment;  filename=Template.xlsx";
                        excel.SaveAs(memoryStream);
                        memoryStream.WriteTo(Response.Body);
                    }
                }
                else
                {
                    try
                    {
                        Byte[] theData = excel.GetAsByteArray();
                        string filename = "Template.xlsx";
                        string mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        return File(theData, mimeType, filename);
                    }
                    catch (Exception)
                    {
                        return NotFound();
                    }
                }
            }
            return NotFound();
        }
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
