using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Order
    {
        public Order()
        {
            products = new HashSet<OrderDetail>();
        }

        public int idOrder { get; set; }
        public string comment { get; set; }
        public DateTime? dateCreated { get; set; }
        public int? idClient { get; set; }
        public int? idDocumentType { get; set; }
        public int? idOrderStatus { get; set; }
        public int? idPaymentStatus { get; set; }
        public int? idVoucher { get; set; }
        public double? igv { get; set; }
        public string shippingAddress { get; set; }
        public double? subtotal { get; set; }
        public double? total { get; set; }
        public int? zipCode { get; set; }

        public virtual Client client { get; set; }
        public virtual DocumentType documentType { get; set; }
        public virtual OrderStatus orderStatus { get; set; }
        public virtual PaymentType paymentType { get; set; }
        public virtual Voucher voucher { get; set; }
        public virtual ICollection<OrderDetail> products { get; set; }
    }
}
