using Csv.Tests;

namespace AddressProcessing.CSV
{
    public interface IReadFile
    {
        bool Read(out string column1, out string column2, IHandleStreams streamHandler);
    }
}