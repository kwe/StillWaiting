namespace StillWaiting;

public class MyDependency: IMyDependency
{
    public void WriteMessage(string message)
    {
        Console.WriteLine($"My dependency called {message}");
    }
}