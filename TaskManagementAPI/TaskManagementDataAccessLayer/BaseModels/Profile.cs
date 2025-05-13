namespace TaskManagementDataAccessLayer.BaseModels;
public class Profile
{
    public int profile_id { get; set; }
    public Guid user_id { set; get; }
    public string first_name { get; set; }
    public string last_name { get; set; }
    public string email { set; get; }
    public bool is_active { set; get; }
}
