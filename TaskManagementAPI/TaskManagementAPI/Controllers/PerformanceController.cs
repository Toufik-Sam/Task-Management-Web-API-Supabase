using Microsoft.AspNetCore.Mvc;
using TaskManagementBusinessLayer.Goals;
using TaskManagementBusinessLayer.Projects;
using TaskManagementBusinessLayer.Tasks;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.GoalData;
using TaskManagementDataAccessLayer.ProjectData;
using TaskManagementDataAccessLayer.TaskData;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformanceController : ControllerBase
    {
        private readonly ILogger<PerformanceController> _logger;
        private readonly IGoal _goal;
        private readonly IProject _project;
        private readonly ITask _task;

        public PerformanceController(ILogger<PerformanceController> logger, IGoal goal, IProject project, ITask task)
        {
            this._logger = logger;
            this._goal = goal;
            this._project = project;
            this._task = task;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("/GoalPerformance/{projectID}")]
        public async Task<ActionResult<List<GoalPerformanceDTO>>>GetGoalsAnalytics(Guid projectID)
        {
            _logger.LogInformation($"GET api/GoalPerformance/{projectID}");
            var goalsPerformance = await _goal.GetGoalsPerformance(projectID);
            if (goalsPerformance != null)
                return Ok(goalsPerformance);

            throw new BadRequestException($"The GET call to api/GoalPerformance/{projectID} failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ProjectsPerformance")]
        public async Task<ActionResult<List<ProjectPerformanceDTO>>> GetProjectsAnalytics()
        {
            _logger.LogInformation("GET api/ProjectsPerformance");
            var projectsPerformance = await _project.GetProjectsPerformance();
            if (projectsPerformance != null)
                return Ok(projectsPerformance);
            throw new BadRequestException("The GET api/ProjectsPerformance failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("ProjectMembersPerformance")]
        public async Task<ActionResult<List<TaskPerformancePerMemberDTO>>> GetProjectMembersAnalytics(Guid ProjectID)
        {
            _logger.LogInformation("GET api/ProjectMembersPerformance");
            var projectMembersPerformance = await _task.GetTaskPerformanceByMember(ProjectID);
            if (projectMembersPerformance != null)
                return Ok(projectMembersPerformance);
            throw new BadRequestException("The GET api/ProjectPerformance failled!");
        }
    }
}
