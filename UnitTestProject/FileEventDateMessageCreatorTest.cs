using ConsoleAppBuenasPracticasEjercicio1ConSOLID;
using ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace UnitTestProject
{
    [TestClass()]
    public class FileEventDateMessageCreatorTest
    {
        private FileEventDateMessageCreator _fileEventDateMessageCreator;
        private Mock<IClock> _clockMock;
        private Mock<IFileEventDateValidator> _fileValidatorMock;
        private Mock<IFileEventDataReader> _fileReaderMock;
        private string _path;
        private string _fileName;
        private string[] _lines;
        private DateTime _dateTimeNow;
        [TestInitialize]
        public void OnSetup()
        {
            _lines = new string[1];
            _path = "F:\\somedirectory";
            _fileName = "somefile.txt";
            _dateTimeNow = DateTime.Now;
            _clockMock = new Mock<IClock>();
            _clockMock.Setup(m => m.GetTime()).Returns(_dateTimeNow);            
            _fileValidatorMock = new Mock<IFileEventDateValidator>();
            _fileValidatorMock.Setup(m => m.ValidateFileExist(It.IsAny<string>(), It.IsAny<string>())).Returns("");
            _fileReaderMock = new Mock<IFileEventDataReader>();
            _fileReaderMock.Setup(m => m.GetFileDataRows(It.IsAny<string>(), It.IsAny<string>())).Returns(_lines);
            _fileEventDateMessageCreator = new FileEventDateMessageCreator(_clockMock.Object,
                _fileValidatorMock.Object,
                _fileReaderMock.Object);
        }

        [TestMethod]
        public void CreateEventMessages_Method_Should_Return_Error_Message_For_File_Error()
        {
            //Arrange
            string errorMessage = "El archivo tuvo un problema al validarse";
            _fileValidatorMock.Setup(m => m.ValidateFileExist(It.IsAny<string>(), It.IsAny<string>())).Returns(errorMessage);

            //Act 
            List<string> result = _fileEventDateMessageCreator.CreateEventMessages(_path, _fileName);

            //Assert
            Assert.AreEqual(errorMessage, result[0]);
        }

        [TestMethod()]
        [DataRow("")]
        [DataRow("Event")]
        [DataRow("Event, 03/15/2/2005, Another thing")]
        public void CreateEventMessages_Method_Should_Return_Event_Incorrect_For_Line_Split_Diferent_Two_Columns(string dataLine)
        {
            //Arrange
            _lines[0] = dataLine;

            //Act 
            List<string> result = _fileEventDateMessageCreator.CreateEventMessages(_path, _fileName);

            //Assert
            Assert.AreEqual(string.Format("Evento incorrecto para la linea con valor '{0}'.", dataLine), result[0]);
        }

        [TestMethod()]
        [DataRow(",")]
        [DataRow("Event,")]
        [DataRow("Event, 2015/15/2/2005")]
        [DataRow("Event, 2015/18/35")]
        public void CreateEventMessages_Method_Should_Return_Event_Incorrect_For_Line_With_Incorrect_Datetime(string dataLine)
        {
            //Arrange
            _lines[0] = dataLine;

            //Act 
            List<string> result = _fileEventDateMessageCreator.CreateEventMessages(_path, _fileName);

            //Assert
            Assert.AreEqual(string.Format("Fecha incorrecta para la linea con valor '{0}'.", dataLine), result[0]);
        }

        [TestMethod()]
        [DataRow("Event,2005-05-05 10:12 PM")]
        [DataRow("Event, 2005-05-05 10:12 AM")]
        [DataRow("Event, 2005-05-05 22:12")]
        public void CreateEventMessages_Method_Should_Return_Message_Correctly_For_Correct_Value(string dataLine)
        {
            //Arrange
            _lines[0] = dataLine;
            string message = "Correct Message.";
            _clockMock.Setup(m => m.GenerateTimeElapsedMessage(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(message);

            //Act 
            List<string> result = _fileEventDateMessageCreator.CreateEventMessages(_path, _fileName);

            //Assert
            Assert.AreEqual(string.Format("Event {0}", message), result[0]);
            _clockMock.Verify(m => m.GenerateTimeElapsedMessage(It.IsAny<DateTime>(), It.IsAny<DateTime>()), Times.Once);
        }
    }
}