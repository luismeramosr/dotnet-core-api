using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace dotnet_core_api.Controllers {

    // Ruta base para todos los endpoints /api/category/*
    [ApiController]
    [Route("api/category/")]    
    public class CategoryController : ControllerBase {
        
        private db_pamysContext db = new db_pamysContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<Category>> getAll() {
            return await Task.Run<IEnumerable<Category>>(() => { 
                return this.db.Categories.ToList();
            });
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]      
        public async Task<ActionResult<Category>> getById(uint id) {
            return await Task.Run<ActionResult<Category>>(() => {
                var category = this.db.Categories.Find(id);
                if (category != null)
                    return Ok(category);
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> add(Category category) {
            return await Task.Run<IActionResult>(() => {
                if (category == null) 
                    return BadRequest();                
                this.db.Categories.Add(category);
                this.db.SaveChanges();
                return Created("/api/category", category);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> update(Category category) {
            return await Task.Run<IActionResult>(() =>
            {     
                try {
                    var updateTask = this.db.Categories.Update(category);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    return Ok();
                }catch (DbUpdateConcurrencyException) {
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
                    var deleteTask = this.db.Categories.Remove(new Category(){Id=id});
                    if (deleteTask.State == EntityState.Deleted)
                        this.db.SaveChanges();
                    return Ok();
                }catch (DbUpdateConcurrencyException) {                    
                    return NotFound();
                }
            });
        }

    }
}