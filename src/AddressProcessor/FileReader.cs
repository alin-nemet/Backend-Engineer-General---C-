using Csv.Tests;

namespace AddressProcessing.CSV
{
    public class FileReader : IReadFile
    {
        public bool Read(out string column1, out string column2, IHandleStreams streamHandler)
        {

            char[] separator = { '\t' };

            var line = streamHandler.StreamReader.ReadLine();

            if (line == null)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            var columns = line.Split(separator);

            if (columns.Length == 0)
            {
                column1 = null;
                column2 = null;

                return false;
            }
            column1 = columns[0];
            column2 = columns[1];

            return true;
        }
    }
}