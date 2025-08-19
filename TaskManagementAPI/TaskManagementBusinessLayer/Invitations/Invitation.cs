using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.InvitationData;

namespace TaskManagementBusinessLayer.Invitations;

public class Invitation:IInvitation
{
    private readonly IInvitationData _invitationData;

    public Invitation(IInvitationData invitationData)
    {
        this._invitationData = invitationData;
    }

    public async  Task<bool> AcceptInvitation(int InvitationID)
    {
        return await _invitationData.AcceptInvitation(InvitationID);
    }

    public async Task<bool> DeleteInvitation(int InvitationID)
    {
        return await _invitationData.DeleteInvitation(InvitationID);
    }

    public async Task<IEnumerable<InvitationDTO>> GetAllMyInvitations()
    {
        var invitationsList = await _invitationData.GetAllMyInvitations();
        if (invitationsList != null)
        {
            var InvitationsDTO = new List<InvitationDTO>();
            foreach (var inv in invitationsList)
                InvitationsDTO.Add(new InvitationDTO(inv.invitation_id, inv.sent_by, inv.received_by, inv.team_id, (Roles)inv.role_id,
                    inv.sent_at, inv.is_accepted));
            return InvitationsDTO;
        }
        return null!;
    }

    public async Task<InvitationDTO> InviteNewTeamMember(InvitationDTO invitation)
    {
        int newID = -1;
        newID= await _invitationData.InviteNewTeamMember(invitation);
        return newID != -1 ? new InvitationDTO(newID, invitation.SentBy, invitation.ReceivedBy, invitation.TeamID, invitation.Role, invitation.SentAt,
            false) : null!;
    }
}
