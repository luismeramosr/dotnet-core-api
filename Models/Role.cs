using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Role
    {
        public Role()
        {
            Clients = new HashSet<Client>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Client> Clients { get; set; }
    }
}
