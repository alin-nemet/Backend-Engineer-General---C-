using System.IO;

namespace Csv.Tests
{
    public interface IHandleStreams
    {
        StreamReader StreamReader { get; set; }
        StreamWriter StreamWriter { get; set; }
    }
}