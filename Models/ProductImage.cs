using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable

namespace dotnet_core_api.Models
{
    public partial class ProductImage
    {
        public int IdProductImages { get; set; }
        public int? IdProduct { get; set; }
        public string Url { get; set; }

        [JsonIgnore]
        public virtual Product IdProductNavigation { get; set; }
    }
}
