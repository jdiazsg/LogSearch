namespace LogSearch.DataAccess;

public interface ILogStream:IDisposable
{
    string? ReadLine();
}