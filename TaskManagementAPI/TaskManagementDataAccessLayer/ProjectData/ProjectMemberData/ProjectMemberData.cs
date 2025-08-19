using System.Text.Json;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.CustomSupabaseClient;

namespace TaskManagementDataAccessLayer.ProjectData.ProjectMemberData
{
    public class ProjectMemberData : IProjectMemberData
    {
        private readonly ISupabaseClient _supabase;

        public ProjectMemberData(ISupabaseClient supabase)
        {
            this._supabase = supabase;
        }
        public async Task<int> AddNewProjectMember(ProjectMemberDTO project_member)
        {
            return JsonSerializer.Deserialize<int>(await _supabase.Rpc("sp_add_project_member",
                new { p_team_member_id = project_member.TeamMemberID, 
                      p_project_id=project_member.ProjectID}));
        }

        public async Task<bool> DeleteProjectMember(int ProjectMemberID)
        {
            return JsonSerializer.Deserialize<bool>
                (await _supabase.Rpc("sp_delete_project_member", new { p_project_member_id=ProjectMemberID }));
        }

        public async Task<IEnumerable<ProjectMemberBaseModel>> GetAllProjectMembers(Guid ProjectID)
        {
            return JsonSerializer.Deserialize<IEnumerable<ProjectMemberBaseModel>>
                (await _supabase.Rpc("sp_get_all_project_members", new { p_project_id = ProjectID }))!;
        }

        public async Task<IEnumerable<Profile>> GetProjectMemberProfileInfo(int ProjectMemberID, Guid ProjectID)
        {
            return JsonSerializer.Deserialize<IEnumerable<Profile>>
                 (await _supabase.Rpc("sp_get_project_member_profile_info", 
                 new { p_project_member_id = ProjectMemberID, p_project_id = ProjectID }))!;
        }

        public async Task<bool>IsProjectMember(Guid ProjectID,int ProjectMemberID)
        {
            return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_is_project_member", new { p_project_id = ProjectID,
                                                                                                      p_project_member_id=ProjectMemberID}));
        }
        
    }
}
