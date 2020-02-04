using System;
using System.IO;

namespace AddressProcessing.CSV
{
    public class FileReader : IFileReader, IDisposable
    {
        private readonly IFileStore _fileStore;
        private TextReader _textReader;

        public FileReader(IFileStore fileStore)
        {
            _fileStore = fileStore ?? throw new ArgumentException("FileStore cannot be null");
        }

        public void OpenFile(string fileName)
        {
            _textReader = _fileStore.GetReadingStream(fileName);
        }

        public string ReadLine()
        {
            if (_textReader == null)
            {
                throw new Exception("FileStore is null, Call OpenFile method before reading.");
            }

            return _textReader.ReadLine();
        }
        public bool IsEndOfFile()
        {
            bool isThereCharacterToRead = _textReader.Peek() > 0;
            return !isThereCharacterToRead;
        }

        public void Dispose()
        {
            _textReader?.Dispose();
        }
    }
}
