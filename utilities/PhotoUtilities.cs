using System.IO;
using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
namespace dotnet_core_api.utilities
{
    public class PhotoUtilities
    {

        public async Task<string> copyPhoto(IFormFile file, string pathAbsolute)
        {
            if (!Directory.Exists(pathAbsolute))
            {
                Directory.CreateDirectory(pathAbsolute);
            }
            // nombre encriptado
            string nameFileEncript = Guid.NewGuid().ToString() + "-" + file.FileName.Replace(" ", "");
            // se crea el stream que seria la ruta y nombre donde se va guardar
            using (FileStream fileStream = System.IO.File.Create(pathAbsolute + nameFileEncript))
            {
                // copia archivo a la ruta
                await file.CopyToAsync(fileStream);
                fileStream.Flush(); // limpia el buffer                
            }
            return nameFileEncript;
        }

        public async Task<bool> removePhoto(string nameFile, string pathAbsolute)
        {
            return await Task.Run<bool>(() =>
          {
              if (System.IO.File.Exists(pathAbsolute + nameFile))
              {
                  System.IO.File.Delete(pathAbsolute + nameFile);
                  return true;
              }
              return false;
          });
        }
    }
}