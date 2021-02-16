using System;
using System.Collections.Generic;

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

        public virtual ICollection<Client> Clients { get; set; }
    }
}
