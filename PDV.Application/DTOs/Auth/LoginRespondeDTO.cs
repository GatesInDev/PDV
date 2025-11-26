namespace PDV.Application.DTOs.Auth
{
    public class LoginRespondeDTO
    {
        public string Token { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
    }
}
