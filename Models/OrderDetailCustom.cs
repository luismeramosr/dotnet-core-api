
#nullable disable

namespace dotnet_core_api.Models
{
    public partial class OrderDetailCustom
    {
        public OrderDetailsPK id { get; set; }
        public double? price { get; set; }
        public int? quantity { get; set; }
        public Product product { get; set; }
    }
}
