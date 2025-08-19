using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.TeamData.TeamMemberData;

namespace TaskManagementBusinessLayer.Teams.TeamMembers
{
    public class TeamMember:ITeamMember
    {
        private readonly ITeamMemberData _teamMemberData;

        public TeamMember(ITeamMemberData teamMemberData)
        {
            this._teamMemberData = teamMemberData;
        }

        public async Task<bool> DeleteTeamMember(int TeamID)
        {
            return await _teamMemberData.DeleteTeamMemerRole(TeamID);
        }

        public async Task<IEnumerable<TeamMemberDTO>> GetAllTeamMembers(int TeamID)
        {
            var TeamMembers = await _teamMemberData.GetAllMyTeamMembers(TeamID);
            if (TeamMembers != null)
            {
                var TeamMembersDTO = new List<TeamMemberDTO>();
                foreach (var tm in TeamMembers)
                    TeamMembersDTO.Add(new TeamMemberDTO(tm.team_member_id, tm.first_name + " " + tm.last_name, tm.email, (Roles)tm.role_id));
                return TeamMembersDTO;
            }
            return null!;
        }

        public async Task<bool> IsMainTeamMember(int TeamID)
        {
            return await _teamMemberData.IsMainTeamMember(TeamID);
        }

        public async Task<bool> UpdateTeamMemberRole(int TeamMemberID, int newRole)
        {
            return await _teamMemberData.UpdateTeamMemberRole(TeamMemberID, newRole);
        }
    }
}
