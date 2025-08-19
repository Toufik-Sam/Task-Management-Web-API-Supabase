
namespace TaskManagementDataAccessLayer.BaseModels
{
    public class GoalBaseModel
    {
        public int goal_id { set; get; }
        public Guid belong_to_project_id { set; get; }
        public string title { set; get; }
        public string description { set; get; }
        public int status_id { set; get; }
        public int priority_id { set; get; }
        public int created_by_project_member_id { set; get; }
        public DateTime planned_start_date { set; get; }
        public DateTime planned_end_date { set; get; }
        public DateTime?actual_start_date { set; get; }
        public DateTime?actual_end_date { set; get; }
        public DateTime created_at { set; get; }

    }
}
