namespace PDV.Clients.Models.Auth
{
    public class LoginResponseModel
    {
        public string Token { get; init; } = string.Empty;

        public string Username { get; init; } = string.Empty;

        public string Role { get; init; } = string.Empty;
    }
}
