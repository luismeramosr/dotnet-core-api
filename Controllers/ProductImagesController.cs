using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

// necesarios para upload
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using dotnet_core_api.env;
using dotnet_core_api.utilities;
namespace dotnet_core_api.Controllers
{


    // Ruta base para todos los endpoints /api/product_images/*
    [Authorize]
    [ApiController]
    [Route("api/product_images/")]
    public class ProductImagesController : ControllerBase
    {
        public PhotoUtilities photoUtilities = new PhotoUtilities();
        private readonly IConfiguration _configuration;
        public static IWebHostEnvironment _webHostEnvironment;
        private EnviromentApp env;
        public ProductImagesController(IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _configuration = configuration;//instancio la configuracion
            _webHostEnvironment = webHostEnvironment;
            this.env = new EnviromentApp(_webHostEnvironment, _configuration); // se la paso a mi modelo con las constantes
        }
        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<ProductImage>> getAll()
        {
            return await Task.Run<IEnumerable<ProductImage>>(() =>
            {
                return this.db.ProductImages.ToList();
            });
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductImage>> getById(int id)
        {
            return await Task.Run<ActionResult<ProductImage>>(() =>
            {
                var productImage = this.db.ProductImages.Find(id);
                if (productImage != null)
                    return Ok(productImage);
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductImage>> save(ProductImage productImage)
        {
            return await Task.Run<ActionResult<ProductImage>>(() =>
            {
                if (productImage == null)
                    return BadRequest();
                this.db.ProductImages.Add(productImage);
                this.db.SaveChanges();
                return Ok(productImage);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductImage>> update(ProductImage productImage)
        {
            return await Task.Run<ActionResult<ProductImage>>(() =>
            {
                try
                {
                    var updateTask = this.db.ProductImages.Update(productImage);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    return Ok(productImage);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            });
        }

        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> delete(int id)
        {
            var productImage = this.db.ProductImages.Find(id);
            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    var deleteTask = this.db.ProductImages.Remove(productImage);
                    if (deleteTask.State == EntityState.Deleted)
                        this.db.SaveChanges();
                    return Ok();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
            });
        }

        // upload image client
        [Route("photos/upload")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductImage>>> uploadPhotoClient([FromForm] IFormFile[] imgFile, [FromForm] int idProduct)
        {
            return await Task.Run<ActionResult<IEnumerable<ProductImage>>>(async () =>
            {
                var productImagesList = this.db.ProductImages.Where(e => e.IdProduct == idProduct);
                // existe un cliente y la imgfile almenos algo
                if (imgFile.Length != 0)
                {
                    string path = this.env.pathProductsPhotos;
                    if (productImagesList != null)
                    {
                        // recorro cada imagen y la elimino
                        await productImagesList.ForEachAsync(async (e) =>
                        {
                            this.db.ProductImages.Remove(e);
                            await this.photoUtilities.removePhoto(e.Url, path);
                        });
                        this.db.SaveChanges();
                    }
                    // guardo en cascada las imagenes relacionadas
                    for (int i = 0; i < imgFile.Length; i++)
                    {
                        string nameFileEncript = "";
                        nameFileEncript = await this.photoUtilities.copyPhoto(imgFile[i], path);
                        // guardo en la bd
                        var productImageNew = new ProductImage();
                        productImageNew.IdProduct = idProduct;
                        productImageNew.Url = nameFileEncript;
                        this.db.ProductImages.Add(productImageNew);
                        this.db.SaveChanges();
                    }

                    var productImagesUpdated = this.db.ProductImages.Where(e => e.IdProduct == idProduct);
                    return Ok(productImagesUpdated);
                }
                return BadRequest();

            });
        }
    }
}
