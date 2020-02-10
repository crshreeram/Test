using System;
using AddressProcessing.Address.v2;
using AddressProcessing.CSV;

namespace AddressProcessing.Address
{
    public class AddressFileProcessor
    {
        private readonly IMailShot _mailShot;
        private IContactsMapper _contactsMapper;
        private const string DELIMITER = "\t";
        private readonly IFileReader _fileReader;

        public AddressFileProcessor(IMailShot mailShot,  IContactsMapper contactsMapper, IFileReader fileReader)
        {
            if (mailShot == null) throw new ArgumentNullException("mailShot");
            _mailShot = mailShot;
            _contactsMapper = contactsMapper;
            _fileReader = fileReader;
        }

        public void Process(string inputFile)
        {
            _fileReader.OpenFile(inputFile);

            using (_fileReader)
            {
                while (!_fileReader.IsEndOfFile())
                {
                    var fileDataLine = _fileReader.ReadLine();

                    _contactsMapper.MapContactsDetailsFromContacts(out var contactName, out var contactDetails, fileDataLine, DELIMITER);

                    var addressData = contactDetails.Split('|');

                    _mailShot.SendPostalMailShot(contactName, addressData[0], addressData[1], addressData[2],addressData[3],addressData[4]);
                }
            }

        }
    }
}
