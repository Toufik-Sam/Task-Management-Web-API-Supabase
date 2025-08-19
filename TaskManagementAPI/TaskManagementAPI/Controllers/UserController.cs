using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.InputValidation;
using TaskManagementBusinessLayer.Users;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.UserData;
namespace TaskManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUser _user;
    private readonly ILogger<UserController> _logger;
    private readonly IValidateInput _validateInput;
    private readonly Supabase.Client _supabase;

    public UserController(Supabase.Client supabse,IUser user,ILogger<UserController>logger,IValidateInput validateInput)
    {
        this._supabase = supabse;
        this._user = user;
        this._logger = logger;
        this._validateInput = validateInput;
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
    public async Task<IActionResult> UpdateProfile(string Email,[FromBody]Profile profileBaseModel)
    {
        _logger.LogInformation("PUT api/UpdateProfileInfo");
        if(!_validateInput.ProfileDataValidator(profileBaseModel))
            throw new BadRequestException("the PUT call to api/UpdateProfileInfo failled due to Input Validation Error!");

        var CurrentProfile = await _user.Find(Email);
        if (CurrentProfile == null)
            throw new NotFoundException($"Profile Was Not Found!");
        CurrentProfile.FirstName = profileBaseModel.first_name;
        CurrentProfile.LastName = profileBaseModel.last_name;
        CurrentProfile.IsActive = profileBaseModel.is_active;
        if (await _user.UpdateUserProfileInfo(CurrentProfile))
            return Ok($"Profile with Email:{Email} Has Been Updated !");
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
        if(!_validateInput.EmailValidator(Email))
            throw new BadRequestException("the PUT call to api/UpdatePassword failled due to Input Validation Error!");

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
