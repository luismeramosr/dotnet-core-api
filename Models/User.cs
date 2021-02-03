using System;
using System.Collections.Generic;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            Vouchers = new HashSet<Voucher>();
        }

        public uint Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public uint RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int? ZipCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public sbyte Active { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Voucher> Vouchers { get; set; }
    }
}
