using System.IO;

namespace AddressProcessing.CSV
{
    public interface IFileStore
    {
        TextReader GetReadingStream(string fileName);

        TextWriter GetWritingStream(string fileName);
    }
}