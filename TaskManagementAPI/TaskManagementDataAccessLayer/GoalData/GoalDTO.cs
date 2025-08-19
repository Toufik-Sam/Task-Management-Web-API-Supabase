

namespace TaskManagementDataAccessLayer.GoalData;

public class GoalDTO
{
    public int GoalID { set; get; }
    public Guid BelongToProjectID { set; get; }
    public string Title { set; get; }
    public string Description { set; get; }
    public DateTime PlannedStartDate { set; get; }
    public DateTime PlannedEndDate { set; get; }
    public DateTime?ActualStartDate { set; get; }
    public DateTime?ActualEndDate { set; get; }
    public Statuses Status { set; get; }
    public Priorities Priority { set; get; }
    public int CreatedByProjectMemberID{ set; get; }
    public DateTime CreatedAt { set; get; }
    public GoalDTO(int GoalID, Guid BelongToProjectID, string Title, string Description, DateTime PlannedStartDate,
        DateTime PlannedEndDate, DateTime?ActualStartDate, DateTime?ActualEndDate, Statuses Status, Priorities Priority,
        int CreatedByProjectMemberID,DateTime CreatedAt)
    {
        this.GoalID = GoalID;
        this.BelongToProjectID = BelongToProjectID;
        this.Title = Title;
        this.Description = Description;
        this.PlannedStartDate = PlannedStartDate;
        this.PlannedEndDate = PlannedEndDate;
        this.ActualStartDate = ActualStartDate;
        this.ActualEndDate = ActualEndDate;
        this.Status = Status;
        this.Priority = Priority;
        this.CreatedByProjectMemberID = CreatedByProjectMemberID;
        this.CreatedAt = CreatedAt;
    }
}
