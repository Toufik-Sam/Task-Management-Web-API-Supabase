

using TaskManagementDataAccessLayer.ProjectData.ProjectMemberData;

namespace TaskManagementBusinessLayer.Projects.ProjectMembers
{
    public class ProjectMember:IProjectMember
    {
        private readonly IProjectMemberData _projectMemberData;

        public ProjectMember(IProjectMemberData projectMemberData)
        {
            this._projectMemberData = projectMemberData;
        }

        public async Task<ProjectMemberDTO> AddNewProjectMember(ProjectMemberDTO projectMember)
        {
            int ProjectMemberID = await _projectMemberData.AddNewProjectMember(projectMember);
            return (ProjectMemberID>0)?new ProjectMemberDTO(ProjectMemberID, projectMember.TeamMemberID, projectMember.ProjectID):null!;
        }

        public async Task<bool> IsProjectMember(Guid ProjectID,int ProjectMemberID)
        {
            return await _projectMemberData.IsProjectMember(ProjectID,ProjectMemberID);
        }

        public async Task<bool> DeleteProjectMember(int projectMemberID)
        {
            return await _projectMemberData.DeleteProjectMember(projectMemberID);
        }

        public async Task<IEnumerable<ProjectMemberDTO>> GetAllProjectMembers(Guid ProjectID)
        {
            var projectsList= await _projectMemberData.GetAllProjectMembers(ProjectID);
            if(projectsList!=null)
            {
                var projects = new List<ProjectMemberDTO>();
                foreach (var project in projectsList)
                    projects.Add(new ProjectMemberDTO(project.project_member_id, project.team_member_id, project.project_id));
                return projects;
            }
            return null!;
        }
    }
}
