using Microsoft.AspNetCore.Mvc;
using TaskManagementBusinessLayer.Projects;
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
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "value1", "value2" };
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
    public async Task<ActionResult<ProjectDTO>> Post([FromBody]ProjectDTO project)
    {
        _logger.LogInformation("POST api/AddNewUserProject");
        var newProject = await _project.AddNewProject(project);
        if (newProject != null)
            return Ok(newProject);
        throw new BadRequestException("the POST call to api/AddNewUserProject failled!");
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
