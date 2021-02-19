using dotnet_core_api.Models;

namespace dotnet_core_api.Config
{

    public interface IJwtAuthManager
    {
        AuthResponse Authenticate(string username, string password);
    }

}
