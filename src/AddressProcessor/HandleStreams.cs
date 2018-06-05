using System.IO;

namespace Csv.Tests
{
    class HandleStreams : IHandleStreams
    {
        public StreamReader StreamReader { get; set; }
        public StreamWriter StreamWriter { get; set; }
    }
}