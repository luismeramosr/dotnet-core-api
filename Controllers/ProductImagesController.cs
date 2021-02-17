using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;

namespace dotnet_core_api.Controllers
{

    // Ruta base para todos los endpoints /api/product_images/*
    [Authorize]
    [ApiController]
    [Route("api/product_images/")]
    public class ProductImagesController : ControllerBase
    {

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

    }
}
