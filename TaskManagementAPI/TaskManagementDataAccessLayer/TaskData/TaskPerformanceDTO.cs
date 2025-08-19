namespace TaskManagementDataAccessLayer.TaskData;

public class TaskPerformanceDTO
{
    public int TotalNumberOfTasks { get; set; }
    public int NumberOfCompletedTasks { set; get; }
    public int CompletionRate { set; get; }
    public int NumberOfOverdueTasks { set; get; }
    public TimeSpan TasksOverdueTime { set; get; }
    public TaskPerformanceDTO(int TotalNumberOfTasks,int NumberOfCompletedTasks,int CompletionRate,
        int NumberOfOverdueTasks,TimeSpan TasksOverdueTime)
    {
        this.TotalNumberOfTasks = TotalNumberOfTasks;
        this.NumberOfCompletedTasks = NumberOfCompletedTasks;
        this.CompletionRate = CompletionRate;
        this.NumberOfOverdueTasks = NumberOfOverdueTasks;
        this.TasksOverdueTime = TasksOverdueTime;
    }

}
