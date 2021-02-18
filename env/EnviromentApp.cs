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
            this.pathVouchersPhotos = this.getPathAbsolute("pathVouchersPhotos");
            this.pathClientPhotos = this.getPathAbsolute("pathClientPhotos");
            this.pathProductsPhotos = this.getPathAbsolute("pathProductsPhotos");
            this.pathProductsThumbnailPhotos = this.getPathAbsolute("pathProductsThumbnailPhotos");
        }

        public string getPathAbsolute(string pathAppJson)
        {
            return this._webHostEnvironment.WebRootPath + this.pathGeneral + this._configuration.GetValue<string>(pathAppJson) + "/";
        }
    }
}