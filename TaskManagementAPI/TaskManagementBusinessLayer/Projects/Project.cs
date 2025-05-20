using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.ProjectData;

namespace TaskManagementBusinessLayer.Projects;

public class Project : IProject
{
    private readonly IProjectData _projectData;

    public Project(IProjectData projectData)
    {
        this._projectData = projectData;
    }
    public async Task<bool> AddNewProject(ProjectDTO newProject)
    {
        return await _projectData.AddNewProject(newProject);
    }

    public Task<bool> DeleteProject(Guid ProjectID)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<ProjectDTO>> GetAllMyProjects()
    {
        List<ProjectDTO> projects = new List<ProjectDTO>();
        var userProjects = await _projectData.GetAllMyProjects();
        if (userProjects!=null)
        {
            foreach (var project in userProjects)
                projects.Add(new ProjectDTO(project.ProjectID, project.OwnerID, project.Title, project.Description, (Statuses)project.StatusID,
                    (Priorities)project.PriorityID, project.CreatedAt));
            return projects;
        }
        return null!;
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
