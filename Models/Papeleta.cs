using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Papeleta
    {
        public int Nropap { get; set; }
        public string Nropla { get; set; }
        public string Codinf { get; set; }
        public string Idpol { get; set; }
        public DateTime? Papfecha { get; set; }
        public string Pagado { get; set; }
        public DateTime? Fecpago { get; set; }
        public string Eliminado { get; set; }

        [JsonIgnore]
        public virtual Infraccione CodinfNavigation { get; set; }
        [JsonIgnore]
        public virtual Policia IdpolNavigation { get; set; }
        [JsonIgnore]
        public virtual Vehiculo NroplaNavigation { get; set; }
    }
}
