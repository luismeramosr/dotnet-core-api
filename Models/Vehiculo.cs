using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Vehiculo
    {
        public Vehiculo()
        {
            Papeleta = new HashSet<Papeleta>();
        }

        public string Nropla { get; set; }
        public string Dnipro { get; set; }
        public string Color { get; set; }
        public string Modelo { get; set; }
        public int? Año { get; set; }
        public string Eliminado { get; set; }

        [JsonIgnore]
        public virtual Propietario DniproNavigation { get; set; }
        [JsonIgnore]
        public virtual ICollection<Papeleta> Papeleta { get; set; }
    }
}
