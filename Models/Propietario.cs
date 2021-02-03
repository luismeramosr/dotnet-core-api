using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Propietario
    {
        public Propietario()
        {
            Vehiculos = new HashSet<Vehiculo>();
        }

        public string Dnipro { get; set; }
        public string Nompro { get; set; }
        public string Dirpro { get; set; }
        public string Eliminado { get; set; }
        [JsonIgnore]
        public virtual ICollection<Vehiculo> Vehiculos { get; set; }
    }
}
