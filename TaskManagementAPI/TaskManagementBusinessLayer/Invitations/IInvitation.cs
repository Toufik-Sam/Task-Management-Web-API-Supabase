using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.InvitationData;

namespace TaskManagementBusinessLayer.Invitations;

public interface IInvitation
{
    Task<InvitationDTO> InviteNewTeamMember(InvitationDTO invitation);
    Task<bool> DeleteInvitation(int InvitationID);
    Task<IEnumerable<InvitationDTO>> GetAllMyInvitations();
    Task<bool> AcceptInvitation(int InvitationID);
}
