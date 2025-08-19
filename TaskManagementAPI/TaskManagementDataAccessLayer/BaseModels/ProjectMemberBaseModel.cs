

namespace TaskManagementDataAccessLayer.BaseModels
{
    public class ProjectMemberBaseModel
    {
        public int project_member_id { get; set; }
        public int team_member_id { get; set; }
        public Guid project_id { set; get; }
    }
}
