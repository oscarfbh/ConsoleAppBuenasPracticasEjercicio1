using ConsoleAppBuenasPracticasEjercicio1ConSOLID;
using ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace UnitTestProject
{
    [TestClass]
    public class ClockTest
    {
        IClock _clock;
        DateTime _dateTimeEvent;
        private string[] _lines;
        private DateTime _dateTimeNow;

        [TestInitialize]
        public void OnSetup()
        {
            DateTime.TryParse("06/02/2020 09:00", out _dateTimeEvent);
            _dateTimeNow = _dateTimeEvent;
            _clock = new Clock();
            _lines = new string[1];

        }

        [TestMethod]
        public void GetTime_Method_Should_Return_DateTimeNow_Greater_Than_DateTime_Provided()
        {
            //Arrange
            string dateTimeInPastCharacter = "2005-05-05 22:12 PM";
            DateTime dateTime = DateTime.ParseExact(dateTimeInPastCharacter, "yyyy-MM-dd HH:mm tt", CultureInfo.InvariantCulture);

            //Act
            DateTime clockResult = _clock.GetTime();

            //Assert
            Assert.IsTrue(dateTime < clockResult);
        }

        [TestMethod()]
        public void CreateEventMessages_Method_Should_Return_Message_Same_Time_Correctly()
        {
            //Arrange
            string spectedMessage = " inicia en este mismo momento.";

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        public void CreateEventMessages_Method_Should_Return_Message_Future_Time_Correctly_For_Min()
        {
            //Arrange
            string timeWord = "ocurrirá en ";
            DateTime.TryParse("06/02/2020 08:59", out _dateTimeNow);
            int time = 1;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} minuto.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        public void CreateEventMessages_Method_Should_Return_Message_Past_Time_Correctly_For_Min()
        {
            //Arrange
            string timeWord = "ocurrió hace ";
            DateTime.TryParse("06/02/2020 09:01", out _dateTimeNow);
            int time = 1;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} minuto.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("06/02/2020 08:58", 2)]
        [DataRow("06/02/2020 08:30", 30)]
        [DataRow("06/02/2020 08:01", 59)]
        public void CreateEventMessages_Method_Should_Return_Message_Future_Time_Correctly_For_Minutes(string date, int minutesElapsed)
        {
            //Arrange
            string timeWord = "ocurrirá en ";
            DateTime.TryParse(date, out _dateTimeNow);
            int time = minutesElapsed;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} minutos.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("06/02/2020 09:59", 59)]
        [DataRow("06/02/2020 09:30", 30)]
        [DataRow("06/02/2020 09:02", 2)]
        public void CreateEventMessages_Method_Should_Return_Message_Past_Time_Correctly_For_Minutes(string date, int minutesElapsed)
        {
            //Arrange
            string timeWord = "ocurrió hace ";
            DateTime.TryParse(date, out _dateTimeNow);
            int time = minutesElapsed;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} minutos.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        public void CreateEventMessages_Method_Should_Return_Message_Future_Time_Correctly_For_Hour()
        {
            //Arrange
            string timeWord = "ocurrirá en ";
            DateTime.TryParse("06/02/2020 07:30", out _dateTimeNow);
            int time = 1;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} hora.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        public void CreateEventMessages_Method_Should_Return_Message_Past_Time_Correctly_For_Hour()
        {
            //Arrange
            string timeWord = "ocurrió hace ";
            DateTime.TryParse("06/02/2020 10:30", out _dateTimeNow);
            int time = 1;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} hora.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("06/02/2020 07:00", 2)]
        [DataRow("05/02/2020 21:30", 11)]
        public void CreateEventMessages_Method_Should_Return_Message_Future_Time_Correctly_For_Hours(string date, int hoursElapsed)
        {
            //Arrange
            string timeWord = "ocurrirá en ";
            DateTime.TryParse(date, out _dateTimeNow);
            int time = hoursElapsed;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} horas.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("06/02/2020 11:00", 2)]
        [DataRow("06/02/2020 20:59", 11)]
        public void CreateEventMessages_Method_Should_Return_Message_Past_Time_Correctly_For_Hours(string date, int hoursElapsed)
        {
            //Arrange
            string timeWord = "ocurrió hace ";
            DateTime.TryParse(date, out _dateTimeNow);
            int time = hoursElapsed;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} horas.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("05/02/2020 21:00")]
        [DataRow("05/02/2020 09:00")]
        [DataRow("04/02/2020 21:01")]
        public void CreateEventMessages_Method_Should_Return_Message_Future_Time_Correctly_For_Day(string date)
        {
            //Arrange
            string timeWord = "ocurrirá en ";
            DateTime.TryParse(date, out _dateTimeNow);
            int time = 1;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} día.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("07/02/2020 20:59")]
        [DataRow("07/02/2020 09:00")]
        [DataRow("06/02/2020 21:00")]
        public void CreateEventMessages_Method_Should_Return_Message_Past_Time_Correctly_For_Day(string date)
        {
            //Arrange
            string timeWord = "ocurrió hace ";
            DateTime.TryParse(date, out _dateTimeNow);
            int time = 1;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} día.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("04/02/2020 20:59", 2)]
        [DataRow("04/02/2020 09:00", 2)]
        [DataRow("03/02/2020 09:00", 3)]
        public void CreateEventMessages_Method_Should_Return_Message_Future_Time_Correctly_For_Days(string date, int days)
        {
            //Arrange
            string timeWord = "ocurrirá en ";

            DateTime.TryParse(date, out _dateTimeNow);
            int time = days;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} días.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("07/02/2020 21:00", 2)]
        [DataRow("08/02/2020 09:00", 2)]
        [DataRow("08/02/2020 21:00", 3)]
        [DataRow("09/02/2020 09:00", 3)]
        [DataRow("06/03/2020 20:59", 29)]
        public void CreateEventMessages_Method_Should_Return_Message_Past_Time_Correctly_For_Days(string date, int days)
        {
            //Arrange
            string timeWord = "ocurrió hace ";
            DateTime.TryParse(date, out _dateTimeNow);
            int time = days;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} días.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("07/01/2020 21:00")]
        [DataRow("07/01/2020 09:00")]
        [DataRow("06/01/2020 21:00")]
        [DataRow("06/01/2020 09:00")]
        [DataRow("08/12/2019 21:00")]
        [DataRow("08/12/2019 09:01")]
        public void CreateEventMessages_Method_Should_Return_Message_Future_Time_Correctly_For_Month(string date)
        {
            //Arrange
            string timeWord = "ocurrirá en ";
            DateTime.TryParse(date, out _dateTimeNow);
            int time = 1;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} mes.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }

        [TestMethod()]
        [DataRow("06/03/2020 21:00")]
        [DataRow("07/03/2020 09:00")]
        [DataRow("06/04/2020 08:59")]
        public void CreateEventMessages_Method_Should_Return_Message_Past_Time_Correctly_For_Month(string date)
        {
            //Arrange
            string timeWord = "ocurrió hace ";
            DateTime.TryParse(date, out _dateTimeNow);
            int time = 1;
            _lines[0] = string.Format("{0}", _dateTimeEvent);
            string spectedMessage = string.Format("{0} {1} mes.", timeWord, time);

            //Act 
            string result = _clock.GenerateTimeElapsedMessage(_dateTimeNow, _dateTimeEvent);

            //Assert
            Assert.AreEqual(spectedMessage, result);
        }
    }
}
