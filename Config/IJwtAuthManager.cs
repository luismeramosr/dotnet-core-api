namespace dotnet_core_api.Config
{

    public interface IJwtAuthManager
    {
        object Authenticate(string username, string password);
    }

}
