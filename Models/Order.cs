using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Order
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public decimal Total { get; set; }
        public string ShippingAddress { get; set; }
        public DateTime DateCreated { get; set; }
        public sbyte Status { get; set; }

        public virtual User Client { get; set; }
    }
}
