namespace LogSearch.DataAccess;

public interface IReverseStreamBuffer: IDisposable
{
    char? GetLastChar();
}