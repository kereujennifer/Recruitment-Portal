using Auth.Common;
using Auth.Data;
using Auth.Models.Database;
using Auth.Models.ViewModels.Auth;
using Auth.Services;
using Auth.Common.Email;
using Auth.Data;
using Auth.Models;
using Auth.Models.ViewModels.Auth;
using Auth.Services;
using Auth.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserManagementController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        private readonly IEmailSender emailSender;

        private Iservice _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public UserManagementController(ApplicationDbContext context, IWebHostEnvironment environment, /*Iservice service,*/ UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this._context = context;
            _environment = environment;
            //_service = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }
    
        public IActionResult Index()
        {

            var user = _userManager.Users.Where(u => u.UserName == u.UserName &&
                       u.UserName != "admin@abno.com").ToList();

            return View(user);
        }
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(RegisterVM register)
        {
            try
            {
                var admin = await _userManager.GetUserAsync(User);
                var role = "User";
                var isAdmin = admin != null && await _userManager.IsInRoleAsync(admin, "Admin");
                if (isAdmin)
                {
                    role = register.Role;
                    register.Password = $"{register.FullName.ToUpper()[0]}product@2023";
                }

                var user = new ApplicationUser
                {
                    UserName = register.Email,
                    Email = register.Email,
                    Name = register.FullName,
                    Role=register.Role
                    //PrimaryRole=register.Role
                };

                var result = await _userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var confirmationLink = Url.Action("Confirm", "Auth",
                        new { userId = user.Id, token = token }, Request.Scheme);
                    await emailSender.SendEmailAsync(
                        user.Email,
                        "Email Confirmation",
                        $"Click on the link below to confirm email <br/> <a>{confirmationLink}</a>");
                    if (isAdmin)
                    {
                        TempData[Constants.Success] = "User added successfully!";
                        RedirectToAction("Index", "AuthController");
                    }
                    return RedirectToAction(nameof(Index));
                }
                foreach (var errors in result.Errors)
                {
                    ModelState.AddModelError("", errors.Description);
                }
                return View();
            }
            catch (Exception ex)
            {
                var message = ex.Message;
                TempData[Constants.Success] = "User added successfully";
               
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData[Constants.Error] = "Sorry, User not found!";
                return RedirectToAction("Index");
            }
            await _userManager.DeleteAsync(user);
            TempData[Constants.Success] = "Deleted successful!";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> UpdateUsersStatus(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var message = "";
            if (user == null)
            {
                TempData[Constants.Error] = "Sorry, User not found!";
                return RedirectToAction("Index");
            }
            if (user.Role == "Admin")
            {
                TempData[Constants.Error] = "Sorry, you are not allowed to disable admin!";
                return RedirectToAction("Users");
            }
            if (user.Status)
            {
                user.Status = false;
                message = "Account deactivated successful!";
            }
            else
            {
                user.Status = true;
                message = "Account activated successful!";
            }
            await _userManager.UpdateAsync(user);
            TempData[Constants.Success] = message;
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> AdminResetPassword(string id)
        {
            var model = new ResetPasswordVW();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                TempData[Constants.Error] = "Sorry, User not found!";
                return RedirectToAction("Users");
            }
            model.Id = user.Id;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AdminResetPassword(ResetPasswordVW model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    TempData[Constants.Error] = "Sorry, User not found!";
                    return RedirectToAction("Users");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var result = await _userManager.ResetPasswordAsync(user, code, model.Password);
                if (result.Succeeded)
                {
                    TempData[Constants.Success] = "Password reset successful!";
                    return RedirectToAction("Users");
                }
                TempData[Constants.Error] = "Sorry, could not!";
                return RedirectToAction("Index");
            }
            return View(model);
        }
        public async Task<IActionResult> Deactivate(string id)
        {
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Admin");
            if (isAdmin)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return RedirectToAction(nameof(Index));

                }
                user.Active = false;
                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                    TempData[Constants.Success] = $"{user.Name} deactivated!";
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Approve(string id)
        {
            var isAdmin = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Admin");
            if (isAdmin)
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return RedirectToAction(nameof(Index));

                }
                user.Active = true;
                user.EmailConfirmed = true;
                user.ActivationStatus = true;
                var res = await _userManager.UpdateAsync(user);
                if (res.Succeeded)
                    TempData[Constants.Success] = $"{user.Name} Approved!";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
