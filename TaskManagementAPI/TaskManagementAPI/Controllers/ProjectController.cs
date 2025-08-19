using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.InputValidation;
using TaskManagementBusinessLayer.Projects;
using TaskManagementBusinessLayer.Projects.ProjectMembers;
using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.ProjectData;
using TaskManagementDataAccessLayer.ProjectData.ProjectMemberData;

namespace TaskManagementAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProject _project;
    private readonly IProjectMember _projectMember;
    private readonly ILogger<ProjectController> _logger;
    private readonly IValidateInput _validateInput;

    public ProjectController(IProject project,IProjectMember projectMember, ILogger<ProjectController> logger,IValidateInput validateInput)
    {
        this._project = project;
        this._projectMember = projectMember;
        this._logger = logger;
        this._validateInput = validateInput;
    }
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("AllUserProjects")]
    public async Task<ActionResult<IEnumerable<ProjectDTO>>>GetAllUserProjects()
    {
        _logger.LogInformation("GET api/AllUserProjects");
        var userProjectsList = await _project.GetAllMyProjects();
        if (userProjectsList != null)
            return Ok(userProjectsList);
        throw new BadRequestException("The GET call to api/AllUserProjects failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("AllProjectMembers")]
    public async Task<ActionResult<IEnumerable<ProjectMemberDTO>>> GetAllProjectMembers(Guid ProjectID)
    {
        _logger.LogInformation("GET api/AllProjectMembers");
        var userProjectsList = await _projectMember.GetAllProjectMembers(ProjectID);
        if (userProjectsList != null)
            return Ok(userProjectsList);
        throw new BadRequestException("The GET call to api/AllProjectMembers failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpGet("GetProjectByID/{ProjectID}")]
    public async Task<ActionResult<ProjectDTO>> GetProjectByID(Guid ProjectID)
    {
        _logger.LogInformation($"GET api/GetProjectByID/{ProjectID}");
        var Project = await _project.Find(ProjectID);
        if (Project != null)
            return Ok(Project);
        throw new NotFoundException($"The GET call to api/GetProjectByID/{ProjectID} failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("AddNewUserProject")]
    public async Task<ActionResult<ProjectDTO>> AddNewProject([FromBody] ProjectBaseModel project)
    {
        _logger.LogInformation("POST api/AddNewUserProject");
        if (!_validateInput.ProjectDataValidator(project))
            throw new BadRequestException($"The Post call to api/AddNewUserProject failled due to Input Validation Error !");
        var newProjectDTO = new ProjectDTO(Guid.NewGuid(),project.owner_id, project.title, project.description, 
            Statuses.enPending, (Priorities)project.priority_id, project.created_at);
        var AddedProject = await _project.AddNewProject(newProjectDTO);
        if (AddedProject)
            return Ok(newProjectDTO);
        throw new BadRequestException("The POST call to api/AddNewUserProject failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPost("AddProjectMember")]
    public async Task<ActionResult<ProjectMemberDTO>> AddProjectMember([FromBody] ProjectMemberBaseModel project)
    {
        _logger.LogInformation("POST api/AddProjectMember");
        if (!_validateInput.ProjectMemberDataValidator(project))
            throw new BadRequestException("The POST call to api/AddProjectMember failled due to Input Validation Error !");
        var newProjectMemberDTO = new ProjectMemberDTO(-1, project.team_member_id, project.project_id);
        var addedProjectMember = await _projectMember.AddNewProjectMember(newProjectMemberDTO);
        if (addedProjectMember != null)
            return Ok(addedProjectMember);
        throw new BadRequestException("The POST call to api/AddProjectMember failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpPut("UpdateProject")]
    public async Task<ActionResult<ProjectDTO>> UpdateProject(Guid ProjectID,[FromBody] ProjectBaseModel project)
    {
        _logger.LogInformation("POST api/UpdateProject");
        if (!_validateInput.ProjectDataValidator(project))
            throw new BadRequestException("The PUT call to api/UpdateProject failled duo to Input Validation!");
        var Updateproject = await _project.Find(ProjectID);
        if (Updateproject == null)
            throw new NotFoundException($"Project with ID {ProjectID} was not FOUND !");
        Updateproject.OwnerID = project.owner_id;
        Updateproject.Title = project.title;
        Updateproject.Description = project.description;
        Updateproject.Status = (Statuses)project.status_id;
        Updateproject.Priority = (Priorities)project.priority_id;
        Updateproject.CreatedAt = project.created_at;

        if (await _project.UpdateProjectInfo(Updateproject))
            return Ok($"Project with ID {ProjectID} was updated successfully !");
        throw new BadRequestException("The PUT call to api/UpdateProject failled!");

    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("DeleteProject/{ProjectID}")]
    public async Task<IActionResult> DeleteProject(Guid ProjectID)
    {
        _logger.LogInformation($"DELETE api/DeleteProject/{ProjectID}");
        if (await _project.DeleteProject(ProjectID))
            return Ok($"Project with ID {ProjectID} was deleted Successfully!");
        throw new BadRequestException($"The DELETE call to api/DeleteProject/{ProjectID} failled!");
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [HttpDelete("ProjectMember/{ProjectMemberID}")]
    public async Task<IActionResult> DeleteProjectMember(int ProjectMemberID)
    {
        _logger.LogInformation($"DELETE api/ProjectMember/{ProjectMemberID}");
        if (await _projectMember.DeleteProjectMember(ProjectMemberID))
            return Ok($"Project Member with ID {ProjectMemberID} was deleted Successfully!");
        throw new BadRequestException($"The DELETE call to api/ProjectMember/{ProjectMemberID} failled!");
    }

}
