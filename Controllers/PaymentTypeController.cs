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

    // Ruta base para todos los endpoints /api/payment_type/*
    [Authorize]
    [ApiController]
    [Route("api/payment_type/")]
    public class PaymentTypeController : ControllerBase
    {

        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<PaymentType>> getAll()
        {
            return await Task.Run<IEnumerable<PaymentType>>(() =>
            {
                return this.db.PaymentTypes.ToList();
            });
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentType>> getById(int id)
        {
            return await Task.Run<ActionResult<PaymentType>>(() =>
            {
                var paymentType = this.db.PaymentTypes.Find(id);
                if (paymentType != null)
                    return Ok(paymentType);
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PaymentType>> save(PaymentType paymentType)
        {
            return await Task.Run<ActionResult<PaymentType>>(() =>
            {
                if (paymentType == null)
                    return BadRequest();
                this.db.PaymentTypes.Add(paymentType);
                this.db.SaveChanges();
                return Ok(paymentType);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PaymentType>> update(PaymentType paymentType)
        {
            return await Task.Run<ActionResult<PaymentType>>(() =>
            {
                try
                {
                    var updateTask = this.db.PaymentTypes.Update(paymentType);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    return Ok(paymentType);
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
            var paymentType = this.db.PaymentTypes.Find(id);
            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    var deleteTask = this.db.PaymentTypes.Remove(paymentType);
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
