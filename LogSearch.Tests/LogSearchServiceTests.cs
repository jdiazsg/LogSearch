using LogSearch.Factories;
using LogSearch.Services;

namespace LogSearch.Tests;

public class LogSearchServiceTests
{
    private readonly Mock<ILogStreamFactory> _logStreamFactory = new();
    private readonly LogSearchService _logSearchService;
    private readonly Mock<ILogStream> _logStream = new Mock<ILogStream>();

    public LogSearchServiceTests()
    {
        _logSearchService = new LogSearchService(_logStreamFactory.Object);
        _logStreamFactory.Setup(f => f.GetLogStream("file")).Returns(_logStream.Object);
    }

    [Fact]
    public void ShouldReturnEmptyWhenFileEmpty()
    {
        _logStream.Setup(s => s.ReadLine()).Returns<string?>(null);

        var results = _logSearchService.Search("file", string.Empty, 10);

        results.Should().BeEmpty();
    }

    [Fact]
    public void ShouldNotReturnNonMatchingLineByKeyword()
    {
        string nonMatchingLine = "this line is not matching";
        
        _logStream.SetupSequence(s => s.ReadLine())
            .Returns(nonMatchingLine);
        
        var results = _logSearchService.Search("file", "keyword", 1);

        results.Should().BeEmpty();
    }

    [Fact]
    public void ShouldReturnMatchingLineByKeyword()
    {
        string matchingLine = "this line has keyword so matches";
        
        _logStream.SetupSequence(s => s.ReadLine())
            .Returns(matchingLine);
        
        var results = _logSearchService.Search("file", "keyword", 1);

        results.Single().Should().Be(matchingLine);
    }

    [Fact]
    public void ShouldReturnMultipleMatchingLines()
    {
        string matchingLine1 = "this line has keyword so matches";
        string matchingLine2 = "this line has keyword so matches too";
        string matchingLine3 = "this line has keyword so matches as well";

        _logStream.SetupSequence(s => s.ReadLine())
            .Returns(matchingLine1)
            .Returns(matchingLine2)
            .Returns(matchingLine3);

        var results = _logSearchService.Search("file", "keyword", 2);

        results.Should().Equal(matchingLine1, matchingLine2);
    }
}