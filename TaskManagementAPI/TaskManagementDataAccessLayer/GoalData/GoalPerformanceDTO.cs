

using TaskManagementDataAccessLayer.TaskData;

namespace TaskManagementDataAccessLayer.GoalData;

public class GoalPerformanceDTO
{
    public int GoalID { get; set; }
    public string Title { set; get; }
    public string Description { set; get; }
    public Priorities priority { set; get; }
    public TaskPerformanceDTO tasksPerformance { set; get; }
    public GoalPerformanceDTO(int GoalID,string Title,string Description,Priorities priority,TaskPerformanceDTO tasksPerformance)
    {
        this.GoalID = GoalID;
        this.Title = Title;
        this.Description = Description;
        this.priority = priority;
        this.tasksPerformance = tasksPerformance;
    }
    public GoalPerformanceDTO()
    {
        this.GoalID = -1;
        this.Title = "";
        this.Description = "";
        this.tasksPerformance = null!;
    }
}
