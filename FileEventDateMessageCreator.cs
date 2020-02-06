using ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleAppBuenasPracticasEjercicio1ConSOLID
{
    public class FileEventDateMessageCreator : IFileEventDateMessageCreator
    {
        private readonly IClock _clock;
        private readonly IFileEventDateValidator _fileEventDateValidator;
        public FileEventDateMessageCreator(IClock clock, IFileEventDateValidator fileEventDateValidator)
        {
            _clock = clock;
            _fileEventDateValidator = fileEventDateValidator;
        }

        /// <summary>
        /// Genera los mensajes del archivo y directorio proporcionado.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>Lista con los mensajes</returns>
        public List<string> CreateEventMessages(string path, string fileName)
        {
            List<string> messages = new List<string>();            
            string errorFile = _fileEventDateValidator.ValidateFileExist(path, fileName);

            if (string.IsNullOrWhiteSpace(errorFile))
            {
                //Para inyección de dependencias por parámetro.
                IFileEventDataReader fileEventDataReader = new FileEventDataReader();
                messages = CreateFileMessages(path, fileName, fileEventDataReader);
            }
            else
            {
                throw new Exception(errorFile);
            }

            return messages;
        }

        private List<string> CreateFileMessages(string path, string fileName, IFileEventDataReader fileEventDataReader)
        {
            List<string> messages = new List<string>();
            //Leemos linea por línea para procesar la información
            string[] lines = fileEventDataReader.GetFileDataRows(path, fileName);
            DateTime dateTimeNow = _clock.GetTime();
            foreach (string line in lines)
            {
                messages.Add(GenerateEventMessage(line, dateTimeNow));
            }

            return messages;
        }

        private string GenerateEventMessage(string line, DateTime dateTimeNow)
        {
            string[] dataValues = line.Split(",");
            DateTime dateTimeEvent;
            if (dataValues.Length != 2)
            {
                return string.Format("Evento incorrecto para la linea con valor '{0}'.", line);
            }

            if (DateTime.TryParse(dataValues[1], out dateTimeEvent))
            {
                return string.Format("{0} {1}", dataValues[0], GenerateTimeElapsedMessage(dateTimeNow, dateTimeEvent));
            }
            else
            {
                return string.Format("Fecha incorrecta para la linea con valor '{0}'.", line);
            }
        }

        private string GenerateTimeElapsedMessage(DateTime dateTimeInit, DateTime dateTimeEnd)
        {
            string timeElapsed = "";
            TimeSpan timeSpan;
            if (dateTimeInit > dateTimeEnd)
            {
                timeSpan = dateTimeInit - dateTimeEnd;
                timeElapsed = GenerateMessage(timeSpan, "ocurrió hace ");
            }
            else if (dateTimeEnd > dateTimeInit)
            {
                timeSpan = dateTimeEnd - dateTimeInit;
                timeElapsed = GenerateMessage(timeSpan, "ocurrirá en ");
            }
            else
            {
                timeElapsed = " inicia en este mismo momento.";
            }

            return timeElapsed;
        }

        private string GenerateMessage(TimeSpan timeSpan, string word)
        {
            string message = string.Empty;

            if (timeSpan.TotalDays > 0 && timeSpan.TotalDays >= 30)
            {
                double months = Math.Truncate(timeSpan.TotalDays / 30);
                if (months > 1)
                {
                    message = string.Format("{0} {1} meses.", word, months);
                }
                else
                {
                    message = string.Format("{0} {1} mes.", word, months);
                }
            }
            else if (timeSpan.TotalDays >= 1)
            {
                double totalHoursDays = timeSpan.TotalDays * 24;
                double diference = timeSpan.TotalHours - totalHoursDays;
                if (diference > 0 && diference > 12)
                {
                    message = string.Format("{0} {1} días.", word, timeSpan.TotalDays + 1);
                }
                else
                {
                    message = string.Format("{0} {1} días.", word, timeSpan.TotalDays);
                }
            }
            else if (timeSpan.TotalHours > 12)
            {
                message = string.Format("{0} 1 día.", word);
            }
            else if (timeSpan.TotalHours >= 1)
            {
                double hours = Math.Truncate(timeSpan.TotalHours);
                if (hours > 1)
                {
                    message = string.Format("{0} {1} horas.", word, hours);
                }
                else
                {
                    message = string.Format("{0} {1} hora.", word, hours);
                }
            }
            else
            {
                message = string.Format("{0} {1} minutos.", word, timeSpan.Minutes);
            }

            return message;
        }
    }
}
