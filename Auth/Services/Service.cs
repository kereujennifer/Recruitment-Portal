using Auth.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class Service : Iservice
    {
        private IWebHostEnvironment _environment;

        public Service( IWebHostEnvironment environment)
        {
            _environment= environment;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            string filename = null;
            if (file != null)
            {
                string uploadDir = Path.Combine(_environment.WebRootPath, "uploads");
                filename = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadDir, filename);
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }

            return filename;
        }
    }
}
