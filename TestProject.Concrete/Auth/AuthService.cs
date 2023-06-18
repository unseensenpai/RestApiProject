using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TestProject.Contracts.Auth;
using TestProject.DAL;
using TestProject.Dto.Auth;
using TestProject.Dto.Core;

namespace TestProject.Concrete.Auth
{
    public class AuthService : IAuthService
    {
        private readonly MySqlContext _context;
        private readonly ServiceConfigs _serviceConfigs;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly IAuthService _authService;

        public AuthService(MySqlContext context, IOptions<ServiceConfigs> serviceConfigs, TokenValidationParameters tokenValidationParameters, IAuthService authService)
        {
            _context = context;
            _serviceConfigs = serviceConfigs.Value;
            _tokenValidationParameters = tokenValidationParameters;
            _authService = authService;
        }

        public Task<BaseDataResponse<TokenModel>> Login(LoginModel loginModel)
        {
            BaseDataResponse<TokenModel> response = new();
            try
            {
                throw new BadImageFormatException("deneme mesaj");
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
