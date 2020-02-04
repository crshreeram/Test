using System;
using System.IO;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter
    {
        private readonly IFileReader _fileReader;
        private readonly IFileWriter _fileWriter;
        private readonly IContactsMapper _contactsMapper;
        public const string DELIMITER = "\t";

        public CSVReaderWriter()
        {
            IFileStore fileStore = new FileStore();
            _fileReader = new FileReader(fileStore);
            _fileWriter = new FileWriter(fileStore);
            _contactsMapper = new ContactsMapper();
        }

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                _fileReader.OpenFile(fileName);
                return;
            }

            _fileWriter.CreateFile(fileName);
        }

        public void Write(params string[] columns)
        {
            columns = columns ?? throw new ArgumentException("The column text to write cannot be null");

            var textToWriteWithSeparator = String.Join(DELIMITER, columns);

            _fileWriter.WriteLine(textToWriteWithSeparator);
        }

        public bool Read(string column1, string column2)
        {
            return !_fileReader.IsEndOfFile();
        }

        public bool Read(out string contactName, out string contactDetails)
        {
            if (_fileReader.IsEndOfFile())
            {
                contactName = null;
                contactDetails = null;
                return false;
            }

            var contactData = _fileReader.ReadLine();

            _contactsMapper.MapContactsDetailsFromContacts(out contactName, out contactDetails, contactData, DELIMITER);

            return true;
        }

        private void WriteLine(string line)
        {
            _fileWriter.WriteLine(line);
        }

        private string ReadLine()
        {
            return _fileReader.ReadLine();
        }

        public void Close()
        {
            _fileReader?.Dispose();
            _fileWriter?.Dispose();
        }
    }
}
