using System.IO;

namespace AddressProcessing.CSV
{
    public class FileStore : IFileStore
    {
        public TextReader GetReadingStream(string fileName)
        {
            var fileStreamToRead = new FileStream(fileName,FileMode.Open); 
            return new StreamReader(fileStreamToRead);
        }

        public TextWriter GetWritingStream(string fileName)
        {
            var fileStreamToRead = new FileStream(fileName,FileMode.CreateNew);
            return new StreamWriter(fileStreamToRead);
        }
    }
}