namespace StillWaiting;

public class RepeatingService: BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(1));
    private readonly IMyDependency _myDependency;
    private IPostsClient _postsClient;
    
    public RepeatingService(IMyDependency myDependency, IPostsClient postsClient)
    {
        _myDependency = myDependency;
        _postsClient = postsClient;

        Console.WriteLine("RepeatingService constructor");
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            _myDependency.WriteMessage(DateTime.Now.ToString("O"));
            // grab some posts
            var posts = await _postsClient.GetPostsAsync();
        }
    }
}