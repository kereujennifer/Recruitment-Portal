using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Auth.Models
{
    public class DocumentUploads
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string FilePath { get; set; }
        public string FileSize { get; set; }
        public string userID { get; set; }
        public List<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

    }
}
