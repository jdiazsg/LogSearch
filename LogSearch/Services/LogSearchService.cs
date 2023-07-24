using LogSearch.Factories;

namespace LogSearch.Services;

public class LogSearchService: ILogSearchService
{
    private readonly ILogStreamFactory _logStreamFactory;

    public LogSearchService(ILogStreamFactory logStreamFactory)
    {
        _logStreamFactory = logStreamFactory;
    }

    public IEnumerable<string> Search(string fileName, string? keyword, int numberOfResults)
    {
        using var logStream = _logStreamFactory.GetLogStream(fileName);
        var lines = new List<string>();
        string? line;
        
        while((line = logStream.ReadLine()) != null && lines.Count < numberOfResults)
        {
            if (MatchesKeyword(keyword, line)) 
            {
                lines.Add(line);
            }
        }
        return lines;
    }

    private static bool MatchesKeyword(string? keyword, string line)
    {
        //Could be extracted for more advanced keyword matching
        return keyword == null || line.Contains(keyword, StringComparison.OrdinalIgnoreCase);
    }
}