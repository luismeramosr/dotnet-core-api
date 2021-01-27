using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace dotnet_core_api.Controllers {

    // Ruta base para todos los endpoints /api/product/*
    [ApiController]
    [Route("api/product/")]
    public class ProductController : ControllerBase {

        private DB_PAMYSContext db = new DB_PAMYSContext();
        
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Product>> getAll() {
            return await Task.Run<IEnumerable<Product>>(() => { 
                return this.db.Products.ToList();
            });
        }

        [Route("byCategory")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Product>>> getByCategory(uint id) {
            return await Task.Run<ActionResult<IEnumerable<Product>>>(() => {
                IEnumerable<Product> products;
                products = this.db.Products.ToList().Where(product => product.CategoryId == id);
                if (products.Count()>0)
                    return Ok(new {products = products, data = products});
                else
                    return NotFound();
            });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> getById(uint id) {
            return await Task.Run<ActionResult<Product>>(() => {
                var product = this.db.Products.Find(id);
                if (product != null)
                    return Ok(product);
                else
                    return NotFound();
            });
        }

        [Route("byName")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> getByName(string name) {
            return await Task.Run<ActionResult<Product>>(() => {
                var product = new Product();
                product = this.db.Products.ToList().Find((product) => product.Name == name);
                if (product != null)
                    return Ok(product);
                else
                    return NotFound();
            });
        }
        
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> add(Product product) {
            return await Task.Run<IActionResult>(() => {
                if (product == null) 
                    return BadRequest();                
                this.db.Products.Add(product);
                this.db.SaveChanges();
                return Created("/api/vendor", product);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> update(Product product) {
            return await Task.Run<IActionResult>(() =>
            {     
                try {
                    var updateTask = this.db.Products.Update(product);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    return Ok();
                }catch (DbUpdateException) {
                    return NotFound();
                }             
            });
        }

        [HttpDelete("{id}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> delete(uint id) {         
            return await Task.Run<IActionResult>(() => {
                try {
                    var deleteTask = this.db.Products.Remove(new Product(){Id=id});
                    if (deleteTask.State == EntityState.Deleted)
                        this.db.SaveChanges();
                    return Ok();
                }catch (DbUpdateException) {                    
                    return NotFound();
                }
            });
        }

    }
}