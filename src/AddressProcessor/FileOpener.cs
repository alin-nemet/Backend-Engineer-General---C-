using System;
using System.IO;
using AddressProcessing.CSV;

namespace Csv.Tests
{
    public class FileOpener : IOpenFile
    {
        public IHandleStreams Open(string fileName, CSVReaderWriter.Mode mode)
        {
            if (mode == CSVReaderWriter.Mode.Read)
            {
                return new HandleStreams {StreamReader = File.OpenText(fileName)};
            }
            if (mode == CSVReaderWriter.Mode.Write)
            {
                FileInfo fileInfo = new FileInfo(fileName);
                return new HandleStreams {StreamWriter = fileInfo.CreateText()};
            }

            throw new Exception("No read mode for file " + fileName);
        }
    }
}