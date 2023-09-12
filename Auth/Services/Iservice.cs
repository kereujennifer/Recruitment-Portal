
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Auth.Services
{
    public interface Iservice
    {
        Task<string> UploadFile(IFormFile file);
       
    }
}
