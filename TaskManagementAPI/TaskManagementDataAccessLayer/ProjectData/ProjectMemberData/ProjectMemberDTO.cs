

namespace TaskManagementDataAccessLayer.ProjectData.ProjectMemberData
{
    public class ProjectMemberDTO
    {
        public int ProjectMemberID { set; get; }
        public int TeamMemberID { set; get; }
        public Guid ProjectID { set; get; }
        public ProjectMemberDTO(int ProjectMemberID,int TeamMemberID,Guid ProjectID)
        {
            this.ProjectMemberID = ProjectMemberID;
            this.TeamMemberID = TeamMemberID;
            this.ProjectID = ProjectID;
        }
    }
}
