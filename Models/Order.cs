using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public uint Id { get; set; }
        public uint ClientId { get; set; }
        public decimal Subtotal { get; set; }
        public decimal Igv { get; set; }
        public decimal Total { get; set; }
        public string ShippingAddress { get; set; }
        public int ZipCode { get; set; }
        public DateTime DateCreated { get; set; }
        public uint DocumentTypeId { get; set; }
        public uint OrderStatusId { get; set; }

        public virtual User Client { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
