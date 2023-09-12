
using Auth.Models.ViewModels.Auth;
using Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System.Threading.Tasks;

using System;
using Microsoft.Extensions.Logging;
using Auth.Data;
using Auth.Models;
using Auth.Common;
using Auth.Common.Email;

namespace Auth.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly Iservice _service;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ILogger<AuthController> logger;
        private readonly IEmailSender emailSender;

        public AuthController(
            UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            SignInManager<ApplicationUser> signInManager,
              //IEmailSender emailSender,
              ILogger<AuthController>logger,
            Iservice service)
        {
            this.userManager = userManager;
            this._context = context;
            this.signInManager = signInManager;
            this.logger = logger;
            _service = service;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(login.UserName);
                if (user != null && !user.EmailConfirmed && await userManager.CheckPasswordAsync(user, login.Password))
                {
                    TempData[Constants.Error] = "Your email is not confirmed!";
                    return View();
                }

                if (user != null && !user.Active)
                {
                    return RedirectToAction(nameof(Verification));
                }

                var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, isPersistent: login.RememberMe, false);
                if (result.Succeeded)
                {
                    user.IsLoggedIn = true;
                    await userManager.UpdateAsync(user);
                    TempData[Constants.Success] = "Login successful!";
                    return RedirectToAction("Dashboard", "Home");
                }
               
                ModelState.AddModelError(string.Empty, "Wrong password or username!");
            }
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            try
            {
                var admin = await userManager.GetUserAsync(User);
                var role = "User";
                var isAdmin = admin != null && await userManager.IsInRoleAsync(admin, "Admin");
                if (isAdmin) {
                    role = register.Role;
                    register.Password = $"{register.FullName.ToUpper()[0]}product@2023";
                }
            
                var user = new ApplicationUser
                {
                    UserName = register.Email,
                    Email = register.Email,            
                    Name = register.FullName,
                    PhoneNumber=register.PhoneNumber,
                    //PrimaryRole=register.Role
                };

               var result = await userManager.CreateAsync(user, register.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
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
                    return RedirectToAction(nameof(EmailConfirmation));
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
                TempData[Constants.Success] = "Registered, Kindly Confirm your email";
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Confirm(string userId, string token)
        {
            if (userId == null && token == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData[Constants.Warning] = $"User with id {userId} not found!";
                return RedirectToAction("Index", "Home");
            }
            var result = await userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                TempData[Constants.Success] = "Email confirmed proceed to login!";
                RedirectToAction(nameof(Login));
            }
            TempData[Constants.Error] = "Email not confirmed";
            return RedirectToAction(nameof(EmailConfirmation));
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            var user = await userManager.FindByEmailAsync(forgotPassword.Email);
            if (user != null && await userManager.IsEmailConfirmedAsync(user))
            {
                var token = await userManager.GeneratePasswordResetTokenAsync(user);

                var passwordLink = Url.Action("ResetPassword", "Auth",
                    new { email = user.Email, token = token }, Request.Scheme);
                await emailSender.SendEmailAsync(user.Email,
                    "Reset Password", $"Click on the below link to reset your password <br/>" +
                    $"<a>{passwordLink}</a>");

            }
            return View();
        }
        public IActionResult ResetPassword(string email, string token)
        {
            if(email == null || token == null)
            {
                TempData[Constants.Error] = "An error occurred!";
            }
            var model = new ResetPasswordVW
            {
                Email = email,
                Token = token
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVW model)
        {
            var user = await userManager.FindByNameAsync(model.Email);
            var errors = "";
            if(user != null)
            {
                var result = await userManager.ResetPasswordAsync(user,model.Token, model.Password);
                if(result.Succeeded)
                {
                    TempData[Constants.Success] = "Reset successful!";
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    foreach(var error in result.Errors)
                    {
                        errors += error.Description;
                    }
                    TempData[Constants.Error] = errors;
                }
            }
            return View(model);
        }
        public IActionResult EmailConfirmation()
        {
            return View();
        }
        public IActionResult Verification()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            var user  = await userManager.GetUserAsync(User);
            user.IsLoggedIn = false;
          
            await userManager.UpdateAsync(user);
            await signInManager.SignOutAsync();
            //TempData[Constants.Success] = "Logged Out successful!";
            return RedirectToAction("Index", "Home");

        }
    }
}
