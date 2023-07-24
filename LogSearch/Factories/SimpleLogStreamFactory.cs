using LogSearch.DataAccess;

namespace LogSearch.Factories;

public class SimpleLogStreamFactory : ILogStreamFactory
{
    private readonly string _logLocation;

    public SimpleLogStreamFactory(string logLocation)
    {
        _logLocation = logLocation;
    }
    public ILogStream GetLogStream(string fileName)
    {
        return new ReverseReadLogStream(_logLocation, fileName);
        //return new SimpleLogStream(Path.Combine(_logLocation, fileName));
    }
}