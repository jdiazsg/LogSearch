namespace LogSearch.DataAccess;

public class SimpleLogStream : ILogStream
{
    private readonly StreamReader _streamReader;

    public SimpleLogStream(string path)
    {
        _streamReader = new StreamReader(path);
    }

    public void Dispose()
    {
        _streamReader.Dispose();
    }

    public string? ReadLine()
    {
        return _streamReader.ReadLine();
    }
}