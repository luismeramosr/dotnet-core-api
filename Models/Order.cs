using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Order
    {
        public Order()
        {
            products = new HashSet<OrderDetail>();
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

        public virtual Client client { get; set; }
        public virtual DocumentType documentType { get; set; }
        public virtual OrderStatus orderStatus { get; set; }
        public virtual PaymentType paymentType { get; set; }
        public virtual Voucher voucher { get; set; }
        public virtual ICollection<OrderDetail> products { get; set; }
    }
}
