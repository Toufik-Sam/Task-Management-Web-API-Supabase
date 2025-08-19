
namespace TaskManagementDataAccessLayer.TaskData;

public class TaskGraph
{

    private Dictionary<TaskDTO, List<TaskDTO>> _adjacencyList;

    private Dictionary<int,TaskDTO> _tasksDictionary;

    public TaskGraph(List<TaskDTO> vertices)
    {

        _adjacencyList = new Dictionary<TaskDTO, List<TaskDTO>>();


        _tasksDictionary = new Dictionary<int, TaskDTO>();

        for (int i = 0; i < vertices.Count; i++)
        {
            _tasksDictionary[vertices[i].TaskID] = vertices[i];
            _adjacencyList[vertices[i]] = new List<TaskDTO>();  
        }
    }

    public bool CreateEdges()
    {
        foreach (var pair in _tasksDictionary)
        {
            if (_tasksDictionary.ContainsKey(pair.Value.ParentTaskID))
                _adjacencyList[_tasksDictionary[pair.Value.ParentTaskID]].Add(pair.Value);
            else if(pair.Value.ParentTaskID!=-1)
                return false;
        }
        return true;
    }
    private void _Merge()
    {
        foreach (var pair in _adjacencyList)
        {
            pair.Key.SubTasks = pair.Value;
        }
    }
    public TaskDTO GetTaskByID(int TaskID)
    {
        if (_tasksDictionary.ContainsKey(TaskID))
        {
            var task = _tasksDictionary[TaskID];
            task.SubTasks = _adjacencyList[task];
            return task;
        }
        return null;
    }
    public List<TaskDTO> GetTasks()
    {
        _Merge();
        var Res = new List<TaskDTO>();
        foreach (var pair in _adjacencyList)
            if (pair.Key.ParentTaskID == -1)
                Res.Add(pair.Key);

        return Res;
    }
    public int TotalNumberOfTasks()
    {
        return _adjacencyList.Count;
    }
    public int TotalNumberOfCompletedTasks()
    {
        int Res = 0;
        foreach (var pair in _adjacencyList)
            if (pair.Key.Status == Statuses.enCompleted)
                Res++;
        return Res;
    }
    public int TotalNumberOfOverdueTasks()
    {
        int Res = 0;
        foreach (var pair in _adjacencyList)
            if (pair.Key.Status == Statuses.enOverdue || pair.Key.ActualEndDate>pair.Key.EndDate)
                Res++;
        return Res;
    }
    public TimeSpan TotalTasksOverdueTime()
    {
        TimeSpan res = TimeSpan.Zero;
        foreach (var pair in _adjacencyList)
            if (pair.Key.Status == Statuses.enOverdue || pair.Key.ActualEndDate > pair.Key.EndDate)
                res +=pair.Key.ActualEndDate.Value- pair.Key.EndDate;
        return res;
    }
    public int TotalCompletionRate()
    {
        return ((TotalNumberOfCompletedTasks() * 100) / TotalNumberOfTasks());
    }
    public TaskPerformanceDTO GetTasksPerformance()
    {
        var tasksPerformance = new TaskPerformanceDTO(TotalNumberOfTasks(), 
                                                      TotalNumberOfCompletedTasks(), 
                                                      TotalCompletionRate(),
                                                      TotalNumberOfOverdueTasks(),
                                                      TotalTasksOverdueTime());
        return tasksPerformance;
    }
}
