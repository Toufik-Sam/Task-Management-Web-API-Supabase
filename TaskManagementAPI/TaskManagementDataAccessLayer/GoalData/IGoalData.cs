

using TaskManagementDataAccessLayer.BaseModels;

namespace TaskManagementDataAccessLayer.GoalData;

public interface IGoalData
{
    Task<int> AddNewGoal(GoalDTO goalDTO);
    Task<IEnumerable<GoalBaseModel>> GetGoalByID(int GoalID);
    Task<IEnumerable<GoalBaseModel>> GetAllGoalsByProject(Guid projectID);
    Task<bool> UpdateGoal(GoalDTO goalDTO);
    Task<bool> DeleteGoal(int goalID);
}
