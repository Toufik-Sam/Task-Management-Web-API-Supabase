namespace TaskManagementDataAccessLayer.BaseModels;

public class TeamMemberBaseModel
{
    public int team_member_id{set;get;}
    public string first_name { set; get; }
    public string last_name { set; get; }
    public string email { set; get; }
    public int role_id { set; get; }
}
