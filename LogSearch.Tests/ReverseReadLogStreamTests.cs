using System.Text;

namespace LogSearch.Tests;

public class ReverseReadLogStreamTests
{
    private readonly ReverseReadLogStream _reverseReadLogStream;
    private readonly Mock<IReverseStreamBuffer> _reverseStreamBuffer = new();

    public ReverseReadLogStreamTests()
    {
        _reverseReadLogStream = new ReverseReadLogStream(_reverseStreamBuffer.Object);
    }

    [Fact]
    public void ShouldReturnSingleChar()
    {
        _reverseStreamBuffer
            .SetupSequence(b => b.GetLastChar())
            .Returns('a');
        
        var line = _reverseReadLogStream.ReadLine();
        line.Should().Be("a");
    }
    
    [Fact]
    public void ShouldIgnoreLineCarriage()
    {
        _reverseStreamBuffer
            .SetupSequence(b => b.GetLastChar())
            .Returns('a')
            .Returns('\r');
        
        var line = _reverseReadLogStream.ReadLine();
        line.Should().Be("a");
    }
    
    [Fact]
    public void ShouldReturnLineOnLineBreak()
    {
        _reverseStreamBuffer
            .SetupSequence(b => b.GetLastChar())
            .Returns('a')
            .Returns('b')
            .Returns('c')
            .Returns('\n');
        
        var line = _reverseReadLogStream.ReadLine();
        line.Should().Be("cba");
    }
    
    [Fact]
    public void ShouldReturnLineWhenNoMoreChars()
    {
        _reverseStreamBuffer
            .SetupSequence(b => b.GetLastChar())
            .Returns('a')
            .Returns('b')
            .Returns('c')
            .Returns((char?)null);
        
        var line = _reverseReadLogStream.ReadLine();
        line.Should().Be("cba");
    }
    
    [Fact]
    public void ShouldReturnNull()
    {
        _reverseStreamBuffer
            .SetupSequence(b => b.GetLastChar())
            .Returns((char?)null);
        
        var line = _reverseReadLogStream.ReadLine();
        line.Should().BeNull();
    }

   
}