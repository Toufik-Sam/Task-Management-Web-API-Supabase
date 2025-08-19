using Microsoft.AspNetCore.Mvc;
using TaskManagementAPI.InputValidation;
using TaskManagementBusinessLayer.Goals;
using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.GoalData;


namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalController : ControllerBase
    {
        private readonly IGoal _goal;
        private readonly ILogger<GoalController> _logger;
        private readonly IValidateInput _validateInput;

        public GoalController(IGoal goal,ILogger<GoalController>logger,IValidateInput validateInput)
        {
            this._goal = goal;
            this._logger = logger;
            this._validateInput = validateInput;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("AllGoals")]
        public async Task<ActionResult<IEnumerable<GoalDTO>>>GetAllGoals(Guid projectID)
        {
            _logger.LogInformation("GET api/AllGoals");
            var goalsList = await _goal.GetAllGoalsByProject(projectID);
            if (goalsList != null)
                return Ok(goalsList);
            throw new  BadRequestException("The GET call to api/AllUserProjects failled!");
        }
       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("AddNewGoal")]
        public async Task<ActionResult<GoalDTO>>AddNewGoal([FromBody] GoalBaseModel goal)
        {
            _logger.LogInformation("POST api/AddNewGoal");
            if(!_validateInput.GoalDataValidator(goal))
                throw new BadRequestException("The POST call to api/AddNewGoal failled due to Input Validation Error!");

            var newGoal = new GoalDTO(
                  -1,
                   goal.belong_to_project_id,
                   goal.title,
                   goal.description,
                   goal.planned_start_date,
                   goal.planned_end_date,
                   null,
                   null,
                   (Statuses)goal.status_id,
                   (Priorities)goal.priority_id,
                   goal.created_by_project_member_id,
                   goal.created_at);
            var AddedGoal = await _goal.AddNewGoal(newGoal);
            if (AddedGoal != null)
                return Ok(AddedGoal);
            throw new BadRequestException("The POST call to api/AddNewGoal failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("{goalID}")]
        public async Task<IActionResult>UpdateGoal(int goalID, [FromBody] GoalBaseModel goal)
        {
            _logger.LogInformation($"PUT api/{goalID}");
            if (!_validateInput.GoalDataValidator(goal))
                throw new BadRequestException($"The PUT call to api/{goalID} failled due to Input Validation Error!");

            GoalDTO goalDTO = await _goal.Find(goalID);
            if (goalDTO == null)
                throw new NotFoundException($"Goal with ID {goalID} was not Found!");
           
            goalDTO.Title = goal.title;
            goalDTO.Description = goal.description;
            goalDTO.PlannedStartDate = goal.planned_start_date;
            goalDTO.PlannedEndDate = goal.planned_end_date;
            goalDTO.ActualStartDate = goal.actual_start_date;
            goalDTO.ActualEndDate = goal.actual_end_date;
            goalDTO.Status = (Statuses)goal.status_id;
            goalDTO.Priority = (Priorities)goal.priority_id;
            //goalDTO.CreatedByProjectMemberID = goal.created_by_project_member_id;
            goalDTO.CreatedAt = goal.created_at;
            if (await _goal.UpdateGoal(goalDTO))
                return Ok(goalDTO);
            throw new BadRequestException($"The PUT call to api/{goalID} failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("{goalID}")]
        public async Task<IActionResult>DeleteGoal(int goalID)
        {
            _logger.LogInformation($"DELETE api/{goalID}");
            GoalDTO goalDTO = await _goal.Find(goalID);
            if (goalDTO == null)
                throw new NotFoundException($"Goal with ID {goalID} was not Found!");
            
            if (await _goal.DeleteGoal(goalID))
                return Ok($"Goal with ID {goalID} has been deleted successfully!");
             throw new  BadRequestException($"The DELETE call to api/{goalID} failled!");
        }
    }
}
