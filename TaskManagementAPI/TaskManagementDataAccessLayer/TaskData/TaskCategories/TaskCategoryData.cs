
using System.Text.Json;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.CustomSupabaseClient;

namespace TaskManagementDataAccessLayer.TaskData.TaskCategories;

public class TaskCategoryData : ITaskCategoryData
{
    private readonly ISupabaseClient _supabase;
    public TaskCategoryData(ISupabaseClient supabase)
    {
        this._supabase = supabase;
    }
    public async Task<int> AddNewTaskCategory(TaskCategoryDTO taskCategoryDTO)
    {
        return JsonSerializer.Deserialize<int>(await _supabase.Rpc("sp_add_new_task_category",
            new
            {
                p_title = taskCategoryDTO.Title,
                p_project_id=taskCategoryDTO.ProjectID,
                p_description = taskCategoryDTO.Description
            }));
    }

    public async Task<bool> DeleteTaskCategory(int taskCategoryID,Guid project_id)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_delete_task_category",
            new
            {
                p_task_category_id = taskCategoryID,
                p_project_id=project_id
            }));
    }

    public async Task<IEnumerable<TaskCategoryBaseModel>> GeTaskCategoryByID(int taskCategoryID,Guid projectID)
    {
        return JsonSerializer.Deserialize<IEnumerable<TaskCategoryBaseModel>>(await _supabase.Rpc("sp_get_task_category_id",
                   new
                   {
                       p_task_category_id = taskCategoryID,
                       p_project_id=projectID
                   }))!;
    }

    public async Task<IEnumerable<TaskCategoryBaseModel>> GetTaskCategoriesByProjectID(Guid ProjectID)
    {
        return JsonSerializer.Deserialize<IEnumerable<TaskCategoryBaseModel>>(await _supabase.Rpc("sp_get_all_task_categories", 
            new {p_project_id=ProjectID}
            ))!;
    }

    public async Task<bool> UpdateTaskCategory(TaskCategoryDTO taskCategoryDTO)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_update_task_category", 
            new {
                 p_task_category_id=taskCategoryDTO.TaskCategoryID,
                 p_project_id=taskCategoryDTO.ProjectID,
                 p_title=taskCategoryDTO.Title,
                 p_description=taskCategoryDTO.Description }));
    }
}
