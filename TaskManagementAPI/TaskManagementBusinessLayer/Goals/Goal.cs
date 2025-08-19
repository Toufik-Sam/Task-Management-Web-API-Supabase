
using TaskManagementBusinessLayer.Tasks;
using TaskManagementDataAccessLayer;
using TaskManagementDataAccessLayer.GoalData;
using TaskManagementDataAccessLayer.TaskData;

namespace TaskManagementBusinessLayer.Goals;

public class Goal : IGoal
{
    private readonly IGoalData _goalData;
    private readonly ITask _task;
    public Goal(IGoalData goalData,ITask task)
    {
        this._goalData = goalData;
        this._task = task;
    }
    public async Task<GoalDTO> AddNewGoal(GoalDTO goalDTO)
    {
        int newGoalID = await _goalData.AddNewGoal(goalDTO);
        return newGoalID > 0 ? new GoalDTO(newGoalID, goalDTO.BelongToProjectID, goalDTO.Title, goalDTO.Description, goalDTO.PlannedStartDate,
            goalDTO.PlannedEndDate, goalDTO.ActualStartDate, goalDTO.ActualEndDate, goalDTO.Status, goalDTO.Priority,
            goalDTO.CreatedByProjectMemberID,goalDTO.CreatedAt):null!;
    }
    public async Task<GoalDTO>Find(int GoalID)
    {
        var goal = await _goalData.GetGoalByID(GoalID);
        return goal != null ? new GoalDTO(
            goal.First().goal_id, 
            goal.First().belong_to_project_id, 
            goal.First().title, 
            goal.First().description,
            goal.First().planned_start_date, 
            goal.First().planned_end_date, 
            goal.First().actual_start_date,
            goal.First().actual_end_date, 
            (Statuses)goal.First().status_id, 
            (Priorities)goal.First().priority_id,
            goal.First().created_by_project_member_id, 
            goal.First().created_at) : null!;
    }
    public async Task<bool> DeleteGoal(int goalID)
    {
        return await _goalData.DeleteGoal(goalID);
    }
    public async Task<IEnumerable<GoalDTO>> GetAllGoalsByProject(Guid projectID)
    {
        var goalsList = await _goalData.GetAllGoalsByProject(projectID);
        if (goalsList != null)
        {
            var goals = new List<GoalDTO>();
            foreach (var goal in goalsList)
                goals.Add(new GoalDTO(goal.goal_id, goal.belong_to_project_id, goal.title, goal.description, goal.planned_start_date,
            goal.planned_end_date, goal.actual_start_date, goal.actual_end_date,(Statuses) goal.status_id,(Priorities) goal.priority_id,
            goal.created_by_project_member_id,goal.created_at));
            return goals;
        }
        return null!;
    }
    public async Task<bool> UpdateGoal(GoalDTO goalDTO)
    {
        return await _goalData.UpdateGoal(goalDTO);
    }
    public async Task<IEnumerable<GoalPerformanceDTO>> GetGoalsPerformance(Guid projectID)
    {
        var goals = await _goalData.GetAllGoalsByProject(projectID);
        if (goals != null)
        {
            var goalsPerformance = new List<GoalPerformanceDTO>();
            foreach (var goal in goals)
            {
                var goalPerformanceItem = new GoalPerformanceDTO();
                goalPerformanceItem.GoalID = goal.goal_id;
                goalPerformanceItem.Title = goal.title;
                goalPerformanceItem.Description = goal.description;
                goalPerformanceItem.priority = (Priorities)goal.priority_id;
                var tasksGraph = await _task.GetTaskGraph(goal.goal_id);
                if (tasksGraph != null)
                {
                    TaskPerformanceDTO tasksPerformance = tasksGraph.GetTasksPerformance();
                    goalPerformanceItem.tasksPerformance = tasksPerformance;
                    goalsPerformance.Add(goalPerformanceItem);
                }   
            }
            return goalsPerformance;
        }
        return null!;
    }
}
