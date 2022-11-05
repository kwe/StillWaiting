using StillWaiting.Models;

namespace StillWaiting;

public interface IMyDependency
{
    void WriteMessage(string message);
    // set a token
    void SetToken(string token);
    // get a token
    string GetToken();
}