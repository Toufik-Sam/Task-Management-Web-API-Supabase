using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.ApplicationDTOs.AuthControllerDTOs;
using TaskManagementAPI.Services;

namespace TaskManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IAuthService _supabaseAuth;

    public AuthenticationController(IConfiguration config,ILogger<AuthenticationController>logger,IAuthService supabaseAuth)
    {
        this._config = config;
        this._logger = logger;
        this._supabaseAuth = supabaseAuth;
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDataDTO Request)
    {
        var metadata = new Dictionary<string, object>
            {
                 { "first_name", Request.FirstName },
                 { "last_name", Request.LastName },
                 { "phone", Request.Phone }
            };
        var user = await _supabaseAuth.SignUpAsync(Request.Email, Request.Password, metadata);
        if (user == null)
            throw new BadHttpRequestException("The Post Call To api/SignUp Failled");

        return Ok(user);
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous]
    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] SignInDataDTO Request)
    {
        var Res = await _supabaseAuth.SignInAsync(Request.Email, Request.Password);
        if (Res == null)
            throw new BadHttpRequestException("The Post Call To api/SignIn Failled");
        return Ok(Res);
    }
    
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [HttpPost("SignOut")]
    public async Task<IActionResult> SignOutUser()
    {
        bool flag=await _supabaseAuth.Signout();
        if (!flag)
            throw new BadHttpRequestException("the Post Call to api/SignOut Failled");
        return Ok(flag);
    }
}
