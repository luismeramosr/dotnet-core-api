using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class OrderCustom
    {
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

        public Client client { get; set; }
        public DocumentType documentType { get; set; }
        public OrderStatus orderStatus { get; set; }
        public PaymentType paymentType { get; set; }
        public Voucher voucher { get; set; }
        public ICollection<OrderDetailCustom> products { get; set; }
    }
}
