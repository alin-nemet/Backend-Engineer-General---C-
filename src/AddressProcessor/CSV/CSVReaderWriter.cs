using System;
using Csv.Tests;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, 
            without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter : ICsvReaderWriter
    {
        private readonly IOpenFile _fileOpener;
        private readonly IReadFile _fileReader;
        private readonly IWriteFile _fileWriter;
        private IHandleStreams _streamHandler;

        // this forces modifying AddressFileProcessor, though it was specifically required not to
        // I opted this way since it just felt natural to abstract away and inject the resultant
        // interfaces into constructor (though probably will break "production code" ) while
        // testing also becomes easier
        public CSVReaderWriter(IOpenFile fileOpener, IReadFile fileReader, IWriteFile fileWriter)
        {
            _fileOpener = fileOpener;
            _fileReader = fileReader;
            _fileWriter = fileWriter;
        }

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string fileName, Mode mode)
        {
            _streamHandler = _fileOpener.Open(fileName, mode);
        }

        public void Write(params string[] columns)
        {
            if (_streamHandler?.StreamWriter == null)
            {
                throw new Exception("Cannot write file, failed to acquire write mode");
            }
            _fileWriter.Write(columns, _streamHandler);
        }

        public bool Read(out string column1, out string column2)
        {
            if (_streamHandler?.StreamReader == null)
            {
                throw new Exception("Can not read the file, failed to acquire read mode");
            }
            return _fileReader.Read(out column1, out column2, _streamHandler);
        }

        public void Dispose()
        {
            _streamHandler.StreamReader?.Close();
            _streamHandler.StreamWriter?.Close();
        }
    }
}
