

namespace TaskManagementDataAccessLayer.BaseModels;

public class TaskCategoryBaseModel
{
    public int task_category_id { set; get; }
    public string title { set; get; }
    public string description { set; get; }
    public Guid project_id { set; get; }

}
