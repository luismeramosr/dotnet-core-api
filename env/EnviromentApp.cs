using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
namespace dotnet_core_api.env
{
    public class EnviromentApp
    {
        public readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;
        private string pathGeneral = "/";
        public string pathVouchersPhotos { get; set; }
        public string pathClientPhotos { get; set; }
        public string pathProductsPhotos { get; set; }
        public string pathProductsThumbnailPhotos { get; set; }


        public EnviromentApp(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
            this.habilityVaribles();

        }
        public void habilityVaribles()
        {
            this.pathVouchersPhotos = this.getRelativePath("pathVouchersPhotos");
            this.pathClientPhotos = this.getRelativePath("pathClientPhotos");
            this.pathProductsPhotos = this.getRelativePath("pathProductsPhotos");
            this.pathProductsThumbnailPhotos = this.getRelativePath("pathProductsThumbnailPhotos");
        }

        public string getRelativePath(string pathAppJson)
        {
            return "./" + this.pathGeneral + this._configuration.GetValue<string>(pathAppJson) + "/";
        }
    }
}
