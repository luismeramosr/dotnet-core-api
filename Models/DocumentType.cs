using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            Orders = new HashSet<Order>();
        }

        public int idDocumentType { get; set; }
        public string doctype { get; set; }

        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
