using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public ulong? Active { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public int? IdRol { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public int? ZipCode { get; set; }
        public string ProfilePictureUrl { get; set; }

        public virtual Role role { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
