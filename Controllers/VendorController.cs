using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;

namespace dotnet_core_api.Controllers {

    // Ruta base para todos los endpoints /api/vendor/*
    [ApiController]
    [Route("api/vendor/")]
    public class VendorController : ControllerBase {

        private db_pamysContext db = new db_pamysContext();
        
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IEnumerable<Vendor>> getAll() {
            return await Task.Run<IEnumerable<Vendor>>(() => { 
                return this.db.Vendors.ToList();
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Vendor>> getById(int id) {
            return await Task.Run<ActionResult<Vendor>>(() => {
                var vendor = this.db.Vendors.Find(id);
                if (vendor != null)
                    return Ok(vendor);
                else
                    return NotFound();
            });
        }

        [Route("search")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Vendor>> getByCompany(string company) {
            return await Task.Run<ActionResult<Vendor>>(() => {
                var vendor = new  Vendor();
                vendor = this.db.Vendors.ToList().Find((vendor) => vendor.Company == company);
                if (vendor != null)
                    return Ok(vendor);
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> add(Vendor vendor) {
            return await Task.Run<IActionResult>(() => {
                if (vendor == null) 
                    return BadRequest();                
                this.db.Vendors.Add(vendor);
                this.db.SaveChanges();
                return Created("/api/vendor", vendor);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> update(Vendor vendor) {
            return await Task.Run<IActionResult>(() =>
            {     
                try {
                    var updateTask = this.db.Vendors.Update(vendor);
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
                    var deleteTask = this.db.Vendors.Remove(new Vendor(){Id=id});
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