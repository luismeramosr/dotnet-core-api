using System.ComponentModel.DataAnnotations.Schema;

namespace dotnet_core_api.Models
{
    [NotMapped]
    public class OrderDetailsPK
    {
        public int idOrder { get; set; }
        public int idProduct { get; set; }
    }
}
