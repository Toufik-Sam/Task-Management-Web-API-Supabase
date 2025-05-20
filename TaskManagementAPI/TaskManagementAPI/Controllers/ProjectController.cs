using Microsoft.AspNetCore.Mvc;
using TaskManagementBusinessLayer.Projects;
using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.ProjectData;


namespace TaskManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProject _project;
    private readonly ILogger<ProjectController> _logger;

    public ProjectController(IProject project, ILogger<ProjectController> logger)
    {
        this._project = project;
        this._logger = logger;
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("GetAllUserProjects")]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>>GetAllUserProjects()
    {
        _logger.LogInformation("GET API/GetAllUserProjects");
        var userProjectsList = await _project.GetAllMyProjects();
        if (userProjectsList != null)
            return Ok(userProjectsList);
        throw new BadRequestException("The GET call to api/GetAllUserProjects failled!");
    }

    // GET api/<ProjectController>/5
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
    [HttpPost("AddNewUserProject")]
    public async Task<ActionResult<ProjectDTO>> Post([FromBody] ProjectBaseModel project)
    {
        _logger.LogInformation("POST api/AddNewUserProject");
        var newProjectDTO = new ProjectDTO(Guid.NewGuid(), project.OwnerID, project.Title, project.Description, 
            (Statuses)project.StatusID, (Priorities)project.PriorityID, project.CreatedAt);
        var AddedProject = await _project.AddNewProject(newProjectDTO);
        if (AddedProject)
            return Ok(newProjectDTO);
        throw new BadRequestException("The POST call to api/AddNewUserProject failled!");
    }

    // PUT api/<ProjectController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProjectController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
