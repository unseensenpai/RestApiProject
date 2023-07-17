using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestProject.HttpApi.Core
{
    [ApiController]
    [Route("api/[controller]/[Action]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    [AllowAnonymous]
    public class CoreController : ControllerBase
    {

    }
}
