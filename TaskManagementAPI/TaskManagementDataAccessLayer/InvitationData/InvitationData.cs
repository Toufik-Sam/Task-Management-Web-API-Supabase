using System.Text.Json;
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.CustomSupabaseClient;

namespace TaskManagementDataAccessLayer.InvitationData;

public class InvitationData : IInvitationData
{
    private readonly ISupabaseClient _supabase;

    public InvitationData(ISupabaseClient supabase)
    {
        this._supabase = supabase;
    }
    public async Task<bool> AcceptInvitation(int InvitationID)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_accept_invitation", new { p_invitation_id = InvitationID }))!;
    }

    public async Task<bool> DeleteInvitation(int InvitationID)
    {
        return JsonSerializer.Deserialize<bool>(await _supabase.Rpc("sp_delete_invitation", new { p_invitation_id = InvitationID }))!;
    }

    public async Task<IEnumerable<InvitationBaseModel>> GetAllMyInvitations()
    {
        return JsonSerializer.Deserialize<IEnumerable<InvitationBaseModel>>(await _supabase.Rpc("sp_get_all_my_invitations", new {}))!;
    }

    public async Task<int> InviteNewTeamMember(InvitationDTO invitation)
    {
        return JsonSerializer.Deserialize<int>(await _supabase.Rpc("sp_invite_team_member", 
            new { 
                  p_sent_by=invitation.SentBy,
                  p_received_by=invitation.ReceivedBy,
                  p_team_id=invitation.TeamID,
                  p_role_id=(int)invitation.Role,
                  p_sent_at=invitation.SentAt,
                  p_is_accepted=invitation.IsAccepted}))!;
    }
}
