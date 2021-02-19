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

    // Ruta base para todos los endpoints /api/order/*
    [Authorize]
    [ApiController]
    [Route("api/order/")]
    public class OrderController : ControllerBase
    {

        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<Order>> getAll()
        {
            return await Task.Run<IEnumerable<Order>>(() =>
            {
                List<Order> orders = this.db.Orders.AsNoTracking().ToList();
                orders.ForEach(o =>
                {
                    o.client = this.db.Clients.Find(o.idClient);
                    o.documentType = this.db.DocumentTypes.Find(o.idDocumentType);
                    o.orderStatus = this.db.OrderStatuses.Find(o.idOrderStatus);
                    o.paymentType = this.db.PaymentTypes.Find(o.idPaymentStatus);
                    Voucher orderVoucher = this.db.Vouchers.Find(o.idVoucher);
                    if (orderVoucher != null)
                    {
                        o.voucher = orderVoucher;
                    }
                    List<OrderDetail> products = this.db.OrderDetails.Where(d => d.id.idOrder == o.idOrder).ToList();
                    products.ForEach(p =>
                    {
                        p.id = new OrderDetailsPK { idOrder = p.idOrder, idProduct = p.idProduct };
                        p.product = this.db.Products.Find(p.idProduct);
                    });
                    o.products = products;
                });
                return orders;
            });
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> getById(int id)
        {
            return await Task.Run<ActionResult<Order>>(() =>
            {
                var order = this.db.Orders.Find(id);
                if (order != null)
                    return Ok(order);
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Order>> save(Order order)
        {
            return await Task.Run<ActionResult<Order>>(() =>
            {
                if (order == null)
                    return BadRequest();
                this.db.Orders.Add(order);
                this.db.SaveChanges();
                return Ok(order);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Order>> update(Order order)
        {
            return await Task.Run<ActionResult<Order>>(() =>
            {
                try
                {
                    var updateTask = this.db.Orders.Update(order);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    return Ok(order);
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
            var order = this.db.Orders.Find(id);
            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    var deleteTask = this.db.Orders.Remove(order);
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
