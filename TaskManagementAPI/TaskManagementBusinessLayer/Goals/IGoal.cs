

using TaskManagementDataAccessLayer.BaseModels;
using TaskManagementDataAccessLayer.GoalData;

namespace TaskManagementBusinessLayer.Goals;

public interface IGoal
{
    Task<GoalDTO> AddNewGoal(GoalDTO goalDTO);
    Task<IEnumerable<GoalDTO>> GetAllGoalsByProject(Guid projectID);
    Task<bool> UpdateGoal(GoalDTO goalDTO);
    Task<bool> DeleteGoal(int goalID);
    Task<GoalDTO> Find(int GoalID);
    Task<IEnumerable<GoalPerformanceDTO>> GetGoalsPerformance(Guid projectID);
}
