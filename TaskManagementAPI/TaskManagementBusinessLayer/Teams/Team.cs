using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.TeamData;

namespace TaskManagementBusinessLayer.Teams;

public class Team : ITeam
{
    private readonly ITeamData _teamData;

    public Team(ITeamData teamData)
    {
        this._teamData = teamData;
    }
    public async Task<TeamDTO> AddNewTeam(TeamDTO team)
    {
        int newTeamID = await _teamData.AddNewTeam(team);
        return (newTeamID != -1) ? new TeamDTO(newTeamID, team.CreatedByProfileID, team.Name) : null!;
    }

    public async Task<bool> DeleteTeam(int TeamID)
    {
        return await _teamData.DeleteTeam(TeamID);
    }

    public Task<IEnumerable<TeamBaseModel>> GetAllMyTeams()
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateTeamName(int TeamID ,string Name)
    {
        return await _teamData.UpdateTeamName(TeamID, Name);
    }
}
