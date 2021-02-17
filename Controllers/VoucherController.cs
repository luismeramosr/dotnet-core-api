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

    // Ruta base para todos los endpoints /api/voucher/*
    [Authorize]
    [ApiController]
    [Route("api/voucher/")]
    public class VoucherController : ControllerBase
    {

        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<Voucher>> getAll()
        {
            return await Task.Run<IEnumerable<Voucher>>(() =>
            {
                return this.db.Vouchers.ToList();
            });
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Voucher>> getById(int id)
        {
            return await Task.Run<ActionResult<Voucher>>(() =>
            {
                var voucher = this.db.Vouchers.Find(id);
                if (voucher != null)
                    return Ok(voucher);
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Voucher>> save(Voucher voucher)
        {
            return await Task.Run<ActionResult<Voucher>>(() =>
            {
                if (voucher == null)
                    return BadRequest();
                this.db.Vouchers.Add(voucher);
                this.db.SaveChanges();
                return Ok(voucher);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Voucher>> update(Voucher voucher)
        {
            return await Task.Run<ActionResult<Voucher>>(() =>
            {
                try
                {
                    var updateTask = this.db.Vouchers.Update(voucher);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    return Ok(voucher);
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
            var voucher = this.db.Vouchers.Find(id);
            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    var deleteTask = this.db.Vouchers.Remove(voucher);
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
