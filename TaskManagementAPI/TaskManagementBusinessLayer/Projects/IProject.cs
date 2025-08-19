using TaskManagementDataAccessLayer.ProjectData;

namespace TaskManagementBusinessLayer.Projects;

public interface IProject
{
    Task<bool> AddNewProject(ProjectDTO newProject);
    Task<ProjectDTO> Find(Guid ProjectID);
    Task<IEnumerable<ProjectDTO>> GetAllMyProjects();
    Task<bool> UpdateProjectInfo(ProjectDTO updatedProject);
    Task<bool> DeleteProject(Guid ProjectID);
    Task<IEnumerable<ProjectPerformanceDTO>> GetProjectsPerformance();
}
