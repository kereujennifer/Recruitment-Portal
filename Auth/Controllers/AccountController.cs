using Auth.Data;
using Auth.Models;
using Auth.Services;
using Auth.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly Iservice _service;

        public AccountController(ApplicationDbContext _context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, Iservice service)
        {
            context = _context;
            _service = service;

            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var model = new EditPersonalInformation();
        //    var user = await userManager.GetUserAsync(HttpContext.User);
        //    if (user != null)
        //    {
        //        model.userID = user.UserID;
        //        model.Address = user.Address;
        //        model.Name = user.Name;
        //        model.Email = user.Email;
        //        model.Name = user.Name;
        //        model.Gender = user.Gender;
        //        model.County = user.Address;
        //        model.PhoneNumber = user.PhoneNumber;
        //    }
        //    return View(model);
        //}

 
        
        
     
        //[HttpPost]
        //public async Task<IActionResult> AddorEditProfile(ProfileVm profileVm)
        //{
        //    var user = await userManager.FindByIdAsync(profileVm.UserId);
        //    var file = "";
        //    if (profileVm.file != null)
        //    {
        //        file = await _service.UploadFile(profileVm.file);
        //        user.ImageRef = file;
        //    }
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    user.Email = profileVm.Email;
        //    user.Name = profileVm.Name;
        //    user.JobNo = profileVm.JobNo;
        //    user.JobTitle = profileVm.JobTitle;
        //    user.PhoneNumber = profileVm.PhoneNo;
        //    var res = await userManager.UpdateAsync(user);
        //    if (res.Succeeded)
        //    {
        //        TempData[Constants.Success] = "Profile updated succefully";
        //    }
        //    else
        //    {
        //        TempData[Constants.Error] = "An error Occurred";
        //    }
        //    return RedirectToAction("Index");
        //}
      
        public IActionResult AccessDenied()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
