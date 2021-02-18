using System.Net.Mime;
using System.Threading.Tasks;
using dotnet_core_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Config;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_core_api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/public/")]
    public class PublicController : ControllerBase
    {
        private DB_PAMYSContext db = new DB_PAMYSContext();
        private Encription bcrypt = new Encription();

        [HttpPost("client")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Client>> saveClient([FromBody] Client client)
        {
            return await Task.Run<ActionResult<Client>>(() =>
            {
                Client newClient = client;
                newClient.Password = bcrypt.hashPassword(newClient.Password);
                this.db.Clients.Add(newClient);
                this.db.SaveChanges();
                return Ok(client);
            });
        }

        [Route("product")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Product>> getAll()
        {
            return await Task.Run<IEnumerable<Product>>(() =>
            {
                IEnumerable<Product> products = this.db.Products.ToList();
                products.ToList().ForEach(product =>
                  {
                      product.category = this.db.Categorys.Find(product.IdCategory);
                      product.vendor = this.db.Vendors.Find(product.IdVendor);
                      product.productsImages = this.db.ProductImages.Where(e => e.IdProduct == product.IdProduct).ToList();
                  });
                return products;
            });
        }

        [HttpGet("product/search/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> getByName(string name)
        {
            return await Task.Run<ActionResult<IEnumerable<Product>>>(() =>
            {
                IEnumerable<Product> products = this.db.Products.Where((product) => product.Name.Contains(name));
                if (products.Count() != 0)
                {
                    products.ToList().ForEach(product =>
                  {
                      product.category = this.db.Categorys.Find(product.IdCategory);
                      product.vendor = this.db.Vendors.Find(product.IdVendor);
                      product.productsImages = this.db.ProductImages.Where(e => e.IdProduct == product.IdProduct).ToList();
                  });
                    return Ok(products);
                }
                return NotFound();
            });
        }

        [HttpGet("product/search/{slug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> getBySlug(string slug)
        {
            return await Task.Run<ActionResult<Product>>(() =>
            {
                Product product = this.db.Products.Where((product) => product.Slug == slug).FirstOrDefault();
                if (product != null)
                {
                    product.category = this.db.Categorys.Find(product.IdCategory);
                    product.vendor = this.db.Vendors.Find(product.IdVendor);
                    product.productsImages = this.db.ProductImages.Where(e => e.IdProduct == product.IdProduct).ToList();
                    return Ok(product);
                }
                return NotFound();
            });
        }

    }

}
