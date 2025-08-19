

using TaskManagementDataAccessLayer.TaskData;

namespace TaskManagementBusinessLayer.Tasks;

public interface ITask
{
    Task<TaskDTO> AddNewTask(TaskDTO taskDTO);
    Task<IEnumerable<TaskDTO>> GetAllTaskByGoal(int goalID);
    Task<TaskGraph> GetTaskGraph(int goalID);
    Task<TaskDTO> Find(int GoalID,int TaskID);
    Task<bool> DeleteTask(int GoalID,int TaskID);
    Task<bool> UpdateTask(TaskDTO taskDTO);
    Task<List<TaskPerformancePerMemberDTO>> GetTaskPerformanceByMember(Guid projectID);

}
