using System.Text.Json;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.CustomSupabaseClient;

namespace TaskManagementDataAccessLayer.TeamData.TeamMemberData;

public class TeamMemberData : ITeamMemberData
{
    private readonly ISupabaseClient _supabase;

    public TeamMemberData(ISupabaseClient supabase)
    {
        this._supabase = supabase;
    }
    public async Task<bool> DeleteTeamMemerRole(int TeamMemberID)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_delete_team_member", new { p_team_member_id = TeamMemberID }));
    }
    public async Task<IEnumerable<TeamMemberBaseModel>> GetAllMyTeamMembers(int TeamID)
    {
        return JsonSerializer.Deserialize<IEnumerable<TeamMemberBaseModel>>(await _supabase.Rpc("sp_get_all_team_members", 
            new {p_team_id = TeamID}
            ))!;
    }
    public async Task<bool> IsMainTeamMember(int TeamID)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_is_main_team_member", new { p_team_id = TeamID }));
    }
    public async Task<bool> UpdateTeamMemberRole(int TeamMemberID, int newRole)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_update_team_member_role", new {p_team_member_id=TeamMemberID,
                                                                                                       p_new_role_id=newRole}));
    }
}
