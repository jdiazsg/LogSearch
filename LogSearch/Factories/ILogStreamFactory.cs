using LogSearch.DataAccess;

namespace LogSearch.Factories;

public interface ILogStreamFactory
{
    public ILogStream GetLogStream(string fileName);
}