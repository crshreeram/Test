using System;
using System.IO;
using System.Text;
using AddressProcessing.CSV;
using NUnit.Framework;
using Moq;

namespace AddressProcessing.Tests.CSV
{
    [TestFixture]
    public class FileWriterTests
    {
        MemoryStream _writeMemoryStream = new MemoryStream();

        [SetUp]
        public void SetUp()
        {
            _writeMemoryStream = new MemoryStream();
        }

        [TearDown]
        public void TearDown()
        {
            _writeMemoryStream = null;
        }


        [Test]
        public void Given_FileName_When_CreateFile_Called_GetsFileWritingStream()
        {
            //Arrange
            var fileName = "testFile";
            var fileStoreMoq = new Mock<IFileStore>();
            IFileWriter fileWriter = new FileWriter(fileStoreMoq.Object);

            //Act
            fileWriter.CreateFile(fileName);

            //Assert
            fileStoreMoq.Verify(x => x.GetWritingStream(fileName));
        }

        [Test]
        public void Given_Null_Filename_When_FileWriter_Created_Returns_ArgumentException()
        {
            //Assert
            Assert.Throws<ArgumentException>(() => new FileWriter(null));
        }

        [Test]
        public void Given_TestData_When_WriteLine_Called_Then_Data_WrittenToStream()
        {
            //Arrange
            string textForFile = "Test Name\tTest Address";
            var fileStoreMoq = GenerateMockWriteStream();
            IFileWriter fileWriter = new FileWriter(fileStoreMoq.Object);

            //Act
            fileWriter.CreateFile("fileName");
            fileWriter.WriteLine(textForFile);
            string fileData = GetTextInFile();

            //Assert
            Assert.AreEqual(textForFile + Environment.NewLine, fileData);
        }

        private string GetTextInFile()
        {
            var fileData = Encoding.UTF8.GetString(_writeMemoryStream.ToArray());
            return fileData;
        }

        [Test]
        public void Given_textFile_When_WriteLine_Called_WithoutCallingCreateFile_Then_ThrowsException()
        {
            //Arrange
            string textForFile = "Test Name\tTest Address";
            var fileStoreMoq = GenerateMockWriteStream();
            IFileWriter fileWriter = new FileWriter(fileStoreMoq.Object);


            //Assert
            Assert.Throws<Exception>(() => fileWriter.WriteLine(textForFile));
        }
        private Mock<IFileStore> GenerateMockWriteStream()
        {
            var fileStoreMoq = new Mock<IFileStore>();
            UTF8Encoding encoding = new UTF8Encoding();
            _writeMemoryStream = new MemoryStream();
            StreamWriter writer = new StreamWriter(_writeMemoryStream);
            fileStoreMoq.Setup(fs => fs.GetWritingStream(It.IsAny<string>())).Returns(() => writer);
            return fileStoreMoq;
        }
    }
}