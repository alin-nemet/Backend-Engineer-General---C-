using System.IO;
using Csv.Tests;

namespace AddressProcessing.CSV
{
    public interface IWriteFile
    {
        void Write(string[] columns, IHandleStreams streamHandler);
    }
}