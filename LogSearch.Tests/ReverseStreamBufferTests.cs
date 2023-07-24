using System.Text;

namespace LogSearch.Tests;

public class ReverseStreamBufferTests
{
    [Fact]
    public void ShouldRefreshBufferAndReturnChar()
    {
        var text = "012";
        var stream = new MemoryStream(Encoding.ASCII.GetBytes(text));
        var buffer = new ReverseStreamBuffer(stream, 2);

        var lastChar1 = buffer.GetLastChar();
        var lastChar2 = buffer.GetLastChar();
        var lastChar3 = buffer.GetLastChar();

        lastChar1.Should().Be('2');
        lastChar2.Should().Be('1');
        lastChar3.Should().Be('0');
    }
}