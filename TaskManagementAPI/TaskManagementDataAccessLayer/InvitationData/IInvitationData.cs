using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementDataAccessLayer.InvitationData;

public interface IInvitationData
{
    Task<int> InviteNewTeamMember(InvitationDTO invitation);
    Task<bool> DeleteInvitation(int InvitationID);
    Task<IEnumerable<InvitationBaseModel>> GetAllMyInvitations();
    Task<bool> AcceptInvitation(int InvitationID);
}
