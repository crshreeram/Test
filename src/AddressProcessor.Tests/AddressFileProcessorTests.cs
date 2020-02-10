using AddressProcessing.Address;
using AddressProcessing.Address.v2;
using AddressProcessing.CSV;
using NUnit.Framework;

namespace AddressProcessing.Tests
{
    [TestFixture]
    public class AddressFileProcessorTests
    {
        private FakeMailShotService _fakeMailShotService;
        private const string TestInputFile = @"test_data\contacts.csv";

        [SetUp]
        public void SetUp()
        {
            _fakeMailShotService = new FakeMailShotService();
        }

        [Test]
        public void Should_send_mail_using_mailshot_service()
        {
            var contactsMapper = new ContactsMapper();
            var fileReader = new FileReader(new FileStore());
            var processor = new AddressFileProcessor(_fakeMailShotService,contactsMapper,fileReader);
            processor.Process(TestInputFile);

            Assert.That(_fakeMailShotService.Counter, Is.EqualTo(229));
        }

        internal class FakeMailShotService : IMailShot
        {
            internal int Counter { get; private set; }

            public void SendPostalMailShot(string name, string address1, string town, string county, string country, string postCode)
            {
                Counter++;
            }

            public void SendEmailMailShot(string name, string email)
            {
                throw new System.NotImplementedException();
            }

            public void SendSmsMailShot(string name, string number)
            {
                throw new System.NotImplementedException();
            }
        }
    }
}