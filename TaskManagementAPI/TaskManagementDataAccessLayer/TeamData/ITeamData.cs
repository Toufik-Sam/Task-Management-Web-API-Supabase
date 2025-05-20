using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementDataAccessLayer.TeamData
{
    public interface ITeamData
    {
        Task<int> AddNewTeam(TeamDTO team);
        Task<IEnumerable<TeamBaseModel>>GetAllMyTeams();
        Task<bool> UpdateTeamName(int TeamID,string Name);
        Task<bool> DeleteTeam(int TeamID);
    }
}
