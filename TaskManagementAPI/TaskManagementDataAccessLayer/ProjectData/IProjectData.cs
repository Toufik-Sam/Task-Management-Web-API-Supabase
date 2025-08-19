using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementDataAccessLayer.ProjectData;

public  interface IProjectData
{
    Task<bool> AddNewProject(ProjectDTO newProject);
    Task<ProjectBaseModel> GetMyProjectInfoByID(Guid ProjectID);
    Task<IEnumerable<ProjectBaseModel>> GetAllMyProjects();
    Task<bool> UpdateProjectInfo(ProjectDTO updatedProject);
    Task<bool> DeleteProject(Guid ProjectID);
}
