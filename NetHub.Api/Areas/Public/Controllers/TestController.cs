using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetHub.Core.Constants;

namespace NetHub.Api.Areas.Public.Controllers;

[ApiController]
[ApiVersion(Versions.V1)]
[Route("/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class TestController : Controller
{
    [Authorize(Policy = Policies.User)]
    [HttpGet("hi")]
    public ActionResult<string> GetHelloWorld() => "Hello World :)";
    
    [Authorize(Policy = Policies.Admin)]
    [HttpGet("admin")]
    public ActionResult<string> GetAdminWord() => "Admin";
    
    [Authorize(Policy = Policies.Master)]
    [HttpGet("master")]
    public ActionResult<string> GetMasterWord() => "Master";
}