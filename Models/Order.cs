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

        public int Id { get; set; }
        public string Comment { get; set; }
        public DateTime? DateCreated { get; set; }
        public int? IdClient { get; set; }
        public int? IdDocumentType { get; set; }
        public int? IdOrderStatus { get; set; }
        public int? IdPaymentStatus { get; set; }
        public int? IdVoucher { get; set; }
        public double? Igv { get; set; }
        public string ShippingAddress { get; set; }
        public double? Subtotal { get; set; }
        public double? Total { get; set; }
        public int? ZipCode { get; set; }

        public virtual Client IdClientNavigation { get; set; }
        public virtual DocumentType IdDocumentTypeNavigation { get; set; }
        public virtual OrderStatus IdOrderStatusNavigation { get; set; }
        public virtual PaymentType IdPaymentStatusNavigation { get; set; }
        public virtual Voucher IdVoucherNavigation { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
