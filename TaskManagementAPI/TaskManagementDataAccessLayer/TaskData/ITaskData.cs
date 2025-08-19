using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementDataAccessLayer.TaskData;

public interface ITaskData
{
    Task<int> AddNewTask(TaskDTO taskDTO);
    Task<IEnumerable<TaskBaseModel>> GetALLTasksByGoal(int goalID);
    Task<IEnumerable<TaskBaseModel>> GetAllTasksAssginedTo(int ProjectMemberID,Guid ProjectID);
    Task<bool> UpdateTask(TaskDTO taskDTO);
    Task<bool> DeleteTaskByID(int GoalID,int TaskID);
    Task<List<TaskPerformancePerMemberDTO>> GetTaskPerformanceByMember(Guid ProjectID);
}
