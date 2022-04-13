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
using System.Drawing;

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
        public async Task<IActionResult> Index(string actionButton,string SearchFirstName,string SearchLastName,string SearchProgramTerm,
           string SearchEmail , int? page, int? pageSizeID, string sortField = "User", string sortDirection = "asc")
        {
            string[] sortOptions = new[] { "Student ID", "Full Name", "Email" };

            var users = (from u in _context.Users
                .Include(u => u.ProgramTerm)
                select u).AsNoTracking();

            //if (String.IsNullOrEmpty(SearchStudentID))
            //{
            //    users = users.Where(p => p.StudentID.ToString().Contains(SearchStudentID));
            //}
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
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["ProgramTermId"] = new SelectList(_context.ProgramTerms, "ID", "ProgramInfo");
            ViewData["TermId"] = new SelectList(_context.Terms, "ID", "Code");
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "ID", "UserGroupName");

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
        [Authorize(Roles = "Super-Admin")]
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

        [Authorize(Roles = "Super-Admin")]
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
                List<ProgramTerm> pts = new List<ProgramTerm>();
                List<Term> ts = new List<Term>();

                for (int row = start.Row; row <= end.Row; row++)
                {
                    //Check if columns 1, 2, 4, 5, 6, 7, 8 and 10 are empty
                    //These columns are important as they are required fields
                    //If columns are empty, the program skips to the next row
                    if (workSheet.Cells[row, 1].Text != "" && workSheet.Cells[row, 2].Text != "" && workSheet.Cells[row, 4].Text != "" && workSheet.Cells[row, 5].Text != "" && workSheet.Cells[row, 6].Text != "" && workSheet.Cells[row, 7].Text != "" && workSheet.Cells[row, 8].Text != "" && workSheet.Cells[row, 9].Text != "" && workSheet.Cells[row, 10].Text != "")
                    {
                        if (workSheet.Cells[row, 1].Text.Contains("ID") | workSheet.Cells[row, 2].Text.Contains("Name") |
                            workSheet.Cells[row, 4].Text.Contains("Name") | workSheet.Cells[row, 7].Text.Contains("Email") | 
                            workSheet.Cells[row, 10].Text.Contains("Term"))
                        {
                            continue;
                        }
                        else
                        {
                            //Create a string for the Program Term spot
                            //Take the Program Term and find it using firstordefaultasync
                            //Then assign that Program ID to the user
                            int? TId = 1;
                            int? PId = 1;
                            try
                            {
                                Term b = await _context.Terms.FirstOrDefaultAsync(u => u.Code == Int32.Parse(workSheet.Cells[row, 10].Text));
                                if(b == null)
                                {
                                    TId = null;
                                }
                                else
                                {
                                    TId = b.ID;
                                }
                                
                                ProgramTerm c = await _context.ProgramTerms.FirstOrDefaultAsync(u => u.ProgramCode == workSheet.Cells[row, 5].Text);
                                if(c == null)
                                {
                                    PId = null;
                                }
                                else
                                {
                                    PId = c.ID;
                                }
                            }
                            catch (NullReferenceException) { }

                            if (TId == null)
                            {
                                var newTerm = new Term { Code = Int32.Parse(workSheet.Cells[row, 10].Text) };
                                TId = newTerm.ID;
                                ts.Add(newTerm);
                            }
                            if (PId == null)
                            {
                                var newProgram = new ProgramTerm { ProgramCode = workSheet.Cells[row, 5].Text, ProgramInfo = workSheet.Cells[row, 6].Text, UserGroupId = 1 };
                                PId = newProgram.ID;
                                pts.Add(newProgram);
                            }

                            bool lvl = false;
                            if(workSheet.Cells[row, 9].Text == "Y" | workSheet.Cells[row, 9].Text == "y")
                            {
                                lvl = true;
                            }
                            else
                            {
                                lvl = false;
                            }

                            //Setting the current user being made's password.
                            //If column 12 does have something in it, the password is set to that
                            //If it doesn't, password is password.
                            string pswd = "password";
                            if(workSheet.Cells[row, 12].Text != "")
                            {
                                pswd = workSheet.Cells[row, 12].Text;
                            }

                            //Creation of the user using the uploaded data
                            User a = new User
                            {
                                StudentID = Int32.Parse(workSheet.Cells[row, 1].Text),
                                FirstName = workSheet.Cells[row, 2].Text,
                                MiddleName = workSheet.Cells[row, 3].Text,
                                LastName = workSheet.Cells[row, 4].Text,
                                ProgramTermId = Convert.ToInt32(PId),
                                Email = workSheet.Cells[row, 7].Text,
                                TermLevel = Int32.Parse(workSheet.Cells[row, 8].Text),
                                LastLevel = lvl,
                                TermId = Convert.ToInt32(TId),
                                UserGroupId = 1
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
                                    var result = await _userManager.CreateAsync(user, pswd);
                                }
                                catch
                                {
                                    return NotFound();
                                }
                            }
                        }
                    }
                }
                _context.Terms.AddRange(ts);
                await _context.SaveChangesAsync();

                _context.ProgramTerms.AddRange(pts);
                await _context.SaveChangesAsync();

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

                //Headings
                workSheet.Cells[2, 1].Value = "Student ID";
                workSheet.Cells[2, 2].Value = "First Name";
                workSheet.Cells[2, 3].Value = "Middle Name";
                workSheet.Cells[2, 4].Value = "Last Name";
                workSheet.Cells[2, 5].Value = "Program Code";
                workSheet.Cells[2, 6].Value = "Program Description";
                workSheet.Cells[2, 7].Value = "Email";
                workSheet.Cells[2, 8].Value = "Student Level";
                workSheet.Cells[2, 9].Value = "Last Level (Y/N)";
                workSheet.Cells[2, 10].Value = "Program Term";
                workSheet.Cells[2, 12].Value = "Birth Date";

                //Sample Data
                workSheet.Cells[3, 1].Value = "4362670";
                workSheet.Cells[3, 2].Value = "Cat";
                workSheet.Cells[3, 3].Value = "Sun";
                workSheet.Cells[3, 4].Value = "Al-Bahrani";
                workSheet.Cells[3, 5].Value = "PO122";
                workSheet.Cells[3, 6].Value = "Broadcasting: Radion, TV & Film";
                workSheet.Cells[3, 7].Value = "cstevens@ncstudents.niagaracollege.ca";
                workSheet.Cells[3, 8].Value = "1";
                workSheet.Cells[3, 9].Value = "N";
                workSheet.Cells[3, 10].Value = "1204";
                workSheet.Cells[3, 12].Value = "19941223";

                workSheet.Cells[4, 1].Value = "9311011";
                workSheet.Cells[4, 2].Value = "Sterling";
                workSheet.Cells[4, 4].Value = "Archer";
                workSheet.Cells[4, 5].Value = "PO163";
                workSheet.Cells[4, 6].Value = "Presentation / Radio";
                workSheet.Cells[4, 7].Value = "sarcher4@ncstudents.niagaracollege.ca";
                workSheet.Cells[4, 8].Value = "1";
                workSheet.Cells[4, 9].Value = "N";
                workSheet.Cells[4, 10].Value = "1214";
                workSheet.Cells[4, 12].Value = "19930806";

                workSheet.Cells[5, 1].Value = "9508725";
                workSheet.Cells[5, 2].Value = "Matt";
                workSheet.Cells[5, 3].Value = "Elmi Johanna";
                workSheet.Cells[5, 4].Value = "Parker";
                workSheet.Cells[5, 5].Value = "PO164";
                workSheet.Cells[5, 6].Value = "TV Production";
                workSheet.Cells[5, 7].Value = "maparker1@ncstudents.niagaracollege.ca";
                workSheet.Cells[5, 8].Value = "3";
                workSheet.Cells[5, 9].Value = "N";
                workSheet.Cells[5, 10].Value = "1214";
                workSheet.Cells[5, 12].Value = "20000305";

                workSheet.Cells[6, 1].Value = "4407393";
                workSheet.Cells[6, 2].Value = "James";
                workSheet.Cells[6, 3].Value = "Bullough";
                workSheet.Cells[6, 4].Value = "Lansing";
                workSheet.Cells[6, 5].Value = "PO165";
                workSheet.Cells[6, 6].Value = "Film Production";
                workSheet.Cells[6, 7].Value = "jblansing@ncstudents.niagaracollege.ca";
                workSheet.Cells[6, 8].Value = "5";
                workSheet.Cells[6, 9].Value = "N";
                workSheet.Cells[6, 10].Value = "1204";
                workSheet.Cells[6, 12].Value = "19990901";

                workSheet.Cells[7, 1].Value = "4105233";
                workSheet.Cells[7, 2].Value = "Judy";
                workSheet.Cells[7, 3].Value = "Ugonna";
                workSheet.Cells[7, 4].Value = "Garland";
                workSheet.Cells[7, 5].Value = "PO198";
                workSheet.Cells[7, 6].Value = "Acting for TV & Film";
                workSheet.Cells[7, 7].Value = "jgarland@ncstudents.niagaracollege.ca";
                workSheet.Cells[7, 8].Value = "2";
                workSheet.Cells[7, 9].Value = "N";
                workSheet.Cells[7, 10].Value = "1214";
                workSheet.Cells[7, 12].Value = "19970420";

                workSheet.Cells[8, 1].Value = "4242885";
                workSheet.Cells[8, 2].Value = "Marge";
                workSheet.Cells[8, 4].Value = "Simpson";
                workSheet.Cells[8, 5].Value = "PO795";
                workSheet.Cells[8, 6].Value = "Digital Photography";
                workSheet.Cells[8, 7].Value = "masimpson@ncstudents.niagaracollege.ca";
                workSheet.Cells[8, 8].Value = "1";
                workSheet.Cells[8, 9].Value = "N";
                workSheet.Cells[8, 10].Value = "1214";
                workSheet.Cells[8, 12].Value = "19941223";

                workSheet.Cells[9, 1].Value = "4035763";
                workSheet.Cells[9, 2].Value = "Sarah";
                workSheet.Cells[9, 3].Value = "Wing-Hay";
                workSheet.Cells[9, 4].Value = "Slean";
                workSheet.Cells[9, 5].Value = "P6801";
                workSheet.Cells[9, 6].Value = "Joint BSc Game Programming";
                workSheet.Cells[9, 7].Value = "sslean13@ncstudents.niagaracollege.ca";
                workSheet.Cells[9, 8].Value = "4";
                workSheet.Cells[9, 9].Value = "N";
                workSheet.Cells[9, 10].Value = "1204";
                workSheet.Cells[9, 12].Value = "19960922";

                workSheet.Cells[10, 1].Value = "4444312";
                workSheet.Cells[10, 2].Value = "Morag";
                workSheet.Cells[10, 3].Value = "Leah-Grace";
                workSheet.Cells[10, 4].Value = "Smith";
                workSheet.Cells[10, 5].Value = "P6800";
                workSheet.Cells[10, 6].Value = "Join BA Game Design";
                workSheet.Cells[10, 7].Value = "msmith11@ncstudents.niagaracollege.ca";
                workSheet.Cells[10, 8].Value = "6";
                workSheet.Cells[10, 9].Value = "Y";
                workSheet.Cells[10, 10].Value = "1204";
                workSheet.Cells[10, 12].Value = "19951015";

                workSheet.Cells[11, 1].Value = "4398123";
                workSheet.Cells[11, 2].Value = "Akira";
                workSheet.Cells[11, 3].Value = "Kaur";
                workSheet.Cells[11, 4].Value = "Kurosawa";
                workSheet.Cells[11, 5].Value = "P0441";
                workSheet.Cells[11, 6].Value = "Game Development";
                workSheet.Cells[11, 7].Value = "akurosawa@ncstudents.niagaracollege.ca";
                workSheet.Cells[11, 8].Value = "3";
                workSheet.Cells[11, 9].Value = "N";
                workSheet.Cells[11, 10].Value = "1204";
                workSheet.Cells[11, 12].Value = "20020616";

                workSheet.Cells[12, 1].Value = "9470695";
                workSheet.Cells[12, 2].Value = "Zhuo Chang";
                workSheet.Cells[12, 4].Value = "Wu";
                workSheet.Cells[12, 5].Value = "P0474";
                workSheet.Cells[12, 6].Value = "CST – Network and Cloud Tech";
                workSheet.Cells[12, 7].Value = "zcwu@ncstudents.niagaracollege.ca";
                workSheet.Cells[12, 8].Value = "3";
                workSheet.Cells[12, 9].Value = "N";
                workSheet.Cells[12, 10].Value = "1214";
                workSheet.Cells[12, 12].Value = "19950723";

                using (ExcelRange headings = workSheet.Cells[2, 1, 2, 12])
                {
                    headings.Style.Font.Bold = true;
                    var fill = headings.Style.Fill;
                    fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    fill.BackgroundColor.SetColor(Color.LightBlue);
                }
                workSheet.Cells.AutoFitColumns();

                workSheet.Cells[1, 1].Value = "User Data Template";
                using(ExcelRange Rng = workSheet.Cells[1, 1, 1, 6])
                {
                    Rng.Merge = true;
                    Rng.Style.Font.Bold = true;
                    Rng.Style.Font.Size = 18;
                    Rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                }

                using(ExcelRange Desc = workSheet.Cells[2, 13, 6, 16])
                {
                    Desc.Merge = true;
                    Desc.Style.Font.Italic = true;
                    Desc.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                }
                workSheet.Cells[2, 13, 6, 16].Value = "Birth date should be written as YYYYMMDD, without any slashes. If the birth date is left blank, then the user's password will be left as password and not their birth date.";

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
