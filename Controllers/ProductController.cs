using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_core_api.Controllers
{

    // Ruta base para todos los endpoints /api/product/*
    [Authorize]
    [ApiController]
    [Route("api/product/")]
    public class ProductController : ControllerBase
    {

        private DB_PAMYSContext db = new DB_PAMYSContext();

        [Route("all")]
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
                      product.ProductImages = this.db.ProductImages.Where(e => e.IdProduct == product.IdProduct).ToList();
                  });
                return products;
            });
        }


        [HttpGet("byCategory/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> getByCategory(int id)
        {
            return await Task.Run<ActionResult<IEnumerable<Product>>>(() =>
            {
                IEnumerable<Product> products;
                products = this.db.Products.ToList().Where(product => product.IdCategory == id);
                if (products.Count() != 0)
                {
                    products.ToList().ForEach(product =>
                  {
                      product.category = this.db.Categorys.Find(product.IdCategory);
                      product.vendor = this.db.Vendors.Find(product.IdVendor);
                      product.ProductImages = this.db.ProductImages.Where(e => e.IdProduct == product.IdProduct).ToList();
                  });
                    return Ok(products);
                }
                return NotFound();
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> getById(int id)
        {
            return await Task.Run<ActionResult<Product>>(() =>
            {
                var product = this.db.Products.Find(id);
                product.category = this.db.Categorys.Find(product.IdCategory);
                product.vendor = this.db.Vendors.Find(product.IdVendor);
                product.ProductImages = this.db.ProductImages.Where(e => e.IdProduct == id).ToList();
                if (product != null)
                    return Ok(product);
                else
                    return NotFound();
            });
        }


        [HttpGet("search/{name}")]
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
                      product.ProductImages = this.db.ProductImages.Where(e => e.IdProduct == product.IdProduct).ToList();
                  });
                    return Ok(products);
                }
                return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Product>> save(Product product)
        {
            return await Task.Run<ActionResult<Product>>(() =>
            {
                if (product == null)
                    return BadRequest();
                product.DateCreated = DateTime.Now;
                product.Slug = product.Name.ToLower().Replace(" ", "-");
                this.db.Products.Add(product);
                this.db.SaveChanges();
                product.category = this.db.Categorys.Find(product.IdCategory);
                product.vendor = this.db.Vendors.Find(product.IdVendor);
                product.ProductImages = this.db.ProductImages.Where(e => e.IdProduct == product.IdProduct).ToList();
                return Ok(product);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> update(Product product)
        {
            return await Task.Run<ActionResult<Product>>(() =>
            {
                try
                {
                    var currentProduct = this.db.Products.Find(product.IdProduct);
                    // aqui pondre lo que se modificara esto es un caso especial
                    currentProduct.Name = product.Name;
                    currentProduct.Slug = product.Name.ToLower().Replace(" ", "-");
                    currentProduct.Price = product.Price;
                    currentProduct.SalePrice = product.SalePrice;
                    currentProduct.Stock = product.Stock;
                    currentProduct.Sku = product.Sku;
                    currentProduct.Description = product.Description;
                    currentProduct.IdVendor = product.IdVendor;
                    currentProduct.IdCategory = product.IdCategory;


                    // if (updateTask.State == EntityState.Modified)
                    this.db.SaveChanges();
                    currentProduct.category = this.db.Categorys.Find(product.IdCategory);
                    currentProduct.vendor = this.db.Vendors.Find(product.IdVendor);
                    currentProduct.ProductImages = this.db.ProductImages.Where(e => e.IdProduct == product.IdProduct).ToList();
                    return Ok(currentProduct);
                }
                catch (DbUpdateException)
                {
                    return NotFound();
                }
            });
        }

        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> delete(uint id)
        {
            var currentProduct = this.db.Products.Find(id);

            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    var deleteTask = this.db.Products.Remove(currentProduct);
                    if (deleteTask.State == EntityState.Deleted)
                        this.db.SaveChanges();
                    return Ok();
                }
                catch (DbUpdateException)
                {
                    return NotFound();
                }
            });
        }

    }
}
