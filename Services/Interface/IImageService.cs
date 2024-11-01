using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IImageService
    {
        Task<string> UploadKoiImage(IFormFile file, int KoiId);
        Task<string> UploadKoiFishyImage(IFormFile file, int KoiFishyId);
    }
}
