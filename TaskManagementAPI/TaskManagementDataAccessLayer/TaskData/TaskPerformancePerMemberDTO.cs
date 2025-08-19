

namespace TaskManagementDataAccessLayer.TaskData;

public class TaskPerformancePerMemberDTO
{
    public int AssigneeID { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public string Email { set; get; }
    public int TotalNumberOfTasks { get; set; }
    public int NumberOfCompletedTasks { set; get; }
    public int CompletionRate { set; get; }
    public int NumberOfOverdueTasks { set; get; }
    public TimeSpan TasksOverdueTime { set; get; }
    public TaskPerformancePerMemberDTO(int AssigneeID, string FirstName, string LastName,string Email, int TotalNumberOfTasks,int NumberOfCompletedTasks,
        int CompletionRate,int NumberOfOverdueTasks,TimeSpan TasksOverdueTime)
    {
        this.AssigneeID = AssigneeID;
        this.FirstName = FirstName;
        this.LastName = LastName;
        this.Email = Email;
        this.TotalNumberOfTasks = TotalNumberOfTasks;
        this.NumberOfCompletedTasks = NumberOfCompletedTasks;
        this.CompletionRate = CompletionRate;
        this.NumberOfOverdueTasks = NumberOfOverdueTasks;
        this.TasksOverdueTime = TasksOverdueTime;
    }
    public TaskPerformancePerMemberDTO()
    {
        this.AssigneeID =-1;
        this.FirstName = "";
        this.LastName = "";
        this.Email = "";
        this.TotalNumberOfTasks = -1;
        this.NumberOfCompletedTasks = -1;
        this.CompletionRate = -1;
        this.NumberOfOverdueTasks = -1;
        this.TasksOverdueTime = TimeSpan.Zero;
    }
}
