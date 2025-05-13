using Microsoft.AspNetCore.Mvc;
using TaskManagementBusinessLayer.Users;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.UserData;
namespace TaskManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUser _user;
    private readonly ILogger<UserController> _logger;
    private readonly Supabase.Client _supabase;

    public UserController(Supabase.Client supabse,IUser user,ILogger<UserController>logger)
    {
        this._supabase = supabse;
        this._user = user;
        this._logger = logger;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("CurrentProfile")]
    public async Task<ActionResult<UserDTO>>GetCurrentAuthProfile()
    {
        _logger.LogInformation("GET api/ProfileInfo");
        var profile = await _user.Find();
        if (profile != null)
            return Ok(profile);
        else
            throw new BadRequestException("the GET call to api/ProfileInfo failled!");

    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("ProfileInfoByEmail")]
    public async Task<ActionResult<UserDTO>> GetUserProfile(string Email)
    {
        _logger.LogInformation("GET api/ProfileInfo");
        var profile = await _user.Find(Email);
        if (profile != null)
            return Ok(profile);
        else
            throw new BadRequestException("the GET call to api/ProfileInfoByEmail failled!");

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("UpdateProfileInfo")]
    public async Task<IActionResult> UpdateProfile([FromBody]UserDTO newUser)
    {
        _logger.LogInformation("PUT api/UpdateProfileInfo");
        
        var updatedProfile = await _user.UpdateUserProfileInfo(newUser);
        if (updatedProfile != null)
            return Ok(updatedProfile);
        else
            throw new BadRequestException("the PUT call to api/UpdateProfileInfo failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("UpdatePassword")]
    public async Task<IActionResult> UpdatePassword([FromBody]string Email)
    {
        _logger.LogInformation("PUT api/UpdatePassword");
        await _supabase.InitializeAsync();
        var responce=await _supabase.Auth.ResetPasswordForEmail(Email);
        if(responce)
            return Ok("Password Reset Link Has Been sent to your email ");
        throw new BadRequestException("Failed to Send Password Reset Link !");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("SoftDelete")]
    public async Task<IActionResult> DeactivateAccount()
    {
        _logger.LogInformation("DELETE api/SoftDelete");

        if (await _user.DeactivateUserProfile())
            return Ok("Account was Deactivated Successfully !");
        else
            throw new BadRequestException("the GET call to api/SoftDelete failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("Delete")]
    public async Task<IActionResult> DeleteUser()
    {
        _logger.LogInformation("DELETE api/Delete");

        if (await _user.DeleteUserAndProfile())
            return Ok("User was Deleted Successfully !");
        else
            throw new BadRequestException("the GET call to api/Delete failled!");
        
    }
}
