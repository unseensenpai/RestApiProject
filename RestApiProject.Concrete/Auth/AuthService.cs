using Microsoft.Extensions.Logging;
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
        private readonly ILogger<AuthService> _logger;

        public AuthService(MySqlContext context, IOptions<ServiceConfigs> serviceConfigs, TokenValidationParameters tokenValidationParameters, ILogger<AuthService> logger)
        {
            _logger = logger;
            _context = context;
            _serviceConfigs = serviceConfigs.Value;
            _tokenValidationParameters = tokenValidationParameters;
        }

        public Task<BaseDataResponse<TokenModel>> LoginAsync(LoginModel loginModel)
        {
            BaseDataResponse<TokenModel> response = new();
            try
            {
                throw new BadImageFormatException("Bu bir bad format exception ismidir.");
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured on Login process. {ex}", ex.Message);
                throw;
            }
        }

        public BaseDataResponse<ServiceConfigs> GetServiceConfigs()
        {
            return new() { Data = _serviceConfigs, IsSuccess = true, Message = "Success" };
        }
    }
}
