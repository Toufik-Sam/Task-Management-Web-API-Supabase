using System.Text.Json;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.CustomSupabaseClient;

namespace TaskManagementDataAccessLayer.TeamData;

public class TeamData : ITeamData
{
    private readonly ISupabaseClient _supabase;

    public TeamData(ISupabaseClient supabase)
    {
        this._supabase = supabase;
    }
    public async Task<int> AddNewTeam(TeamDTO team)
    {
        int newTeamID = -1;
        newTeamID = JsonSerializer.Deserialize<int>(await _supabase.Rpc("sp_add_new_team", 
        new
        {
            p_name = team.Name
        }));
        return newTeamID;
    }

    public async Task<bool> DeleteTeam(int TeamID)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_delete_team", new {p_team_id=TeamID}));
    }

    public async Task<IEnumerable<TeamBaseModel>> GetAllMyTeams()
    {
        return JsonSerializer.Deserialize<IEnumerable<TeamBaseModel>>(await _supabase.Rpc("sp_get_all_my_teams", new {}))!;
    }

    public async Task<bool> UpdateTeamName(int TeamID,string Name)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_update_team_name", new { p_team_id=TeamID,p_new_name = Name }));
    }
}
