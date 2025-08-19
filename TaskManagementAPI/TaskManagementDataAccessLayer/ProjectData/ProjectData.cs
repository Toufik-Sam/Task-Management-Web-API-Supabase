
using System.Text.Json;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.CustomSupabaseClient;

namespace TaskManagementDataAccessLayer.ProjectData;

public class ProjectData : IProjectData
{
    private readonly ISupabaseClient _supabase;

    public ProjectData(ISupabaseClient supabase)
    {
        this._supabase = supabase;
    }
    public async Task<bool> AddNewProject(ProjectDTO newProject)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_add_new_user_project",
           new
           {
               p_owner_id = newProject.OwnerID,
               p_title = newProject.Title,
               p_desc = newProject.Description,
               p_status_id = (int)newProject.Status,
               p_priority_id = (int)newProject.Priority,
               p_created_at = newProject.CreatedAt
           }));
    }

    public async Task<bool> DeleteProject(Guid ProjectID)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_delete_user_project", new {p_project_id=ProjectID}));
    }

    public async Task<IEnumerable<ProjectBaseModel>> GetAllMyProjects()
    {
        return JsonSerializer.Deserialize<IEnumerable<ProjectBaseModel>>(await _supabase.Rpc("sp_get_all_user_projects", new {}))!;
    }
    public async Task<ProjectBaseModel> GetMyProjectInfoByID(Guid ProjectID)
    {
        var project= JsonSerializer.Deserialize<IEnumerable<ProjectBaseModel>>(await _supabase.Rpc("sp_get_user_project_by_id", 
            new {p_project_id=ProjectID}))!;
        return project.FirstOrDefault()!;
    }
    public async Task<bool> UpdateProjectInfo(ProjectDTO updatedProject)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_update_project",
          new
          {
              p_project_id = updatedProject.ProjectID,
              p_owner_id=updatedProject.OwnerID,
              p_title = updatedProject.Title,
              p_description = updatedProject.Description,
              p_status_id = updatedProject.Status,
              p_priority_id = updatedProject.Priority,
              p_created_at=updatedProject.CreatedAt
          }));
    }
}
