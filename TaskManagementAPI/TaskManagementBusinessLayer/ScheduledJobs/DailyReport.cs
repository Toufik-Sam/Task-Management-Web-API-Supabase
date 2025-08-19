using Microsoft.Extensions.Hosting;


namespace TaskManagementBusinessLayer.ScheduledJobs;

public class DailyReport : IHostedService,IDisposable
{
    private Timer? _timer;
    private readonly Report _report;
    public DailyReport(Report report)
    {
        this._report = report;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(GenerateReport, state: null, TimeSpan.Zero, TimeSpan.FromSeconds(1));
        return Task.CompletedTask;
    }
    
    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void GenerateReport(object? state)
    {
        _report.ReportData.Add($"This is Your Daily Report !");
    }
}
