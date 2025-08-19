namespace TaskManagementDataAccessLayer.BaseModels;

public class ProjectBaseModel
{
    public Guid project_id { set; get; }
    public int owner_id { set; get; }
    public string title { set; get; }
    public string description { set; get; }
    public int status_id { set; get; }
    public int priority_id { set; get; }
    public DateTime created_at { set; get; }
}
