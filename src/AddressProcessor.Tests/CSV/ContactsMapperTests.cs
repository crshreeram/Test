using System;
using AddressProcessing.CSV;
using NUnit.Framework;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    class ContactsMapperTests
    {
        [Test]
        public void Given_Valid_ContactsData_FromCSVFile_When_Mapper_Called_Returns_Mapped_ContactName_And_Details()
        {
            // Arrange
            IContactsMapper mapper = new ContactsMapper();
            string contactName;
            string contactDetails;
            string contactData = "Test Name\tTestAddress";

            // Act
            mapper.MapContactsDetailsFromContacts(out contactName, out contactDetails, contactData, "\t");
            
            //Assert
            Assert.AreEqual("Test Name",contactName);
            Assert.AreEqual("TestAddress", contactDetails);
        }

        [Test]
        public void Given_Only_ContactsName_FromCSVFile_When_Mapper_Called_Returns_Mapped_ContactName_Only()
        {
            // Arrange
            IContactsMapper mapper = new ContactsMapper();
            string contactName;
            string contactDetails;
            string contactData = "Test Name";

            // Act
            mapper.MapContactsDetailsFromContacts(out contactName, out contactDetails, contactData, "\t");
            
            //Assert
            Assert.AreEqual("Test Name",contactName);
        }
    }
}
