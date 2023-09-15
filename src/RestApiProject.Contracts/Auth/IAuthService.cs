using TestProject.Dto.Auth;
using TestProject.Dto.Core;

namespace TestProject.Contracts.Auth
{
    public interface IAuthService
    {
        public Task<BaseDataResponse<TokenModel>> LoginAsync(LoginModel loginModel);
        public BaseDataResponse<ServiceConfigs> GetServiceConfigs();
    }
}
