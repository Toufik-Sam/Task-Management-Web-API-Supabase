using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.ApplicationDTOs.AuthControllerDTOs;
using TaskManagementAPI.InputValidation;
using TaskManagementAPI.Services;
using TaskManagementBusinessLayer.Users;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.UserData;

namespace TaskManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IUser _user;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IValidateInput _validateInput;
    private readonly IAuthService _supabaseAuth;
    private readonly Supabase.Client _client;

    public AuthenticationController(Supabase.Client client,IUser user,IAuthService supabaseAuth,
        ILogger<AuthenticationController>logger,IValidateInput validateInput)
    {
        this._user = user;
        this._supabaseAuth = supabaseAuth;
        this._logger = logger;
        this._validateInput = validateInput;
        this._client = client;
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp([FromBody] SignUpDataDTO Request)
    {
        _logger.LogInformation("POST api/SignUp");
        
        if (!_validateInput.SignUpDataValidator(Request))
            throw new BadRequestException("The Post Call To api/SignUp Failled due to Input validation Error!");

        var UserMetaData = new Dictionary<string, object>
            {
                 { "first_name", Request.FirstName },
                 { "last_name", Request.LastName },
                 { "phone", Request.Phone }
            };
        var user = await _supabaseAuth.SignUpAsync(Request.Email, Request.Password, UserMetaData);
        if (user == null)
            throw new BadRequestException("The Post Call To api/SignUp Failled");

        return Ok(user);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [AllowAnonymous]
    [HttpPost("SignIn")]
    public async Task<IActionResult> SignIn([FromBody] SignInDataDTO Request)
    {
        _logger.LogInformation("POST api/SignIn");
        if(!_validateInput.SignInDataValidator(Request))
            throw new BadRequestException("The POST call api/SignIn failled due to Input Validation Error!");

        var Res = await _supabaseAuth.SignInAsync(Request.Email, Request.Password);
        if (Res == null)
            throw new BadRequestException("The POST call to api/SignIn failled!");

        UserDTO newUserProfile = new UserDTO(-1, Guid.Parse(Res.User.Id), (string)Res.User.UserMetadata["first_name"], 
            (string)Res.User.UserMetadata["last_name"], Res.User.Email, true);
        if (!await _user.DoesUserProfileExist(Res.AccessToken))
        {
            if (!await _user.AddNewProfile(newUserProfile,Res.AccessToken))
                throw new BadRequestException("The POST call api/SignIn failled!");
        }
        return Ok(Res);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("SignOut")]
    public async Task<IActionResult> SignOutUser()
    {
        _logger.LogInformation("POST api/SignOut");
        bool IsLoggedOut =await _supabaseAuth.Signout();
        if (!IsLoggedOut)
            throw new BadRequestException("the Post Call to api/SignOut Failled");
        return Ok("You have Been Logged Out Successfully !");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("RefreshToken")]
    public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
    {
        _logger.LogInformation("POST api/RefreshToken");
        if(string.IsNullOrEmpty(refreshToken))
            throw new BadRequestException("the POST call to api/RefreshToken failled due to Input Validation Error!");

        string accessToken = "";
        var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
        if (!string.IsNullOrWhiteSpace(authHeader) && authHeader.StartsWith("Bearer "))
            accessToken= authHeader.Substring("Bearer ".Length).Trim();
        if (await _client.Auth.SetSession(accessToken, refreshToken) != null && await _client.InitializeAsync() != null)
            await _client.Auth.RefreshToken();
        throw new BadRequestException("the POST call to api/RefreshToken failled!");

    }
}
