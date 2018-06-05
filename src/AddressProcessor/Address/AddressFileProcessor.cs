using System;
using AddressProcessing.Address.v1;
using AddressProcessing.CSV;

namespace AddressProcessing.Address
{
    public class AddressFileProcessor
    {
        private readonly IMailShot _mailShot;
        private readonly ICsvReaderWriter _csvReaderWriter;

        public AddressFileProcessor(IMailShot mailShot, ICsvReaderWriter csvReaderWriter)
        {
            if (mailShot == null) throw new ArgumentNullException("mailShot");
            _mailShot = mailShot;
            _csvReaderWriter = csvReaderWriter;
        }

        public void Process(string inputFile)
        {
            using (_csvReaderWriter)
            {
                _csvReaderWriter.Open(inputFile, CSVReaderWriter.Mode.Read);

                string column1, column2;

                while (_csvReaderWriter.Read(out column1, out column2))
                {
                    _mailShot.SendMailShot(column1, column2);
                }
            }
        }
    }
}
