using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementDataAccessLayer.TaskData.TaskCategories
{
    public interface ITaskCategoryData
    {
        Task<int> AddNewTaskCategory(TaskCategoryDTO taskCategoryDTO);
        Task<bool> UpdateTaskCategory(TaskCategoryDTO taskCategoryDTO);
        Task<bool> DeleteTaskCategory(int taskCategoryID,Guid projectID);
        Task<IEnumerable<TaskCategoryBaseModel>> GetTaskCategoriesByProjectID(Guid ProjectID);
        Task<IEnumerable<TaskCategoryBaseModel>> GeTaskCategoryByID(int taskCategoryID,Guid projectID);
    }
}
