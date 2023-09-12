using Auth.Data;
using Auth.Models;
using Auth.Models.ViewModels;
using Auth.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        private IWebHostEnvironment _environment;
        private Iservice _service;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger,/*Iservice service,*/ UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            this._context = context;
            //_service = service;
            _userManager = userManager;
            _signInManager = signInManager;
        }
      

        public IActionResult Index()
        {
            var featuredJobs = _context.Jobs
.Where(job => job.IsFeatured)
.ToList(); 
            ViewBag.FeaturedJobs = featuredJobs;
            return View();
        }
        public IActionResult Dashboard()
        {


            // Retrieve all jobs, both featured and applied
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;


            var user = _userManager.Users.Where(u => u.UserName == u.UserName &&
                         u.UserName != "admin@abno.com")

                        .Take(5).ToList();
            //var product = _context.Users.ToList();
            var jobs = _context.Jobs.ToList().Count;
            var myApplications = _context.JobApplications.Where(a => a.UserId == userId).ToList().Count;
            var appliedJobIds = _context.JobApplications
                    .Where(ja => ja.UserId == userId)
                    .Select(ja => ja.JobId)
                    .ToList();


            var featuredJobs = _context.Jobs
          .Where(job => job.IsFeatured && !appliedJobIds.Contains(job.Id))
          .ToList();
            var otherJobs = _context.Jobs.Where(job => !job.IsFeatured && !appliedJobIds.Contains(job.Id)).ToList();
            var appliedJobs = _context.Jobs
                   .Where(job => appliedJobIds.Contains(job.Id))
                   .ToList();
            var applications = _context.JobApplications.ToList().Count;
            ViewBag.FeaturedJobs = featuredJobs;
            ViewBag.AppliedJobs = appliedJobs;
            ViewBag.AppliedJobIds = appliedJobIds;
            ViewBag.OtherJobs = otherJobs;

            //var upctiveProduct = projects.ToList().Count;
            var model = new DashboardVM
            {
               
                Users = user,
                Jobs = jobs,
                MyApplications=myApplications,
                Applications = applications,
         
        };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
