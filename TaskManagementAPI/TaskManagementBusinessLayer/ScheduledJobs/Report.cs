using System.Collections.Concurrent;


namespace TaskManagementBusinessLayer.ScheduledJobs;

public class Report
{
   public ConcurrentBag<string> ReportData { get; set; } = new();
}
