using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TaskManagementAPI.InputValidation;
using TaskManagementBusinessLayer.Tasks;
using TaskManagementBusinessLayer.Tasks.TaskCategory;
using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.Exceptions;
using TaskManagementDataAccessLayer.TaskData;
using TaskManagementDataAccessLayer.TaskData.TaskCategories;

namespace TaskManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITask _task;
        private readonly ILogger<TaskController> _logger;
        private readonly ITaskCategory _taskCategory;
        private readonly IValidateInput _validateInput;

        public TaskController(ITask task,ILogger<TaskController>logger,ITaskCategory taskCategory,IValidateInput validateInput)
        {
            this._task = task;
            this._logger = logger;
            this._taskCategory = taskCategory;
            this._validateInput = validateInput;
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("AllTasks")]
        public async Task<ActionResult<IEnumerable<TaskBaseModel>>> GetAllTasksByGoal(int GoalID)
        {
            _logger.LogInformation("GET api/AllTask");
            var tasksList = await _task.GetAllTaskByGoal(GoalID);
            if (tasksList != null)
                return Ok(tasksList);
            throw new BadRequestException("The GET call to api/AllTasks failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("AllTasksCategories")]
        public async Task<ActionResult<IEnumerable<TaskBaseModel>>> GetAllTasksCategoriesByProjectID(Guid ProjectID)
        {
            _logger.LogInformation("GET api/AllTasksCategories");
            var tasksCategoriesList = await _taskCategory.GetTaskCategoriesByProjectID(ProjectID);
            if (tasksCategoriesList != null)
                return Ok(tasksCategoriesList);
            throw new BadRequestException("The GET call to api/AllTasksCategories failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("AddNewTask")]
        public async Task<ActionResult<TaskDTO>> AddNewTask([FromBody] TaskBaseModel task)
        {
            _logger.LogInformation("GET api/AddNewTask");
            if (!_validateInput.TaskDataValidor(task))
                throw new BadRequestException("The POST call to api/AddNewTask failled due to Input Validation Error!");

            var parentTask = await _task.Find(task.goal_id, task.parent_task_id);

            if (parentTask == null && task.parent_task_id>0)
                throw new ForbiddenRequestException($"The Parent Task With ID {task.parent_task_id} is not part of tasks for " +
                    $"Goal with ID {task.parent_task_id}");

            var newTask = new TaskDTO(task.task_id, task.title, task.description, task.created_at, task.start_date, task.end_date
                ,null,null,Statuses.enPending, (Priorities)task.priority_id, task.goal_id, task.created_by_project_member_id, 
                task.assigned_to_project_member_id, (task.parent_task_id>0?task.parent_task_id:-1),task.task_category_id);
            var AddedTask = await _task.AddNewTask(newTask);

            if (AddedTask != null)
                return Ok(AddedTask);
            throw new BadRequestException("The POST call to api/AddNewTask failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("AddNewTaskCategory")]
        public async Task<ActionResult<TaskCategoryDTO>> AddNewTaskCategory([FromBody] TaskCategoryBaseModel taskCategory)
        {
            _logger.LogInformation("GET api/AddNewTaskCategory");
            if(!_validateInput.TaskCategoryDataValidor(taskCategory))
                throw new BadRequestException("The POST call to api/AddNewTaskCategory failled due to Input Validation Error!");

            var newTaskCategory = new TaskCategoryDTO(taskCategory.task_category_id,taskCategory.project_id,taskCategory.title,
                taskCategory.description);
            var AddedTaskCategory = await _taskCategory.AddNewTaskCategory(newTaskCategory);
            if (AddedTaskCategory != null)
                return Ok(AddedTaskCategory);
            throw new BadRequestException("The POST call to api/AddNewTaskCategory failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("UpdateTask/{TaskID}")]
        public async Task<IActionResult> UpdateTask(int TaskID, [FromBody] TaskBaseModel task)
        {
            _logger.LogInformation($"PUT api/UpdateTask/{TaskID}");

            if (!_validateInput.TaskDataValidor(task))
                throw new BadRequestException($"The PUT call to api/UpdateTask/{TaskID} failled due to Input Validation Error!");

            var updatedtask = await _task.Find(task.goal_id,TaskID);
            if (updatedtask == null)
                throw new NotFoundException($"Task Category with ID: {TaskID} was Not Found!");
            updatedtask.Title = task.title;
            updatedtask.Description = task.description;
            updatedtask.CreatedAt = task.created_at;
            updatedtask.StartDate = task.start_date;
            updatedtask.EndDate = task.end_date;
            updatedtask.Status = (Statuses)task.status_id;
            updatedtask.Priority = (Priorities)task.priority_id;
            //updatedtask.GoalID = task.goal_id;
            updatedtask.AssignedToProjectMemberID = task.assigned_to_project_member_id;
            updatedtask.ActualStartDate = task.actual_start_date;
            updatedtask.ActualEndDate = task.actual_end_date;
            updatedtask.task_category_id = task.task_category_id;
            
            if (await _task.UpdateTask(updatedtask))
                return Ok($"The Task with ID {TaskID} was updated successfully!");

            throw new BadRequestException($"The PUT call to api/UpdateTask/{TaskID} failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPut("UpdateTaskCategory/{TaskCategoryID}")]
        public async Task<IActionResult> UpdateTaskCategory(int TaskCategoryID, [FromBody] TaskCategoryBaseModel taskCategory)
        {
            _logger.LogInformation($"PUT api/UpdateTaskCategory/{TaskCategoryID}");
            if (!_validateInput.TaskCategoryDataValidor(taskCategory))
                throw new BadRequestException($"The PUT call to api/UpdateTask/{TaskCategoryID} failled due to Input Validation Error!");

            var updatedtaskCategory = await _taskCategory.Find(TaskCategoryID, taskCategory.project_id);
            if (updatedtaskCategory == null)
                throw new NotFoundException($"Task Category with ID: {TaskCategoryID} was Not Found!");
            updatedtaskCategory.Title = taskCategory.title;
            updatedtaskCategory.Description = taskCategory.description;

            if (await _taskCategory.UpdateTaskCategory(updatedtaskCategory))
                return Ok($"The Task Category with ID {TaskCategoryID} was updated successfully!");
            throw new BadRequestException($"The PUT call to api/UpdateTaskCategory/{TaskCategoryID} failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("/DeleteTask/{TaskID}")]
        public async Task<IActionResult> DeleteTask(int GoalID,int TaskID)
        {
            _logger.LogInformation($"DELETE api/DeleteTask/{TaskID}");
            if (await _task.DeleteTask(GoalID, TaskID))
                return Ok($"Task With ID {TaskID} was deleted successfully !");
            throw new BadRequestException($"The PUT call to api/UpdateTask/{TaskID} failled!");
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("/DeleteTaskCategory/{TaskCategoryID}")]
        public async Task<IActionResult> DeleteTaskCategory(int TaskCategoryID,Guid ProjectID)
        {
            _logger.LogInformation($"DELETE api/DeleteTaskCategory/{TaskCategoryID}");
            if (await _taskCategory.DeleteTaskCategory(TaskCategoryID,ProjectID))
                return Ok($"Task Category With ID {TaskCategoryID} was deleted successfully !");
            throw new BadRequestException($"The PUT call to api/DeleteTaskCategory/{TaskCategoryID} failled!");
        }
    }
}
