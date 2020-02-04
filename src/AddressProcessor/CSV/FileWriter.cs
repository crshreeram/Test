using System;
using System.IO;

namespace AddressProcessing.CSV
{
    public class FileWriter : IFileWriter, IDisposable
    {
        private IFileStore _fileStore;
        private TextWriter _textWriter;

        public FileWriter(IFileStore fileStore)
        {
            _fileStore = fileStore ?? throw new ArgumentException("FileStore cannot be null");
        }

        public void CreateFile(string fileName)
        {
            _textWriter = _fileStore.GetWritingStream(fileName);
        }

        public void WriteLine(string text)
        {
            if (_textWriter == null)
            {
                throw new Exception("TextWriter is null, Call CreateFile before writing to file.");
            }

            _textWriter.WriteLine(text);
            _textWriter.Flush();

        }

        public void Dispose()
        {
            _textWriter?.Dispose();
        }
    }
}