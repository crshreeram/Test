using System;

namespace AddressProcessing.CSV
{
    public interface IFileReader : IDisposable
    {
        void OpenFile(string fileName);

        string ReadLine();

        bool IsEndOfFile();
    }
}