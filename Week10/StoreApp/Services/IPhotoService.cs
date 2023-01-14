using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace StoreApp.Services
{
    public interface IPhotoService
    {
        Task<Guid> AddPhotoAsync(IFormFile file);

        void RemovePhoto(Guid guid);
    }
}
