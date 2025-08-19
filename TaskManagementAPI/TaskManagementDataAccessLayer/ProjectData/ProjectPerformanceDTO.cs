using TaskManagementDataAccessLayer.GoalData;

namespace TaskManagementDataAccessLayer.ProjectData
{
    public class ProjectPerformanceDTO
    {
        public Guid ProjectID { set; get; }
        public string Title { set; get; }
        public string Description { set; get; }
        public Priorities Priority { set; get; }
        public IEnumerable<GoalPerformanceDTO> GoalsPerformances { set; get; }
        public ProjectPerformanceDTO(Guid ProjectID,string Title,string Description,Priorities Priority,
            List<GoalPerformanceDTO>GoalsPerformances)
        {
            this.ProjectID = ProjectID;
            this.Title = Title;
            this.Description = Description;
            this.Priority = Priority;
            this.GoalsPerformances = GoalsPerformances;
        }
        public ProjectPerformanceDTO()
        {
            this.ProjectID = Guid.Empty;
            this.Title = "";
            this.Description = "";
            this.GoalsPerformances = null!;
        }
    }
}
