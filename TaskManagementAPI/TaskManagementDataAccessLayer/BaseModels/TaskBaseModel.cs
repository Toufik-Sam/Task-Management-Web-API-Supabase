

namespace TaskManagementDataAccessLayer.BaseModels;

public class TaskBaseModel
{
    public int task_id { set; get; }
    public string title { set; get; }
    public string description { set; get; }
    public DateTime created_at { set; get; }
    public DateTime start_date { set; get; }
    public DateTime end_date { set; get; }
    public int status_id { set; get; }
    public int priority_id { set; get; }
    public int goal_id { set; get; }
    public int created_by_project_member_id { set; get; }
    public int assigned_to_project_member_id { set; get; }
    public int parent_task_id { set; get; }
    public DateTime? actual_start_date { set; get; }
    public DateTime? actual_end_date { set; get; }
    public int? task_category_id { set; get; }
}
