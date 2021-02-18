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

        public int Id { get; set; }
        public double? Amount { get; set; }
        public int? IdClient { get; set; }
        public int? IdClientAccount { get; set; }
        public int? IdOperation { get; set; }
        public int? IdStoreAccount { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedAt { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
