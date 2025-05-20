using Microsoft.AspNetCore.Mvc;
using TaskManagementBusinessLayer.Teams;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.TeamData;


namespace TaskManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController : ControllerBase
{
    private readonly ITeam _team;
    private readonly ILogger<TeamController> _logger;

    public TeamController(ITeam team,ILogger<TeamController>logger)
    {
        this._team = team;
        this._logger = logger;
    }
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
    }

    // GET api/<TeamController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("AddNewTeam")]
    public async Task<ActionResult<TeamDTO>> Post([FromBody]TeamBaseModel newTeam)
    {
        _logger.LogInformation("POST api/AddNewTeam");
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
        if (await _team.UpdateTeamName(TeamID, NewName))
            return Ok($"Team Name with ID {TeamID} Has Been Updates Successfully!");
        throw new BadRequestException($"The PUT Call to api/{TeamID}/UpdateTeamName failled!");

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("DeleteTeam/{TeamID}")]
    public async Task<IActionResult> DeleteTamName(int TeamID)
    {
        _logger.LogInformation($"DELETE api/DeleteTeam/{TeamID}");
        if (await _team.DeleteTeam(TeamID))
            return Ok($"Team Name with ID {TeamID} Has Been Deleted Successfully!");
        throw new BadRequestException($"The DELETE Call to api/DeleteTeam/{TeamID} failled!");
    }
}
