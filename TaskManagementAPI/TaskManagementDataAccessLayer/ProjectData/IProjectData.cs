namespace TaskManagementDataAccessLayer.ProjectData;

public  interface IProjectData
{
    Task<Guid> AddNewProject(ProjectDTO newProject);
    Task<ProjectDTO>GetMyProjectInfoByID(Guid ProjectID);
    Task<IEnumerable<ProjectDTO>> GetAllMyProjects();
    Task<bool> UpdateProjectInfo(ProjectDTO updatedProject);
    Task<bool> DeleteProject(Guid ProjectID);

}
