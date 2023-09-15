using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TestProject.Dto.Auth
{
    public class TokenModel
    {
        [JsonProperty("token")] public string Token { get; set; } = string.Empty;
        [JsonProperty("refreshToken")] public string RefreshToken { get; set; } = string.Empty;
    }
    public class AuthenticationResult : TokenModel
    {
        public bool Success { get; set; }
        public IEnumerable<string> Errors { get; set; } = Enumerable.Empty<string>();
    }    
}
