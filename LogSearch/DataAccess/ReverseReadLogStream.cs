using System.Text;

namespace LogSearch.DataAccess
{
    public class ReverseReadLogStream : ILogStream, IDisposable
    {
        private readonly IReverseStreamBuffer _buffer;

        public ReverseReadLogStream(string logLocation, string fileName) : this(
            new FileStream(Path.Combine(logLocation, fileName), FileMode.Open, FileAccess.Read))
        {
        }

        internal ReverseReadLogStream(Stream stream)
            : this(new ReverseStreamBuffer(stream))
        {
        }

        internal ReverseReadLogStream(IReverseStreamBuffer buffer)
        {
            _buffer = buffer;
        }

        public string? ReadLine()
        {
            var stringBuilder = new StringBuilder();

            char? readChar;
            do
            {
                readChar = _buffer.GetLastChar();
                if (readChar == '\r') // Ignore carriage return characters
                {
                    continue;
                }
                if (readChar == '\n') // Line break
                {
                    return stringBuilder.ToString();
                }

                if (readChar == null)
                {
                    if (stringBuilder.Length > 0) //no more to read
                    {
                        return stringBuilder.ToString();
                    }

                    return null;
                }

                stringBuilder.Insert(0, readChar);
            } while (readChar != null);

            return stringBuilder.ToString();
        }

        public void Dispose()
        {
            _buffer.Dispose();
        }
    }
}
