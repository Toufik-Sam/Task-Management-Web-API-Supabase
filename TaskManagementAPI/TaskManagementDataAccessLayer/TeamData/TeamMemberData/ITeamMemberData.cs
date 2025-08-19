using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementDataAccessLayer.TeamData.TeamMemberData;

public interface ITeamMemberData
{
    Task<bool> UpdateTeamMemberRole(int TeamMemberID, int newRole);
    Task<bool> DeleteTeamMemerRole(int TeamMemberID);
    Task<IEnumerable<TeamMemberBaseModel>> GetAllMyTeamMembers(int TeamID);
    Task<bool> IsMainTeamMember(int TeamID);
}
