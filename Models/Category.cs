using System.Collections.Generic;
using System.Text.Json.Serialization;
#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public int IdCategory { get; set; }
        public bool Active { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
