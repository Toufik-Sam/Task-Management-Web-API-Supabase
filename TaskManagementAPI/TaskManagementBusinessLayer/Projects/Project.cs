using TaskManagementBusinessLayer.Goals;
using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.ProjectData;

namespace TaskManagementBusinessLayer.Projects;

public class Project : IProject
{
    private readonly IProjectData _projectData;
    private readonly IGoal _goal;

    public Project(IProjectData projectData,IGoal goal)
    {
        this._projectData = projectData;
        this._goal = goal;
    }

    public async Task<bool> AddNewProject(ProjectDTO newProject)
    {
        return await _projectData.AddNewProject(newProject);
    }

    public async Task<bool> DeleteProject(Guid ProjectID)
    {
        return await _projectData.DeleteProject(ProjectID);
    }

    public async Task<IEnumerable<ProjectDTO>> GetAllMyProjects()
    {
        var projects = new List<ProjectDTO>();
        var userProjects = await _projectData.GetAllMyProjects();
        if (userProjects!=null)
        {
            foreach (var project in userProjects)
                projects.Add(new ProjectDTO(project.project_id, project.owner_id, project.title, project.description, 
                    (Statuses)project.status_id,(Priorities)project.priority_id, project.created_at));
            return projects;
        }
        return null!;
    }

    public async Task<ProjectDTO> Find(Guid ProjectID)
    {
        var project = await _projectData.GetMyProjectInfoByID(ProjectID);
        if (project != null)
            return new ProjectDTO(project.project_id, project.owner_id, project.title, project.description,
                (Statuses)project.status_id, (Priorities)project.priority_id, project.created_at);
        return null!;
    }

    public async Task<bool> UpdateProjectInfo(ProjectDTO updatedProject)
    {
        return await _projectData.UpdateProjectInfo(updatedProject);
    }

    public async Task<IEnumerable<ProjectPerformanceDTO>> GetProjectsPerformance()
    {
        var projects = await _projectData.GetAllMyProjects();
        if (projects != null)
        {
            var projectsPerformance = new List<ProjectPerformanceDTO>();
            foreach(var project in projects)
            {
                var projectPerformanceItem = new ProjectPerformanceDTO();
                projectPerformanceItem.ProjectID = project.project_id;
                projectPerformanceItem.Title = project.title;
                projectPerformanceItem.Description = project.description;
                projectPerformanceItem.Priority = (Priorities)project.priority_id;
                var goalsPerformance = await _goal.GetGoalsPerformance(project.project_id);
                projectPerformanceItem.GoalsPerformances = goalsPerformance;
                projectsPerformance.Add(projectPerformanceItem);
            }
            return projectsPerformance;
        }
        return null!;
    }
}
