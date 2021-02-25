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

    // Ruta base para todos los endpoints /api/order_details/*
    [Authorize]
    [ApiController]
    [Route("api/order_details/")]
    public class OrderDetailController : ControllerBase
    {

        private DB_PAMYSContext db = new DB_PAMYSContext();

        // Todos los endpoints son funciones asincronas, para mejorar
        // el performance, aun falta ver una manera de retornar los estados http
        // y validar errores.
        [Route("all")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<OrderDetailCustom>> getAll()
        {
            return await Task.Run<IEnumerable<OrderDetailCustom>>(() =>
            {
                List<OrderDetail> orderDetails = this.db.OrderDetails.ToList();
                List<OrderDetailCustom> result = new List<OrderDetailCustom>();
                orderDetails.ForEach(e =>
                {
                    result.Add(new OrderDetailCustom
                    {
                        id = new OrderDetailsPK { idOrder = e.idOrder, idProduct = e.idProduct },
                        quantity = e.quantity,
                        product = this.db.Products.Find(e.idProduct)
                    });
                });
                return result;
            });
        }

        // Recibe el parametro id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailCustom>> getById(int id)
        {
            return await Task.Run<ActionResult<OrderDetailCustom>>(() =>
            {
                List<OrderDetail> orderDetails = (List<OrderDetail>)this.db.OrderDetails.Where(e => e.idOrder == id).ToList();
                List<OrderDetailCustom> result = new List<OrderDetailCustom>();

                if (orderDetails.Count() > 0)
                {
                    orderDetails.ForEach(od =>
                    {
                        result.Add(new OrderDetailCustom
                        {
                            id = new OrderDetailsPK { idOrder = od.idOrder, idProduct = od.idProduct },
                            quantity = od.quantity,
                            product = this.db.Products.Find(od.idProduct)
                        });
                    });
                    return Ok(result);
                }
                else
                    return NotFound();
            });
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderDetailCustom>> save(OrderDetail orderDetail)
        {
            return await Task.Run<ActionResult<OrderDetailCustom>>(() =>
            {
                if (orderDetail == null)
                    return BadRequest();

                this.db.OrderDetails.Add(orderDetail);
                this.db.SaveChanges();
                OrderDetailCustom od = new OrderDetailCustom
                {
                    id = new OrderDetailsPK { idOrder = orderDetail.idOrder, idProduct = orderDetail.idProduct },
                    quantity = orderDetail.quantity,
                    product = this.db.Products.Find(orderDetail.idProduct)
                };

                return Ok(od);
            });
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderDetailCustom>> update(OrderDetailCustom orderDetail)
        {
            return await Task.Run<ActionResult<OrderDetailCustom>>(() =>
            {
                try
                {
                    if (orderDetail == null)
                        return BadRequest();
                    OrderDetail od = new OrderDetail
                    {
                        idOrder = orderDetail.id.idOrder,
                        idProduct = orderDetail.id.idProduct,
                        quantity = orderDetail.quantity
                    };
                    var updateTask = this.db.OrderDetails.Update(od);
                    if (updateTask.State == EntityState.Modified)
                        this.db.SaveChanges();
                    orderDetail.id = new OrderDetailsPK { idOrder = od.idOrder, idProduct = od.idProduct };
                    orderDetail.product = this.db.Products.Find(od.idProduct);
                    orderDetail.product.category = this.db.Categorys.Find(orderDetail.product.IdCategory);
                    orderDetail.product.vendor = this.db.Vendors.Find(orderDetail.product.IdVendor);
                    orderDetail.product.productsImages = this.db.ProductImages.Where(e => e.IdProduct == orderDetail.id.idProduct).ToList();
                    return Ok(orderDetail);
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
            var orderDetail = this.db.OrderDetails.Find(id);
            return await Task.Run<IActionResult>(() =>
            {
                try
                {
                    var deleteTask = this.db.OrderDetails.Remove(orderDetail);
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
