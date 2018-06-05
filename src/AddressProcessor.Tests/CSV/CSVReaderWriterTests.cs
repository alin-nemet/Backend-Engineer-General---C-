using AddressProcessing.CSV;
using FakeItEasy;
using NUnit.Framework;

namespace Csv.Tests
{
    [TestFixture]
    public class CSVReaderWriterTests
    {
        [Test]
        public void OpensFilePassedInReadMode()
        {
            IOpenFile fileOpener = A.Fake<IOpenFile>();
            IReadFile fileReader = A.Fake<IReadFile>();
            IWriteFile fileWriter = A.Fake<IWriteFile>();

            var csvReaderWriter = new CSVReaderWriter(fileOpener, fileReader, fileWriter);
            var filePath = "filePath";
            var openMode = CSVReaderWriter.Mode.Read;
            csvReaderWriter.Open(filePath, openMode);
            A.CallTo(()=> fileOpener.Open(filePath,openMode)).MustHaveHappened(Repeated.Exactly.Once);
        }
    }
}
