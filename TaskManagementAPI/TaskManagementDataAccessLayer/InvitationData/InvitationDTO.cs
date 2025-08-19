namespace TaskManagementDataAccessLayer.InvitationData;

public class InvitationDTO
{
    public int InvitationID { set; get; }
    public int SentBy { set; get; }
    public int ReceivedBy { set; get; }
    public int TeamID { set; get; }
    public Roles Role { set; get; }
    public DateTime SentAt { set; get; }
    public bool IsAccepted { set; get; }
    public InvitationDTO(int InvitationID,int SentBy,int ReceivedBy,int TeamID,Roles Role,DateTime SentAt,bool IsAccepted)
    {
        this.InvitationID = InvitationID;
        this.SentBy = SentBy;
        this.ReceivedBy = ReceivedBy;
        this.TeamID = TeamID;
        this.Role = Role;
        this.SentAt = SentAt;
        this.IsAccepted = IsAccepted;
    }
}
