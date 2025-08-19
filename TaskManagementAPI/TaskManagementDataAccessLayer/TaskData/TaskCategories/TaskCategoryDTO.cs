

namespace TaskManagementDataAccessLayer.TaskData.TaskCategories
{
    public class TaskCategoryDTO
    {
        public int TaskCategoryID { set; get; }
        public Guid ProjectID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public TaskCategoryDTO(int TaskCategoryID,Guid ProjectID,string Title,string Description)
        {
            this.TaskCategoryID = TaskCategoryID;
            this.ProjectID = ProjectID;
            this.Title = Title;
            this.Description = Description;
        }
    }
}
