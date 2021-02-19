using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace dotnet_core_api.Models
{
    public partial class Client
    {
        public Client()
        {
            Orders = new HashSet<Order>();
        }

        public int idClient { get; set; }
        public ulong? active { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public int? idRol { get; set; }
        public string lastName { get; set; }
        public string password { get; set; }
        public string phone { get; set; }
        public string username { get; set; }
        public int? zip_code { get; set; }
        public string profilePictureUrl { get; set; }

        public virtual Role role { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
