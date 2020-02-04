using System;

namespace AddressProcessing.CSV
{
    public interface IFileWriter : IDisposable
    {
        void CreateFile(string fileName);
        void WriteLine(string text);
    }
}