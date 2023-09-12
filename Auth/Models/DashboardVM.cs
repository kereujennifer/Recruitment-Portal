using Auth.Models;
using System.Collections.Generic;

namespace Auth.Models.ViewModels
{
    public class DashboardVM
    {
        public int Notifications { get; set; }
        public int Jobs { get; set; }

        public int MyApplications { get; set; }
        public int Applications { get; set; }
        public int JobProfile { get; set; }
        public string userId { get; set; }

       public List<Jobs> AllJobs { get; set; }

        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();
        //public List<Dashboard> dashboards { get; set; } = new List<Dashboard>();







    }
}
