using BusinessObjects.Models;
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
        Task<IEnumerable<Image>> GetAllImages();
        Task<List<string>> UploadKoiImage(List<IFormFile> files, int KoiId);
        Task<List<string>> UploadKoiFishyImage(List<IFormFile> files, int KoiFishyId);
        Task<List<string>> UploadConsignmentImage(List<IFormFile> files, int ConsignmentId);
        Task<string> UploadCertificateImage(IFormFile file, int originCertificateId);
        Task<bool> DeleteImageAsync(string urlImage, string path);
    }
}
