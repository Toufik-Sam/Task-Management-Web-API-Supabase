using TaskManagementDataAccessLayer.ProjectData.ProjectMemberData;

namespace TaskManagementBusinessLayer.Projects.ProjectMembers
{
    public interface IProjectMember
    {
        Task<ProjectMemberDTO> AddNewProjectMember(ProjectMemberDTO projectMember);
        Task<bool> DeleteProjectMember(int projectMemberID);
        Task<IEnumerable<ProjectMemberDTO>> GetAllProjectMembers(Guid ProjectID);
        Task<bool> IsProjectMember(Guid ProjectID,int ProjectMemberID);
    }
}
