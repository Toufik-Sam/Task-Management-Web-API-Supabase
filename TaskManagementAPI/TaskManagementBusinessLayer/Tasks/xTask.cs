

using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.ProjectData.ProjectMemberData;
using TaskManagementDataAccessLayer.TaskData;
using TaskManagementDataAccessLayer.TeamData.TeamMemberData;
using TaskManagementDataAccessLayer.UserData;

namespace TaskManagementBusinessLayer.Tasks;

public class xTask:ITask
{
    private readonly ITaskData _taskData;

    public xTask(ITaskData taskData)
    {
        this._taskData = taskData;

    }
    public async Task<TaskDTO> AddNewTask(TaskDTO taskDTO)
    {
        int TaskID = await _taskData.AddNewTask(taskDTO);
        return TaskID > 0 ? new TaskDTO(TaskID, taskDTO.Title, taskDTO.Description, taskDTO.CreatedAt, taskDTO.StartDate,
            taskDTO.EndDate,null,null,taskDTO.Status, taskDTO.Priority, taskDTO.GoalID, taskDTO.CreatedByProjectMemberID,
            taskDTO.AssignedToProjectMemberID, taskDTO.ParentTaskID,taskDTO.task_category_id) : null!;
    }

    public async Task<bool> DeleteTask(int GoalID,int TaskID)
    {
        return await _taskData.DeleteTaskByID(GoalID,TaskID);
    }

    public async Task<TaskDTO> Find(int GoalID,int TaskID)
    {
        var tasks = await _taskData.GetALLTasksByGoal(GoalID);
        if (tasks != null)
        {
            List<TaskDTO> tasksList = new List<TaskDTO>();
            foreach (var task in tasks)
                tasksList.Add(new TaskDTO(
                    task.task_id,
                    task.title,
                    task.description,
                    task.created_at,
                    task.start_date,
                    task.end_date,
                    task.actual_start_date,
                    task.actual_end_date,
                    (Statuses)task.status_id,
                    (Priorities)task.priority_id,
                    task.goal_id,
                    task.created_by_project_member_id,
                    task.assigned_to_project_member_id,
                    task.parent_task_id,
                    task.task_category_id));
            TaskGraph taskgraph = new TaskGraph(tasksList);
            if (!taskgraph.CreateEdges())
                return null!;
            return taskgraph.GetTaskByID(TaskID);
        }
        return null;
    }

    public async Task<IEnumerable<TaskDTO>> GetTasksListAssignedTo(int ProjectMember, Guid ProjectID)
    {
        var tasks = await _taskData.GetAllTasksAssginedTo(ProjectMember, ProjectID);
        if (tasks != null)
        {
            List<TaskDTO> tasksList = new List<TaskDTO>();
            foreach(var task in tasks)
            {
                tasksList.Add(new TaskDTO(
                    task.task_id,
                    task.title,
                    task.description,
                    task.created_at,
                    task.start_date,
                    task.end_date,
                    task.actual_start_date,
                    task.actual_end_date,
                    (Statuses) task.status_id,
                    (Priorities)task.priority_id,
                    task.goal_id,
                    task.created_by_project_member_id,
                    task.assigned_to_project_member_id,
                    task.parent_task_id,
                    task.task_category_id));
            }
            return tasksList;
        }
        return null;
    }

    public async Task<IEnumerable<TaskDTO>> GetAllTaskByGoal(int goalID)
    {
        var tasks = await _taskData.GetALLTasksByGoal(goalID);
        if (tasks != null)
        {
            List<TaskDTO> tasksList = new List<TaskDTO>();
            foreach (var task in tasks)
                tasksList.Add(new TaskDTO(
                     task.task_id,
                    task.title,
                    task.description,
                    task.created_at,
                    task.start_date,
                    task.end_date,
                    task.actual_start_date,
                    task.actual_end_date,
                    (Statuses)task.status_id,
                    (Priorities)task.priority_id,
                    task.goal_id,
                    task.created_by_project_member_id,
                    task.assigned_to_project_member_id,
                    task.parent_task_id,
                    task.task_category_id));
            TaskGraph taskgraph = new TaskGraph(tasksList);
            if (!taskgraph.CreateEdges())
                return null!;
            return taskgraph.GetTasks();
        }
        return null;
    }

    public async Task<bool> UpdateTask(TaskDTO taskDTO)
    {
        return await _taskData.UpdateTask(taskDTO);
    }

    public async Task<TaskGraph> GetTaskGraph(int goalID)
    {
        var tasks = await _taskData.GetALLTasksByGoal(goalID);
        if (tasks != null)
        {
            List<TaskDTO> tasksList = new List<TaskDTO>();
            foreach (var task in tasks)
                tasksList.Add(new TaskDTO(
                    task.task_id,
                    task.title,
                    task.description,
                    task.created_at,
                    task.start_date,
                    task.end_date,
                    task.actual_start_date,
                    task.actual_end_date,
                    (Statuses)task.status_id,
                    (Priorities)task.priority_id,
                    task.goal_id,
                    task.created_by_project_member_id,
                    task.assigned_to_project_member_id,
                    task.parent_task_id,
                    task.task_category_id));
            TaskGraph taskgraph = new TaskGraph(tasksList);
            if (taskgraph.CreateEdges())
                return taskgraph;
        }
        return null;
    }

    public async Task<List<TaskPerformancePerMemberDTO>> GetTaskPerformanceByMember(Guid projectID)
    {
        var taskPerformanceMembers = await _taskData.GetTaskPerformanceByMember(projectID);
        return (taskPerformanceMembers != null) ? taskPerformanceMembers : null!;

    }
}
