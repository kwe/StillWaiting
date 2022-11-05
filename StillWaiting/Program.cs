using Polly;
using Polly.Contrib.WaitAndRetry;
using StillWaiting;
using StillWaiting.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<RepeatingService>();

builder.Services.AddSingleton<IPostsClient, PostsClient>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient(PostsClient.ClientName,
        client =>
        {
          client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/");
        })
    .AddTransientHttpErrorPolicy(policyBuilder =>
        policyBuilder.WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5)));

builder.Services.AddSingleton<IMyDependency, MyDependency>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");
app.MapGet("/health", () => "Healthy");
app.MapGet("/posts", async (IPostsClient postsClient, IMyDependency myDependency) =>
{
    var posts = await postsClient.GetPostsAsync();
    Console.WriteLine($"In posts endpoint - {myDependency.GetToken()}");
    return posts is not null ? Results.Ok(posts) : Results.NotFound();
});

app.Run();