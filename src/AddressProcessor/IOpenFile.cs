using AddressProcessing.CSV;

namespace Csv.Tests
{
    public interface IOpenFile
    {
        void Open(string filePath, CSVReaderWriter.Mode mode);
    }
}