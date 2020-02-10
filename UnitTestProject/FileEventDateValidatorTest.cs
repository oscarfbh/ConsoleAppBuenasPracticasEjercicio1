﻿using ConsoleAppBuenasPracticasEjercicio1ConSOLID;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;

namespace UnitTestProject
{
    [TestClass()]
    public class FileEventDateValidatorTest
    {
        private Mock<FileEventDateValidator> _fileEventDateValidatorMock;

        string _path, _fileName;
        [TestInitialize]
        public void OnSetup()
        {
            _path = "F:\\somedirectory";
            _fileName = "somefile.txt";
            _fileEventDateValidatorMock = new Mock<FileEventDateValidator>() { CallBase = true };
            _fileEventDateValidatorMock.Protected().Setup<bool>("CallDirectoryExistMethod", ItExpr.IsAny<string>()).Returns(true);
            _fileEventDateValidatorMock.Protected().Setup<bool>("CallFileExistMethod", ItExpr.IsAny<string>()).Returns(true);
        }
        [TestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void ValidateFileExist_Method_Should_Return_Message_For_Invalid_Path(string path)
        {
            //Arrange
            //Act            
            string result = _fileEventDateValidatorMock.Object.ValidateFileExist(path, _fileName);

            //Assert
            Assert.AreEqual("La ruta proporcionada del archivo no existe.", result);
        }

        [TestMethod()]
        public void ValidateFileExist_Method_Should_Return_Message_For_Path_Does_Not_Exist()
        {
            //Arrange
            _fileEventDateValidatorMock.Protected().Setup<bool>("CallDirectoryExistMethod", ItExpr.IsAny<string>()).Returns(false);
            //Act            
            string result = _fileEventDateValidatorMock.Object.ValidateFileExist(_path, _fileName);

            //Assert
            Assert.AreEqual(string.Format("El directorio '{0}' no existe.", _path), result);
        }

        [TestMethod()]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("   ")]
        public void ValidateFileExist_Method_Should_Return_Message_For_Invalid_FileName(string fileName)
        {
            //Arrange
            //Act            
            string result = _fileEventDateValidatorMock.Object.ValidateFileExist(_path, fileName);

            //Assert
            Assert.AreEqual("El nombre del archivo es incorrecto o vacío.", result);
        }

        [TestMethod()]
        public void ValidateFileExist_Method_Should_Return_Message_For_FileName_Does_Not_Exist()
        {
            //Arrange
            _fileEventDateValidatorMock.Protected().Setup<bool>("CallFileExistMethod", ItExpr.IsAny<string>()).Returns(false);
            //Act            
            string result = _fileEventDateValidatorMock.Object.ValidateFileExist(_path, _fileName);

            //Assert
            Assert.AreEqual(string.Format("El archivo '{0}' no existe en la ruta '{1}'.", _fileName, _path), result);
        }

        [TestMethod()]
        public void ValidateFileExist_Method_Should_Return_Empty_String_For_Validation_Correctly()
        {
            //Arrange
            //Act            
            string result = _fileEventDateValidatorMock.Object.ValidateFileExist(_path, _fileName);

            //Assert
            Assert.AreEqual(string.Empty, result);
        }
    }
}

