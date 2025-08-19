
using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.TaskData.TaskCategories;

namespace TaskManagementBusinessLayer.Tasks.TaskCategory;

public class TaskCategory : ITaskCategory
{
    private readonly ITaskCategoryData _taskCategoryData;

    public TaskCategory(ITaskCategoryData taskCategoryData)
    {
        this._taskCategoryData = taskCategoryData;
    }
    public async Task<TaskCategoryDTO> AddNewTaskCategory(TaskCategoryDTO taskCategoryDTO)
    {
        int TaskCategoryID = await _taskCategoryData.AddNewTaskCategory(taskCategoryDTO);
        return TaskCategoryID > 0 ? new TaskCategoryDTO(TaskCategoryID, 
                                                        taskCategoryDTO.ProjectID,
                                                        taskCategoryDTO.Title, 
                                                        taskCategoryDTO.Description):null!;
    }

    public async Task<bool> DeleteTaskCategory(int taskCategoryID,Guid ProjectID)
    {
        return await _taskCategoryData.DeleteTaskCategory(taskCategoryID,ProjectID);
    }

    public async Task<TaskCategoryDTO> Find(int taskCatgeoryID,Guid ProjectID)
    {
        var temp = await _taskCategoryData.GeTaskCategoryByID(taskCatgeoryID,ProjectID);
        return new TaskCategoryDTO(temp.First().task_category_id,
                                   temp.First().project_id,
                                   temp.First().title,
                                   temp.First().description);
    }

    public async Task<IEnumerable<TaskCategoryDTO>> GetTaskCategoriesByProjectID(Guid ProjectID)
    {
        var taskCategories= await _taskCategoryData.GetTaskCategoriesByProjectID(ProjectID);
        var taskCategoriesList = new List<TaskCategoryDTO>();
        foreach (var item in taskCategories)
            taskCategoriesList.Add(new TaskCategoryDTO(item.task_category_id,item.project_id,item.title, item.description));
        return taskCategoriesList;
    }

    public async Task<bool> UpdateTaskCategory(TaskCategoryDTO taskCategoryDTO)
    {
        return await _taskCategoryData.UpdateTaskCategory(taskCategoryDTO);
    }

   
}
