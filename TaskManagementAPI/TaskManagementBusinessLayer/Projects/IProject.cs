using TaskManagementDataAccessLayer.ProjectData;

namespace TaskManagementBusinessLayer.Projects;

public interface IProject
{
    Task<bool> AddNewProject(ProjectDTO newProject);
    Task<ProjectDTO> GetMyProjectInfoByID(Guid ProjectID);
    Task<IEnumerable<ProjectDTO>> GetAllMyProjects();
    Task<bool> UpdateProjectInfo(ProjectDTO updatedProject);
    Task<bool> DeleteProject(Guid ProjectID);
}
