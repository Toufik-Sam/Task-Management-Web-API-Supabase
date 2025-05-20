namespace TaskManagementDataAccessLayer.ProjectData
{
    public class ProjectDTO
    {
        public Guid ProjectID { set; get; }
        public int OwnerID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public Statuses Status { set; get; }
        public Priorities Priority { set; get; }
        public DateTime CreatedAt { set; get; }
        public ProjectDTO(Guid ProjectID, int OwnerID, string Title, string Description, Statuses Status, Priorities Priority, 
            DateTime CreatedAt)
        {
            this.ProjectID = ProjectID;
            this.OwnerID = OwnerID;
            this.Title = Title;
            this.Description = Description;
            this.Status = Status;
            this.Priority = Priority;
            this.CreatedAt = CreatedAt;
        }
    }
}
