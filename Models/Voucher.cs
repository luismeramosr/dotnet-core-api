using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            Orders = new HashSet<Order>();
        }

        public uint Id { get; set; }
        public string VoucherImgUrl { get; set; }
        public uint UserId { get; set; }
        public DateTime PaymentDate { get; set; }
        public uint OperationId { get; set; }
        public int ClientAccountId { get; set; }
        public int StoreAccountId { get; set; }
        public decimal Amount { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
