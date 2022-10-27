namespace StillWaiting;

public class RepeatingService: BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(1));
    private readonly IMyDependency _myDependency;

    public RepeatingService(IMyDependency myDependency)
    {
        _myDependency = myDependency;
        Console.WriteLine("RepeatingService constructor");
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            _myDependency.WriteMessage(DateTime.Now.ToString("O"));
        }
    }
}