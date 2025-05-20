namespace TaskManagementDataAccessLayer.BaseModels;

public class ProjectBaseModel
{
    public Guid ProjectID { set; get; }
    public int OwnerID { set; get; }
    public string Title { set; get; }
    public string Description { set; get; }
    public int StatusID { set; get; }
    public int PriorityID { set; get; }
    public DateTime CreatedAt { set; get; }
    public ProjectBaseModel(Guid ProjectID,int OwnerID,string Title,string Description,int StatusID, int PriorityID, DateTime CreatedAt)
    {
        this.ProjectID = ProjectID;
        this.Title = Title;
        this.OwnerID=OwnerID;
        this.Description = Description;
        this.StatusID = StatusID;
        this.PriorityID = PriorityID;
        this.CreatedAt = CreatedAt;
    }
}
