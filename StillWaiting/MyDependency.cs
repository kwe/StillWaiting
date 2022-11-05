using StillWaiting.Models;

namespace StillWaiting;

public class MyDependency: IMyDependency
{
    // ReSharper disable once FieldCanBeMadeReadOnly.Local
    private Widget? _widget;
    
    public MyDependency()
    {
        _widget = new Widget();
    }
    public void WriteMessage(string message)
    {
        Console.WriteLine($"My dependency called {message}");
    }

    public void SetToken(string token)
    {
        if (_widget != null) _widget.Token = token;
    }

    public string GetToken()
    {
        return _widget?.Token!;
    }
}