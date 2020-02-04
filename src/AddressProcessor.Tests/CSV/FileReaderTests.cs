using System;
using System.IO;
using System.Text;
using AddressProcessing.CSV;
using NUnit.Framework;
using Moq;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class FileReaderTests
    {
        [Test]
        public void Given_FileName_When_OpenFile_Called_GetsFileReadingStream()
        {
            // Arrange
            var fileName = "TestFileName";
            var fileStoreMoq = new Mock<IFileStore>();
            IFileReader fileReader = new FileReader(fileStoreMoq.Object);

            // Act
            fileReader.OpenFile(fileName);

            //Assert
            fileStoreMoq.Verify(x => x.GetReadingStream(fileName));
        }

        [Test]
        public void Given_Null_Filename_When_Reader_Object_Created_Returns_ArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new FileReader(null));
        }

        [Test]
        public void Given_ValidContactsData_When_ReadLine_Called_Returns_ContactData()
        {
            //Arrange
            string textInFile = "Test Contact\tTestAddress";
            var fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object); ;

            //Act

            fileReader.OpenFile("testFile");
            string result = fileReader.ReadLine();

            //Assert
            Assert.AreEqual(textInFile, result);

        }

        [Test]
        public void Given_ValidContactsData_With_MultiRow_When_ReadLine_Called_Returns_Multiple_ContactData()
        {
            //Arrange
            string textInFile = "Test Contact1\tTestAddress1\nTest Contact2\tTestAddress2";
            var fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object); ;

            //Act
            fileReader.OpenFile("testFile");
            string result1 = fileReader.ReadLine();
            string result2 = fileReader.ReadLine();
            //Assert
            Assert.AreEqual("Test Contact1\tTestAddress1", result1);
            Assert.AreEqual("Test Contact2\tTestAddress2", result2);

        }

        [Test]
        public void Given_File_WithContactData_When_IsEndOfFile_Called_Returns_False()
        {
            //Arrange
            string textInFile = "Test Name \tTest Address\nTest Name2\tTest Address2";
            Mock<IFileStore> fileStoreMoq = GenerateMockStreamWithText(textInFile);
            IFileReader fileReader = new FileReader(fileStoreMoq.Object);
            //Act

            fileReader.OpenFile("fileName");
            bool isEndOfFile = fileReader.IsEndOfFile();

            //Assert
            Assert.IsFalse(isEndOfFile);
        }


        private static Mock<IFileStore> GenerateMockStreamWithText(string textInFile)
        {
            var fileStoreMoq = new Mock<IFileStore>();
            UTF8Encoding encoding = new UTF8Encoding();
            MemoryStream memoryStream = new MemoryStream(encoding.GetBytes(textInFile));
            StreamReader reader = new StreamReader(memoryStream);
            fileStoreMoq.Setup(fs => fs.GetReadingStream(It.IsAny<string>())).Returns(() => reader);
            return fileStoreMoq;
        }
    }
}
