namespace TaskManagementDataAccessLayer.BaseModels;

public class InvitationBaseModel
{
    public int invitation_id { set; get; }
    public int sent_by { set;get; }
    public int received_by { set; get; }
    public int team_id { set; get; }
    public int role_id { set; get; }
    public DateTime sent_at { set; get; }
    public bool is_accepted { set; get; }
}
