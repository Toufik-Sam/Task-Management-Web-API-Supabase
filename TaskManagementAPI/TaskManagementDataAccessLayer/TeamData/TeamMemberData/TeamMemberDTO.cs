namespace TaskManagementDataAccessLayer.TeamData.TeamMemberData;

public class TeamMemberDTO
{
    public int TeamMemberID { set; get; }
    public string FullName { set; get; }
    public string Email { set; get; }
    public Roles Role { set; get; }
    public TeamMemberDTO(int TeamMemberID,string FullName,string Email,Roles Role)
    {
        this.TeamMemberID = TeamMemberID;
        this.FullName = FullName;
        this.Email = Email;
        this.Role = Role;
    }
}
