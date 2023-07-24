namespace LogSearch.DataAccess;

public class ReverseStreamBuffer : IReverseStreamBuffer, IDisposable
{
    //private byte[] _buffer;
    private readonly Stream _stream;
    private readonly int _maxBufferSize = 3; //4096;
    private long _bytesRead;

    private Stack<byte> _bufferStack = new();

    public ReverseStreamBuffer(Stream stream, int maxBufferSize = 4096)
    {
        _stream = stream;
        _stream.Seek(0, SeekOrigin.End);
        _maxBufferSize = maxBufferSize;
    }

    public char? GetLastChar()
    {
        RefreshBufferIfNeeded();
        
        if (_bufferStack.Count == 0)
        {
            return null;
        }
        return (char)_bufferStack.Pop();
    }
        
    private void RefreshBufferIfNeeded()
    {
        //If no more to read
        if (_bytesRead >= _stream.Length)
        {
            return;
        }
        //no need to refresh if buffer still has data
        if (_bufferStack.Count > 0)
        {
            return;
        }
            
        long count = Math.Min(_maxBufferSize, GetBytesLeftToRead());

        _stream.Seek(-_bytesRead - count, SeekOrigin.End);
        var buffer = new byte[count];
        _bytesRead += _stream.Read(buffer, 0, (int)count);
        _bufferStack = new Stack<byte>(buffer);
    }

    private long GetBytesLeftToRead()
    {
        return _stream.Length - _bytesRead;
    }

    public void Dispose()
    {
        _stream.Dispose();
    }
}