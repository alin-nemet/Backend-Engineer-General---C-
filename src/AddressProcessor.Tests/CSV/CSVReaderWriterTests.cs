using System;
using System.IO;
using System.Text;
using AddressProcessing.CSV;
using FakeItEasy;
using NUnit.Framework;

namespace Csv.Tests
{
    [TestFixture]
    public class CsvReaderWriterTests
    {
        private IOpenFile _fileOpener;
        private IReadFile _fileReader;
        private IWriteFile _fileWriter;
        private CSVReaderWriter _csvReaderWriter;

        [SetUp]
        public void Setup()
        {
            _fileOpener = A.Fake<IOpenFile>();
            _fileReader = A.Fake<IReadFile>();
            _fileWriter = A.Fake<IWriteFile>();

            _csvReaderWriter = new CSVReaderWriter(_fileOpener, _fileReader, _fileWriter);
        }

        [Test]
        public void CallsFileOpenerInReadMode()
        {
            var filePath = "filePath";
            var openMode = CSVReaderWriter.Mode.Read;
            _csvReaderWriter.Open(filePath, openMode);
            A.CallTo(() => _fileOpener.Open(filePath, openMode)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void CallsFileOpenerInWriteMode()
        {
            var filePath = "filePath";
            var openMode = CSVReaderWriter.Mode.Write;
            _csvReaderWriter.Open(filePath, openMode);
            A.CallTo(() => _fileOpener.Open(filePath, openMode)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [Test]
        public void FileWriterIsCalledWhenCallingWrite()
        {
            var columns = new[] { "one", "two" };
            var handleStreams = A.Fake<IHandleStreams>();
            handleStreams.StreamWriter= new StreamWriter(new MemoryStream(Encoding.Default.GetBytes("hi")));
            var mode = CSVReaderWriter.Mode.Write;

            A.CallTo(() => _fileOpener.Open("someFile", mode)).Returns(handleStreams);
            _csvReaderWriter.Open("someFile", mode);
            _csvReaderWriter.Write(columns);
            A.CallTo(() => _fileWriter.Write(columns, handleStreams)).MustHaveHappened();
        }

        [Test]
        public void FileReaderIsCallledWhenCallingRead()
        {
            var handleStreams = A.Fake<IHandleStreams>();
            handleStreams.StreamReader = new StreamReader(new MemoryStream(Encoding.Default.GetBytes("hello")));
            var mode = CSVReaderWriter.Mode.Read;
            A.CallTo(() => _fileOpener.Open("someFile", mode)).Returns(handleStreams);
            _csvReaderWriter.Open("someFile", mode);
            string column1, column2;
            _csvReaderWriter.Read(out column1, out column2);
            A.CallTo(() => _fileReader.Read(out column1, out column2, handleStreams))
                .MustHaveHappened();
        }

        [Test]
        public void ThrowsExceptionWhenReadingFileOpenInWrongMode()
        {
            var mode = CSVReaderWriter.Mode.Write;
            var handleStreams = A.Fake<IHandleStreams>();
            A.CallTo(() => _fileOpener.Open("someFile", mode)).Returns(handleStreams);
            _csvReaderWriter.Open("someFile", mode);
            string column1, column2;
            Assert.Throws<Exception>(() => _csvReaderWriter.Read(out column1, out column2));
            A.CallTo(() => _fileReader.Read(out column1, out column2, handleStreams))
                .MustNotHaveHappened();
        }

        [Test]
        public void ThrowsExceptionWhenWritingFileOpenInWrongMode()
        {
            var mode = CSVReaderWriter.Mode.Read;
            var handleStreams = A.Fake<IHandleStreams>();
            A.CallTo(() => _fileOpener.Open("someFile", mode)).Returns(handleStreams);
            _csvReaderWriter.Open("someFile", mode);
            string[] columns = { "one", "two" };
            Assert.Throws<Exception>(() => _csvReaderWriter.Write(columns));
            A.CallTo(() => _fileWriter.Write(columns, handleStreams))
                .MustNotHaveHappened();
        }

        [Test]
        public void ThrowsExceptionWhenWritingFileWithoutOpeningFirst()
        {
            var handleStreams = A.Fake<IHandleStreams>();
            string[] columns = { "one", "two" };
            Assert.Throws<Exception>(() => _csvReaderWriter.Write(columns));
            A.CallTo(() => _fileWriter.Write(columns, handleStreams))
                .MustNotHaveHappened();
        }

        [Test]
        public void ThrowsExceptionWhenReadingFileWithoutOpeningFirst()
        {
            var handleStreams = A.Fake<IHandleStreams>();
            string[] columns = { "one", "two" };
            Assert.Throws<Exception>(() => _csvReaderWriter.Write(columns));
            A.CallTo(() => _fileWriter.Write(columns, handleStreams))
                .MustNotHaveHappened();
        }
    }
}
