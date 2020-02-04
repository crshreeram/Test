using System;
using AddressProcessing.CSV;
using NUnit.Framework;

namespace Csv.Tests
{
    [TestFixture]
    public class CSVReaderWriterTests
    {
        [Test]
        public void Given_Null_Column_Parameters_When_Write_Called_Returns_Argument_Null_Exception()
        {
            // Arrange
            CSVReaderWriter csvReaderWriter = new CSVReaderWriter();

            // Act
            
            
            //Assert
            Assert.Throws<ArgumentException>(()=> csvReaderWriter.Write(null));
        }
    }
}
