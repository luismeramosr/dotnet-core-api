using Microsoft.AspNetCore.Mvc;
using dotnet_core_api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using System;

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
        public async Task<IEnumerable<OrderCustom>> getAll()
        {
            return await Task.Run<IEnumerable<OrderCustom>>(() =>
            {
                List<Order> orders = this.db.Orders.AsNoTracking().ToList();
                List<OrderCustom> result = new List<OrderCustom>();
                orders.ForEach(o =>
                {
                    Voucher orderVoucher = this.db.Vouchers.Find(o.idVoucher);
                    List<OrderDetail> products = this.db.OrderDetails.Where(d => d.idOrder == o.idOrder).ToList();
                    List<OrderDetailCustom> od_result = new List<OrderDetailCustom>();
                    products.ForEach(p =>
                    {
                        od_result.Add(new OrderDetailCustom
                        {
                            id = new OrderDetailsPK { idOrder = p.idOrder, idProduct = p.idProduct },
                            quantity = p.quantity,
                            product = this.db.Products.Find(p.idProduct)
                        });
                    });
                    result.Add(new OrderCustom
                    {
                        idOrder = o.idOrder,
                        comment = o.comment,
                        dateCreated = o.dateCreated,
                        idClient = o.idClient,
                        idDocumentType = o.idDocumentType,
                        idOrderStatus = o.idOrderStatus,
                        idPaymentStatus = o.idPaymentStatus,
                        idVoucher = o.idVoucher,
                        igv = o.igv,
                        shippingAddress = o.shippingAddress,
                        subtotal = o.subtotal,
                        total = o.total,
                        zipCode = o.zipCode,
                        client = this.db.Clients.Find(o.idClient),
                        documentType = this.db.DocumentTypes.Find(o.idDocumentType),
                        orderStatus = this.db.OrderStatuses.Find(o.idOrderStatus),
                        paymentType = this.db.PaymentTypes.Find(o.idPaymentStatus),
                        voucher = orderVoucher != null ? orderVoucher : null,
                        products = od_result
                    });
                });
                return result;
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
        public async Task<ActionResult<OrderCustom>> update(OrderCustom order)
        {
            return await Task.Run<ActionResult<OrderCustom>>(() =>
            {
                try
                {
                    List<OrderDetailCustom> detailsCustom = (List<OrderDetailCustom>)order.products;
                    List<OrderDetail> prods = new List<OrderDetail>();
                    detailsCustom.ForEach(dc =>
                    {
                        prods.Add(new OrderDetail
                        {
                            idOrder = dc.id.idOrder,
                            idProduct = dc.id.idProduct,
                            quantity = dc.quantity,
                            product = this.db.Products.Find(dc.id.idProduct)
                        });
                    });
                    Order updatedOrder = new Order
                    {
                        idOrder = order.idOrder,
                        comment = order.comment,
                        dateCreated = order.dateCreated,
                        idClient = order.idClient,
                        idDocumentType = order.idDocumentType,
                        idOrderStatus = order.idOrderStatus,
                        idPaymentStatus = order.idPaymentStatus,
                        idVoucher = order.idVoucher,
                        igv = order.igv,
                        shippingAddress = order.shippingAddress,
                        subtotal = order.subtotal,
                        total = order.total,
                        zipCode = order.zipCode,
                        client = order.client,
                        documentType = order.documentType,
                        orderStatus = order.orderStatus,
                        paymentType = order.paymentType,
                        voucher = order.voucher,
                        products = prods
                    };
                    var updateTask = this.db.Orders.Update(updatedOrder);
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
