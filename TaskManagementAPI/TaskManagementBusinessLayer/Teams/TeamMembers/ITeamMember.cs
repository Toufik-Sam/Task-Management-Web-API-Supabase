using TaskManagementDataAccessLayer.TeamData.TeamMemberData;

namespace TaskManagementBusinessLayer.Teams.TeamMembers;

public interface ITeamMember
{
    Task<bool> UpdateTeamMemberRole(int TeamMemberID, int newRole);
    Task<IEnumerable<TeamMemberDTO>> GetAllTeamMembers(int TeamID);
    Task<bool> DeleteTeamMember(int TeamID);
    Task<bool> IsMainTeamMember(int TeamID);
}
