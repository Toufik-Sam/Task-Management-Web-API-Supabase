
using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementDataAccessLayer.ProjectData.ProjectMemberData
{
    public interface IProjectMemberData
    {
        Task<int> AddNewProjectMember(ProjectMemberDTO project_member);
        Task<bool> DeleteProjectMember(int ProjectMemberID);
        Task<bool> IsProjectMember(Guid ProjectID,int ProjectMemberID);
        Task<IEnumerable<ProjectMemberBaseModel>> GetAllProjectMembers(Guid ProjectID);
        Task<IEnumerable<Profile>> GetProjectMemberProfileInfo(int ProjectMemberID, Guid ProjectID);
    }
}
