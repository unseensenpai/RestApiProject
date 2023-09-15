using Microsoft.AspNetCore.Mvc;
using RestApiProject.Contracts.User;
using TestProject.HttpApi.Core;

namespace RestApiProject.HttpApi.User
{
    public class UserController : CoreController, IUserService
    {
        readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Veri tabanında kullanıcının olup olmadığını kontrol eder.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> IsUserExist(string username)
            => await _userService.IsUserExist(username);

        [HttpGet]
        public async Task<IActionResult> TestAdmin()
        {
            var response = await IsUserExist("admin");
            if (response)
            {
                return Ok(response);
            }
            else
            {
                return Unauthorized(response);
            }
        }
    }
}
