using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models.Database
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        public string? EmailSettings { get; set; }
        public string? SMTPPort { get; set; }
        public string? SMTServer { get; set; }
        public string? SMTPUserName { get; set; }
        public string? SenderFromEmail { get; set; }
        public string? SenderFromPassword { get; set; }
        [DataType(DataType.Password)]
        public string? SMTPPassword { get; set; }
        public string? ClientName { get; set; }
        public string? TagLine { get; set; }
        public string? TeamVision { get; set; }
        public string? PrimaryColor { get; set; }
        public string? SecondaryColor { get; set; }
        public string? LogoImage { get; set; }
        public string? LayoutImage { get; set; }
        public string? TeamLogo { get; set; }
        public string? ContacUsEmail { get; set; }
        public string? SenderFromName { get; set; }
        public string? FilePath { get; set; }
        public string? Radio { get; set; }
        public string? ReportPassword { get; set; }


    }
}
