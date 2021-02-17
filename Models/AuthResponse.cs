namespace dotnet_core_api.Models
{
    public class AuthResponse
    {
        public string jwt { get; set; }
        public Client user { get; set; }
    }
}
