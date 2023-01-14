using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace StoreApp.Services
{
    public class PhotoService : IPhotoService
    {
        private const string UploadFolder = "upload";

        private readonly string _uploadPath;

        public PhotoService(IWebHostEnvironment webHostEnvironment) =>
            _uploadPath = Path.Combine(webHostEnvironment.WebRootPath, UploadFolder);

        public async Task<Guid> AddPhotoAsync(IFormFile file)
        {
            var photoGuid = Guid.NewGuid();
            await using var stream = System.IO.File.Create(PhotoPath(photoGuid));
            await file.CopyToAsync(stream);
            return photoGuid;
        }

        public void RemovePhoto(Guid guid)
        {
            try
            {
                File.Delete(PhotoPath(guid));
            }
            catch
            {
                Debug.WriteLine("Error while deleting the file!");
            }
        }

        private string PhotoPath(Guid guid) => Path.Join(_uploadPath, $"{guid}.jpg");
    }
}
