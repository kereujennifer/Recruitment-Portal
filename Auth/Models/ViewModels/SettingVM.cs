using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models.ViewModels
{
    public class SettingVM
    {

        public int Id { get; set; }

        public string ClientName { get; set; }
        public string TagLine { get; set; }
        public string PrimaryColor { get; set; }


        public string SecondaryColor { get; set; }


        public string InstitutionName { get; set; }



        public IFormFile? CompanyLogo { get; set; }

        public string ContactUsEmail { get; set; }

        public string EmailSettings { get; set; }
        public string SMTPPort { get; set; }

        public string SMTPUserName { get; set; }
        public string SenderFromEmail { get; set; }

        public string SenderFromPassword { get; set; }
        [DataType(DataType.Password)]
        public string SMTPPassword { get; set; }
        public string EmailSetting { get; internal set; }
        public string TeamVision { get; set; }
        public string None { get; set; }
        public string Auto { get; set; }
        public string SsIonConnect { get; set; }
        public string StartTIs { get; set; }
        public string EndTIs { get; set; }
        public string StartTisWhenAvailable { get; set; }
        public IFormFile? TeamLogo { get; set; }
        public string FilePath { get; set; }
        public string? TeamFile { get; set; }
        public string Radio { get; set; }
        public string ReportPassword { get; set; }
    }
}
