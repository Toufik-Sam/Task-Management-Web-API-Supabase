

using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.TaskData.TaskCategories;

namespace TaskManagementBusinessLayer.Tasks.TaskCategory;

public interface ITaskCategory
{
    Task<TaskCategoryDTO> AddNewTaskCategory(TaskCategoryDTO taskCategoryDTO);
    Task<bool> UpdateTaskCategory(TaskCategoryDTO taskCategoryDTO);
    Task<bool> DeleteTaskCategory(int taskCategoryID,Guid ProjectID);
    Task<IEnumerable<TaskCategoryDTO>> GetTaskCategoriesByProjectID(Guid ProjectID);
    Task<TaskCategoryDTO> Find(int taskCatgeoryID,Guid ProjectID);
}
