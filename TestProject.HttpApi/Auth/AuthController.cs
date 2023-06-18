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

        [HttpPost]
        public Task<BaseDataResponse<TokenModel>> Login(LoginModel loginModel) => _authService.Login(loginModel);

    }
}
