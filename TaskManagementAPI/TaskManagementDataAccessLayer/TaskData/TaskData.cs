using System.Text.Json;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.CustomSupabaseClient;
using TaskManagementDataAccessLayer.ProjectData.ProjectMemberData;

namespace TaskManagementDataAccessLayer.TaskData;

public class TaskData : ITaskData
{
    private readonly ISupabaseClient _supabase;
    private readonly IProjectMemberData _projectMemberData;

    public TaskData(ISupabaseClient supabase,IProjectMemberData projectMemberData)
    {
        this._supabase = supabase;
        this._projectMemberData = projectMemberData;
    }
    public async Task<int> AddNewTask(TaskDTO taskDTO)
    {
        return JsonSerializer.Deserialize<int>(await _supabase.Rpc("sp_add_new_task",
                                  new
                                  {
                                      p_title = taskDTO.Title,
                                      p_description = taskDTO.Description,
                                      p_created_at=taskDTO.CreatedAt,
                                      p_start_date=taskDTO.StartDate,
                                      p_end_date=taskDTO.EndDate,
                                      p_status_id=(int)taskDTO.Status,
                                      p_priority_id=(int)taskDTO.Priority,
                                      p_goal_id=taskDTO.GoalID,
                                      p_created_by_project_member_id=taskDTO.CreatedByProjectMemberID,
                                      p_assigned_to_project_member_id=taskDTO.AssignedToProjectMemberID,
                                      p_parent_task_id=taskDTO.ParentTaskID
                                  }));
    }

    public async Task<bool> DeleteTaskByID(int GoalID,int TaskID)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_delete_task", new { p_task_id = TaskID,p_goal_id = GoalID }));
    }

    public async Task<IEnumerable<TaskBaseModel>> GetAllTasksAssginedTo(int ProjectMemberID,Guid ProjectID)
    {
        return JsonSerializer.Deserialize<IEnumerable<TaskBaseModel>>
            (await _supabase.Rpc("sp_get_all_tasks_assgined_to", new { p_project_member_id = ProjectMemberID,
                                                                       p_project_id=ProjectID}))!;
    }

    public async Task<IEnumerable<TaskBaseModel>> GetALLTasksByGoal(int goalID)
    {
        return JsonSerializer.Deserialize<IEnumerable<TaskBaseModel>>
            (await _supabase.Rpc("sp_get_goal_tasks", new { p_goal_id = goalID }))!;
    }

    public async Task<List<TaskPerformancePerMemberDTO>> GetTaskPerformanceByMember(Guid ProjectID)
    {
        var taskPerformancePerMembers = new List<TaskPerformancePerMemberDTO>();
        var projectMembers = await _projectMemberData.GetAllProjectMembers(ProjectID);
        if (projectMembers != null)
        {

            foreach (var member in projectMembers)
            {
                var taskPerformancePerMember = new TaskPerformancePerMemberDTO();
                taskPerformancePerMember.AssigneeID = member.project_member_id;
                var profileInfo = await _projectMemberData.GetProjectMemberProfileInfo(member.project_member_id, member.project_id);
                if (profileInfo != null)
                {
                    taskPerformancePerMember.FirstName = profileInfo.First().first_name;
                    taskPerformancePerMember.LastName = profileInfo.First().last_name;
                    taskPerformancePerMember.Email = profileInfo.First().email;
                    var taskList = await GetAllTasksAssginedTo(member.project_member_id, ProjectID);
                    if (taskList != null)
                    {
                        taskPerformancePerMember.TotalNumberOfTasks = taskList.Count();

                        taskPerformancePerMember.NumberOfCompletedTasks = 
                            taskList.Where(x => x.status_id == (int)Statuses.enCompleted).Count();
                        if (taskPerformancePerMember.TotalNumberOfTasks != 0)
                            taskPerformancePerMember.CompletionRate = (taskPerformancePerMember.NumberOfCompletedTasks /
                                                                  taskPerformancePerMember.TotalNumberOfTasks) * 100;
                        else
                            taskPerformancePerMember.CompletionRate = 0;

                        taskPerformancePerMember.NumberOfOverdueTasks = taskList.Where(x => x.status_id == (int)Statuses.enOverdue 
                                                                                                           || x.actual_end_date>x.end_date).Count();

                        TimeSpan TotalOverdueTime=TimeSpan.Zero;
                        foreach (var task in taskList)
                            if(task.status_id==(int)Statuses.enOverdue || task.actual_end_date > task.end_date)
                                TotalOverdueTime += task.actual_end_date!.Value - task.end_date;

                        taskPerformancePerMember.TasksOverdueTime = TotalOverdueTime;
                    }
                    taskPerformancePerMembers.Add(taskPerformancePerMember);
                }
            }
            return taskPerformancePerMembers;
        }
        return null!;
    }

    public async Task<bool> UpdateTask(TaskDTO taskDTO)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_update_task",
                                       new
                                       {
                                           p_task_id=taskDTO.TaskID,
                                           p_title = taskDTO.Title,
                                           p_goal_id=taskDTO.GoalID,
                                           p_description = taskDTO.Description,
                                           p_created_at = taskDTO.CreatedAt,
                                           p_start_date = taskDTO.StartDate,
                                           p_end_date = taskDTO.EndDate,
                                           p_status_id = (int)taskDTO.Status,
                                           p_priority_id = (int)taskDTO.Priority,
                                           p_created_by_project_member_id = taskDTO.CreatedByProjectMemberID,
                                           p_assigned_by_project_member_id = taskDTO.AssignedToProjectMemberID,
                                           p_actual_start_date=taskDTO.ActualStartDate,
                                           p_actual_end_date=taskDTO.ActualEndDate,
                                           p_task_category_id=taskDTO.task_category_id
                                       }));
    }


}
