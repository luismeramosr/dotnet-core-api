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

    // Ruta base para todos los endpoints /api/order_status/*
    [Authorize]
    [ApiController]
    [Route("api/order_status/")]
    public class OrderStatusController : ControllerBase
    {

        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<OrderStatus>> getAll()
        {
            return await Task.Run<IEnumerable<OrderStatus>>(() =>
            {
                return this.db.OrderStatuses.ToList();
            });
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderStatus>> getById(int id)
        {
            return await Task.Run<ActionResult<OrderStatus>>(() =>
            {
                var orderStatus = this.db.OrderStatuses.Find(id);
                if (orderStatus != null)
                    return Ok(orderStatus);
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderStatus>> save(OrderStatus orderStatus)
        {
            return await Task.Run<ActionResult<OrderStatus>>(() =>
            {
                if (orderStatus == null)
                    return BadRequest();
                this.db.OrderStatuses.Add(orderStatus);
                this.db.SaveChanges();
                return Ok(orderStatus);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderStatus>> update(OrderStatus orderStatus)
        {
            return await Task.Run<ActionResult<OrderStatus>>(() =>
            {
                try
                {
                    var updateTask = this.db.OrderStatuses.Update(orderStatus);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    return Ok(orderStatus);
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
            var orderStatus = this.db.OrderStatuses.Find(id);
            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    var deleteTask = this.db.OrderStatuses.Remove(orderStatus);
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
