using Auth.Models.Database;
using Auth.Models.ViewModels;
using Auth.Data;
using Auth.Services;
using Auth.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Controllers
{
    public class Settings : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly Iservice _service;
        public Settings(ApplicationDbContext context, Iservice service)
        {
            _context = context;
            _service = service;

        }
        public async Task<IActionResult> Index()
        {
            var model = new SettingVM();
            var settings = await _context.Settings.FirstOrDefaultAsync();
            if (settings != null)
            {
                model.Id = settings.Id;
                model.EmailSettings = settings.EmailSettings;
                model.SMTPPort = settings.SMTPPort;
                model.SMTPUserName = settings.SMTPUserName;
                model.SMTPPassword = settings.SMTPPassword;
                model.SenderFromEmail = settings.SenderFromEmail;
                model.SenderFromPassword = settings.SenderFromPassword;
                model.TeamVision = settings.TeamVision;
                model.ClientName = settings.ClientName;
                model.TagLine = settings.TagLine;
                model.PrimaryColor = settings.PrimaryColor;
                model.SecondaryColor = settings.SecondaryColor;
                model.ContactUsEmail = settings.ContacUsEmail;
                model.FilePath = settings.LayoutImage;
                model.TeamFile = settings.TeamLogo;
            }

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SMTPSettings(SettingVM settings)
        {
            var settingsSetting = await _context.Settings.FirstOrDefaultAsync();
            if (settingsSetting != null)

            {
                settingsSetting.EmailSettings = settings.EmailSettings;
                settingsSetting.SenderFromEmail = settings.SenderFromEmail;
                settingsSetting.SenderFromPassword = settings.SenderFromPassword;

                settingsSetting.SMTPUserName = settings.SMTPUserName;
                settingsSetting.SMTPPassword = settings.SMTPPassword;
                settingsSetting.SMTPPort = settings.SMTPPort;
                settingsSetting.Radio = settings.Radio;

            }
            else
            {
                var model = new Setting()
                {
                    SMTPPassword = settings.SMTPPassword,
                    SenderFromEmail = settings.SenderFromEmail,
                    SenderFromPassword = settings.SenderFromPassword,
                    SMTPPort = settings.SMTPPort,
                    SMTPUserName = settings.SMTPUserName,
                    EmailSettings = settings.EmailSettings,
                    Radio = settings.Radio,
                };

                _context.Settings.Add(model);
            }
            {
                TempData[Constants.Success] = "SMTPSettings Updated successfully!";
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> GeneralSettings(SettingVM setting)
        {
            if (ModelState.IsValid)
            {
                string teamLogo = null;
                string companyLogo = null;

                if (
                    setting.TeamLogo != null)
                {
                    var jFile = _service.UploadFile(setting.TeamLogo).Result;
                    // var jdata = JsonConvert.DeserializeObject<ReturnData<UploadViewModel>>(jFile);
                    if (jFile != null)
                    {
                        teamLogo = jFile;
                    }
                }
                if (
                    setting.CompanyLogo != null)
                {
                    var clogo = _service.UploadFile(setting.CompanyLogo).Result;
                    if (clogo != null)
                    {
                        companyLogo = clogo;
                    }
                }
                if (setting.Id == 0)
                {
                    var sett = new Setting
                    {
                        TeamVision = setting.TeamVision,
                        PrimaryColor = setting.PrimaryColor,
                        SecondaryColor = setting.SecondaryColor,
                        ClientName = setting.ClientName,
                        TagLine = setting.TagLine,
                        ContacUsEmail = setting.ContactUsEmail,

                    };
                    if (teamLogo != null)
                    {
                        sett.TeamLogo = teamLogo;
                        sett.FilePath = teamLogo;
                    }
                    if (companyLogo != null)
                    {
                        sett.LayoutImage = companyLogo;
                        sett.FilePath = "~/uploads/@Model.FilePath";
                    }
                    _context.Settings.Add(sett);
                }
                else
                {
                    var sett = _context.Settings.Where(t => t.Id == setting.Id).FirstOrDefault();
                    if (sett != null)
                    {
                        sett.TeamVision = setting.TeamVision;
                        sett.PrimaryColor = setting.PrimaryColor;
                        sett.SecondaryColor = setting.SecondaryColor;
                        sett.ClientName = setting.ClientName;
                        sett.TagLine = setting.TagLine;
                        sett.ContacUsEmail = setting.ContactUsEmail;

                        if (teamLogo != null)
                        {
                            sett.TeamLogo = teamLogo;
                            sett.FilePath = "~/uploads/@Model.FilePath";
                        }
                        if (companyLogo != null)
                        {
                            sett.LayoutImage = companyLogo;
                            sett.FilePath = "~/uploads/@Model.FilePath";
                        }

                    }
                    TempData[Constants.Success] = "settings Updated successfully!";
                }
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }
    }
}
