using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.TeamData;

namespace TaskManagementBusinessLayer.Teams
{
    public interface ITeam
    {
        Task<TeamDTO> AddNewTeam(TeamDTO team);
        Task<IEnumerable<TeamBaseModel>> GetAllMyTeams();
        Task<bool> UpdateTeamName(int TeamID,string Name);
        Task<bool> DeleteTeam(int TeamID);
        Task<TeamDTO> Find(int TeamID);
    }
}
