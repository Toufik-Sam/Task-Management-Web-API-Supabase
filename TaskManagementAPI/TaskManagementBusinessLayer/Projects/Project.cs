using TaskManagementDataAccessLayer.ProjectData;

namespace TaskManagementBusinessLayer.Projects;

public class Project : IProject
{
    private readonly IProjectData _projectData;

    public Project(IProjectData projectData)
    {
        this._projectData = projectData;
    }
    public async Task<ProjectDTO> AddNewProject(ProjectDTO newProject)
    {
        Guid newProjectID = await _projectData.AddNewProject(newProject);
        return (newProjectID != Guid.Empty) ?
            new ProjectDTO(newProjectID,
                           newProject.OwnerID,
                           newProject.Title,
                           newProject.Description,
                           newProject.Status,
                           newProject.Priority,
                           newProject.CreatedAt) : null!;
    }

    public Task<bool> DeleteProject(Guid ProjectID)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProjectDTO>> GetAllMyProjects()
    {
        throw new NotImplementedException();
    }

    public Task<ProjectDTO> GetMyProjectInfoByID(Guid ProjectID)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProjectInfo(ProjectDTO updatedProject)
    {
        throw new NotImplementedException();
    }
}
