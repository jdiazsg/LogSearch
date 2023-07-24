namespace LogSearch.Services;

public interface ILogSearchService
{
    IEnumerable<string> Search(string fileName, string? keyword, int numberOfResults);
}