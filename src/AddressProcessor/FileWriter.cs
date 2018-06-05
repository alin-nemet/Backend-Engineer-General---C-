using System.Linq;
using System.Text;
using Csv.Tests;

namespace AddressProcessing.CSV
{
    public class FileWriter : IWriteFile
    {
        public void Write(string[] columns, IHandleStreams streamHandler)
        {
            var sb = new StringBuilder();
            columns.ToList().ForEach(c => sb.Append(c).Append('\t'));

            streamHandler.StreamWriter.WriteLine(sb.ToString());
        }
    }
}