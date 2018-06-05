using AddressProcessing.CSV;

namespace Csv.Tests
{
    public interface IOpenFile
    {
        IHandleStreams Open(string filePath, CSVReaderWriter.Mode mode);
    }
}