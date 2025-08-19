using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.InputValidation;
using TaskManagementBusinessLayer.Teams;
using TaskManagementBusinessLayer.Teams.TeamMembers;
using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.TeamData;


namespace TaskManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly ITeam _team;
    private readonly ITeamMember _teamMember;
    private readonly ILogger<TeamController> _logger;
    private readonly IValidateInput _validateInput;

    public TeamController(ITeam team,ITeamMember teamMember,ILogger<TeamController>logger,IValidateInput validateInput)
    {
        this._team = team;
        this._teamMember = teamMember;
        this._logger = logger;
        this._validateInput = validateInput;
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("GetAllMyTeams")]
    public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAllMyTeams()
    {
        _logger.LogInformation("GET api/GetAllMyTeams");
        var teamsList = await _team.GetAllMyTeams();
        if (teamsList != null)
            return Ok(teamsList);
        throw new BadRequestException("the GET call to api/GetAllMyTeams failled!");

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("GetAllTeamMembers/{TeamID}")]
    public async Task<ActionResult<IEnumerable<TeamDTO>>> GetAllTeamMembers(int TeamID)
    {
        _logger.LogInformation($"GET api/GetAllTeamMembers/{TeamID}");
        var teamMembersList = await _teamMember.GetAllTeamMembers(TeamID);
        if (teamMembersList != null)
            return Ok(teamMembersList);
        throw new BadRequestException($"the GET call to api/GetAllTeamMembers/{TeamID} failled!");

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("AddNewTeam")]
    public async Task<ActionResult<TeamDTO>> AddNewTeam([FromBody]TeamBaseModel newTeam)
    {
        _logger.LogInformation("POST api/AddNewTeam");
        if (!_validateInput.TeamDataValidator(newTeam))
            throw new BadRequestException("The POST call to api/AddNew Team failled due to Input Validation Error!");
        var newTeamDTO = new TeamDTO(newTeam.team_id, newTeam.created_by_profile_id, newTeam.name);
        newTeamDTO = await _team.AddNewTeam(newTeamDTO);
        if (newTeamDTO != null)
            return Ok(newTeamDTO);
        throw new BadRequestException("The POST call to api/AddNewTeam failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("UpdateTeamName/{TeamID}")]
    public async Task<IActionResult> UpdateTeamName(int TeamID, [FromBody]string NewName)
    {
        _logger.LogInformation($"PUT api/UpdateTeamName/{TeamID}");
        if (!_validateInput.StringWithOnlyLettersValidator(NewName))
            throw new BadRequestException($"The PUT Call to api/{TeamID}/UpdateTeamName failled due to Input Validation Error!");
        var team = await _team.Find(TeamID);
        if (team == null)
            throw new NotFoundException($"Team With ID {TeamID} was not Found!");
        if (await _team.UpdateTeamName(TeamID, NewName))
            return Ok($"Team Name with ID {TeamID} Has Been Updates Successfully!");
        throw new BadRequestException($"The PUT Call to api/{TeamID}/UpdateTeamName failled!");

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("UpdateTeamMemberRole/{TeamMemberID}")]
    public async Task<IActionResult> UpdateTeamMemberRole(int TeamMemberID, [FromBody] int RoleID)
    {
        _logger.LogInformation($"PUT api/UpdateTeamMemberRole/{TeamMemberID}");
        if (await _teamMember.UpdateTeamMemberRole(TeamMemberID, RoleID))
        {
            string RoleText = "";
            switch ((Roles)RoleID)
            {
                case Roles.enOwner:
                    RoleText = "Owner";
                    break;
                case Roles.enTeamLead:
                    RoleText = "Lead";
                    break;
                case Roles.enTeamMember:
                    RoleText = "Member";
                    break;
                case Roles.enGuest:
                    RoleText = "Guest";
                    break;
            }
            return Ok($"Team Member with ID {TeamMemberID} has his Role Updated to {RoleText}");
        }
        throw new BadRequestException($"The PUT Call to api/{TeamMemberID}/UpdateTeamName failled!");

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("DeleteTeam/{TeamID}")]
    public async Task<IActionResult> DeleteTeam(int TeamID)
    {
        _logger.LogInformation($"DELETE api/DeleteTeam/{TeamID}");
       
        if (await _team.DeleteTeam(TeamID))
            return Ok($"Team with ID {TeamID} Has Been Deleted Successfully!");
        throw new BadRequestException($"The DELETE Call to api/DeleteTeam/{TeamID} failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("DeleteTeamMember/{TeamMemberID}")]
    public async Task<IActionResult> DeleteTeamMember(int TeamMemberID)
    {
        _logger.LogInformation($"DELETE api/DeleteTeamMember/{TeamMemberID}");

        if (await _teamMember.DeleteTeamMember(TeamMemberID))
            return Ok($"Team Member with ID {TeamMemberID} Has Been Deleted Successfully!");
        throw new BadRequestException($"The DELETE Call to api/DeleteTeamMember/{TeamMemberID} failled!");
    }
}
