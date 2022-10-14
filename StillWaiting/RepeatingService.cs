namespace StillWaiting;

public class RepeatingService: BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(4));
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            Console.WriteLine(DateTime.Now.ToString("O"));
            // relogin!
        }
    }
}