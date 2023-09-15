using DevExpress.Xpo;
using Microsoft.Extensions.Logging;
using RestApiProject.Contracts.User;
using RestApiProject.Core.Exceptions;
using RestApiProject.DAL.Datalayers;
using RestApiProject.DAL.DataObjects.User;

namespace RestApiProject.BL.User
{
    public class UserService : IUserService
    {
        private readonly IRestApiDataLayer _restApiDataLayer;
        readonly ILogger<UserService> _logger;
        public UserService(IRestApiDataLayer restApiDataLayer, ILogger<UserService> logger)
        {
            _logger = logger;
            _restApiDataLayer = restApiDataLayer;
        }
        public async Task<bool> IsUserExist(string username)
        {
            bool result = default;
            try
            {
                NullObjectException.ThrowIfNull(_restApiDataLayer);
                using UnitOfWork unitOfWork = new(_restApiDataLayer);
                NullObjectException.ThrowIfNull(unitOfWork);
                result = await unitOfWork.Query<UserObject>().AnyAsync(x => x.Username.Equals(username));
            }
            catch (DatabaseConnectionException ex)
            {
                _logger.LogCritical("An error occured while connecting database. ERROR: {message} - STACK:{stack}", ex.Message, ex.StackTrace);
            }
            catch (NullObjectException ex)
            {
                _logger.LogCritical("An error occured while connecting database. ERROR: {message} - STACK:{stack}", ex.Message, ex.StackTrace);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occured while connecting database {ex}", ex);
            }
            return result;
        }
    }
}
