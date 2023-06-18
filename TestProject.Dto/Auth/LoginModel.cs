using Newtonsoft.Json;

namespace TestProject.Dto.Auth
{
    public class LoginModel
    {
        [JsonProperty("username")] public string Username { get; set; } = string.Empty;
        [JsonProperty("password")] public string Password { get; set; } = string.Empty;
    }
}
