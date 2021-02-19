using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            Orders = new HashSet<Order>();
        }

        public int idVoucher { get; set; }
        public double? amount { get; set; }
        public int? idClient { get; set; }
        public int? idClientAccount { get; set; }
        public int? idOperation { get; set; }
        public int? idStoreAccount { get; set; }
        public string imageUrl { get; set; }
        public DateTime? paymentDate { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
