namespace TaskManagementDataAccessLayer.TaskData;

public class TaskDTO
{
    public int TaskID { get; set; }
    public string Title { set; get; }
    public string Description { set; get; }
    public DateTime CreatedAt { set; get; }
    public DateTime StartDate { set; get; }
    public DateTime EndDate { set; get; }
    public DateTime?ActualStartDate { set; get; }
    public DateTime?ActualEndDate { set; get; }
    public Statuses Status { set; get; }
    public Priorities Priority { set; get; }
    public int GoalID { set; get; }
    public int CreatedByProjectMemberID { set; get; }
    public int AssignedToProjectMemberID { set; get; }
    public int ParentTaskID { set; get; }
    public List<TaskDTO> SubTasks { set; get; }
    public int? task_category_id { set; get; }
    public TaskDTO(int TaskID,string Title,string Description,DateTime CreatedAt,DateTime StartDate,DateTime EndDate,
        DateTime?ActualStartDate,DateTime?ActualEndDate,Statuses Status,Priorities Priority, int GoalID,int CreatedByProjectMemberID,
        int AssignedToProjectMemberID, int ParentTaskID,int?task_category_id)
    {
        this.TaskID = TaskID;
        this.Title = Title;
        this.Description = Description;
        this.CreatedAt = CreatedAt;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
        this.ActualStartDate = ActualStartDate;
        this.ActualEndDate = ActualEndDate;
        this.Status = Status;
        this.Priority = Priority;
        this.GoalID = GoalID;
        this.CreatedByProjectMemberID = CreatedByProjectMemberID;
        this.AssignedToProjectMemberID = AssignedToProjectMemberID;
        this.ParentTaskID = ParentTaskID;
        this.SubTasks = new List<TaskDTO>();
        this.task_category_id = task_category_id;
    }
}
