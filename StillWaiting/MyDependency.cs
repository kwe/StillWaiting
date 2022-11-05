using StillWaiting.Models;

namespace StillWaiting;

public class MyDependency: IMyDependency
{
    private Widget _widget;
    public void WriteMessage(string message)
    {
        Console.WriteLine($"My dependency called {message}");
    }

    public void SetToken(string token)
    {
        _widget = new Widget();
        _widget.Token = token;
    }

    public string GetToken()
    {
        return _widget.Token;
    }
}