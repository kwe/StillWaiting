using StillWaiting.Clients;

namespace StillWaiting;

public class RepeatingService: BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromMinutes(1));
    private readonly IMyDependency _myDependency;
    private readonly IPostsClient _postsClient;

    public RepeatingService(IMyDependency myDependency, IPostsClient postsClient)
    {
        _myDependency = myDependency;
        _postsClient = postsClient;
        _myDependency.SetToken("hello world");
        Console.WriteLine($"RepeatingService constructor, token: {_myDependency.GetToken()}");
    }
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
        {
            _myDependency.WriteMessage(DateTime.Now.ToString("O"));
            var posts = await _postsClient.GetPostsAsync();
            if (posts != null) _myDependency.SetToken(posts.First().Title!);
            Console.WriteLine($"Called get Posts, token is {_myDependency.GetToken()}");
        }
    }
}