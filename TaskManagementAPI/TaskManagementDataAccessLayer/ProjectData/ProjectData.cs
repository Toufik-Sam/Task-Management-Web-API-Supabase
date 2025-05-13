
using System.Text.Json;
using TaskManagementDataAccessLayer.CustomSupabaseClient;

namespace TaskManagementDataAccessLayer.ProjectData;

public class ProjectData : IProjectData
{
    private readonly ISupabaseClient _supabase;

    public ProjectData(ISupabaseClient supabase)
    {
        this._supabase = supabase;
    }
    public async Task<Guid> AddNewProject(ProjectDTO newProject)
    {
        Guid newProjectID = Guid.Empty;
        newProjectID=JsonSerializer.Deserialize<Guid>(await _supabase.Rpc("sp_add_new_user_project", 
           new {p_owner_id=newProject.OwnerID,
               p_title=newProject.Title,
               p_desc=newProject.Description,
               p_status_id=newProject.Status,
               p_priority_id=newProject.Priority,
               p_created_at=newProject.CreatedAt}));
        return newProjectID;
    }

    public async Task<bool> DeleteProject(Guid ProjectID)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_delete_user_project", new {p_project_id=ProjectID}));
    }

    public async Task<IEnumerable<ProjectDTO>> GetAllMyProjects()
    {
        return JsonSerializer.Deserialize<IEnumerable<ProjectDTO>>(await _supabase.Rpc("sp_get_all_user_projects", new {}))!;
    }

    public async Task<ProjectDTO> GetMyProjectInfoByID(Guid ProjectID)
    {
        return JsonSerializer.Deserialize<ProjectDTO>(await _supabase.Rpc("sp_get_user_project_info_by_id", new {p_project_id=ProjectID}))!;
    }
    public async Task<bool> UpdateProjectInfo(ProjectDTO updatedProject)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_update_user_project",
          new
          {
              p_project_id = updatedProject.ProjectID,
              p_title = updatedProject.Title,
              p_desc = updatedProject.Description,
              p_status_id = updatedProject.Status,
              p_priority_id = updatedProject.Priority
          }));
    }
}
