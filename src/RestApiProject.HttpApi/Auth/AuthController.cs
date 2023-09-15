using Microsoft.AspNetCore.Mvc;
using TestProject.Contracts.Auth;
using TestProject.Dto.Auth;
using TestProject.Dto.Core;
using TestProject.HttpApi.Core;

namespace TestProject.HttpApi.Auth
{
    public class AuthController : CoreController, IAuthService
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login For Jwt Token
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<BaseDataResponse<TokenModel>> LoginAsync(LoginModel loginModel)
            => _authService.LoginAsync(loginModel);

        /// <summary>
        /// Service Configs EP
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BaseDataResponse<ServiceConfigs> GetServiceConfigs()
            => _authService.GetServiceConfigs();

    }
}
