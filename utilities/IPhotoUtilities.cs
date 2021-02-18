using System;
using Microsoft.AspNetCore.Http;
namespace dotnet_core_api.utilities
{
    public interface IPhotoUtilities
    {
        string copyPhoto(IFormFile file, string pathAbsolute);
        bool removePhoto(String nameFile, String pathAbsolute);
    }
}