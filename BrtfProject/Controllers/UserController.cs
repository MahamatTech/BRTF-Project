using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BrtfProject.Models;
using Microsoft.AspNetCore.Authorization;
using BrtfProject.Utilities;
using BrtfProject.Data;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.UI.Services;
using BrtfProject.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Authentication;

namespace BrtfProject.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        //Specialized controller just used to allow an 
        //Authenticated user to maintain their own  account details.

        private readonly BrtfDbContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationDbContext _identityContext;

        public UserController(BrtfDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<RegisterModel> logger, IEmailSender emailSender, ApplicationDbContext identityContext)
        {
            _context = context;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _identityContext = identityContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        // GET: UserAccount
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Details));
        }

        // GET: UserAccount/Details/5
        public async Task<IActionResult> Details()
        {

            var user = await _context.Users
               .Where(c => c.Email == User.Identity.Name)
               .FirstOrDefaultAsync();
            if (user == null)
            {
                return RedirectToAction(nameof(Create));
            }

            return View(user);
        }

        // GET: UserAccount/Create
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> CreateAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            ViewData["ProgramTermId"] = new SelectList(_context.ProgramTerms, "ID", "ProgramInfo");
            ViewData["TermId"] = new SelectList(_context.Terms, "ID", "Code");
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "ID", "UserGroupName");
            return View();
        }

        // POST: UserAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> Create([Bind("StudentID,FirstName,MiddleName,LastName,ProgramTermId,TermId,UserGroupId,Email,LastLevel")] User user, string returnUrl = null)
        {
            ViewData["ProgramTermId"] = new SelectList(_context.ProgramTerms, "ID", "ProgramInfo");
            ViewData["TermId"] = new SelectList(_context.Terms, "ID", "Code");
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "ID", "UserGroupName");
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            try
            {
                if (ModelState.IsValid)
                {
                    //Try to add the new user to the DB, if it succeeds then we move on to attempting to create the user's identity
                    //This is to stop the user's account from being a failed creation and then the program making their login anyway.
                    _context.Add(user);
                    var success = await _context.SaveChangesAsync() > 0;
                    
                    if(success != false)
                    {
                        //Here we create the user's new identity using the input.
                        //It's nothing (significantly) different from the regular register page but it is all done in one motion in the user controller.
                        var u = new IdentityUser { UserName = user.Email, Email = user.Email };
                        var result = await _userManager.CreateAsync(u, Input.Password);
                        if (result.Succeeded)
                        {
                            string msg = "Success: account for " + user.Email + " has been created.";
                            _logger.LogInformation(msg);
                            ViewData["msg"] = msg;

                            return RedirectToAction("Index", "UserIdentity");
                            //_logger.LogInformation("User created a new account with password.");

                            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                            //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                            //var callbackUrl = Url.Page(
                            //    "/Account/ConfirmEmail",
                            //    pageHandler: null,
                            //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                            //    protocol: Request.Scheme);

                            //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                            //if (_userManager.Options.SignIn.RequireConfirmedAccount)
                            //{
                            //    return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                            //}
                            //else
                            //{
                            //    await _signInManager.SignInAsync(user, isPersistent: false);
                            //    return LocalRedirect(returnUrl);
                            //}
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(user);
        }

        // GET: UserAccount/Edit/5
        public async Task<IActionResult> Edit()
        {
            ViewData["ProgramTermId"] = new SelectList(_context.ProgramTerms, "ID", "ProgramInfo");
            ViewData["TermId"] = new SelectList(_context.Terms, "ID", "Code");
            ViewData["UserGroupId"] = new SelectList(_context.UserGroups, "ID", "UserGroupName");

            var user = await _context.Users
                .Where(c => c.Email == User.Identity.Name)
                .FirstOrDefaultAsync();
            if (user == null)
            {
                return RedirectToAction(nameof(Create));
            }
            return View(user);
        }

        // POST: UserAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var userToUpdate = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<User>(userToUpdate, "",
                c => c.FirstName, c => c.MiddleName, c => c.LastName,  c => c.StudentID, c => c.ProgramTermId, c => c.Purge))
            {
                try
                {
                    _context.Update(userToUpdate);
                    await _context.SaveChangesAsync();
                    UpdateUserNameCookie(userToUpdate.FullName);
                    return RedirectToAction(nameof(Details));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. The record you attempted to edit "
                                + "was modified by another user after you received your values.  You need to go back and try your edit again.");
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Something went wrong in the database.");
                }
            }
            return View(userToUpdate);

        }

        // GET: UserAccount/Delete/5
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: UserAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Super-Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            await DeleteIdentityUser(user.Email);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "UserIdentity");
        }

        private async Task DeleteIdentityUser(string Email)
        {
            var userToDelete = await _identityContext.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
            if (userToDelete != null)
            {
                _identityContext.Users.Remove(userToDelete);
                await _identityContext.SaveChangesAsync();
            }
        }

        private void UpdateUserNameCookie(string userName)
        {
            CookieHelper.CookieSet(HttpContext, "userName", userName, 960);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
