using ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleAppBuenasPracticasEjercicio1ConSOLID
{
    public class FileEventDateMessageCreator : IFileEventDateMessageCreator
    {
        private readonly IClock _clock;
        private readonly IFileEventDateValidator _fileEventDateValidator;
        private readonly IFileEventDataReader _fileEventDataReader;
        public FileEventDateMessageCreator(IClock clock, IFileEventDateValidator fileEventDateValidator, IFileEventDataReader fileEventDataReader)
        {
            _clock = clock;
            _fileEventDateValidator = fileEventDateValidator;
            _fileEventDataReader = fileEventDataReader;
        }

        /// <summary>
        /// Genera los mensajes del archivo y directorio proporcionado.
        /// </summary>
        /// <param name="path">El directorio del archivo.</param>
        /// <param name="fileName">El nombre del archivo.</param>
        /// <returns>Lista con los mensajes generados.</returns>
        public List<string> CreateEventMessages(string path, string fileName)
        {
            List<string> messages = new List<string>();            
            string errorFile = _fileEventDateValidator.ValidateFileExist(path, fileName);

            if (string.IsNullOrWhiteSpace(errorFile))
            {
                messages = CreateFileMessages(path, fileName);
            }
            else
            {
                messages.Add(errorFile);
            }

            return messages;
        }

        private List<string> CreateFileMessages(string path, string fileName)
        {
            List<string> messages = new List<string>();
            //Leemos linea por línea para procesar la información
            string[] lines = _fileEventDataReader.GetFileDataRows(path, fileName);
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
                return string.Format("{0} {1}", dataValues[0], _clock.GenerateTimeElapsedMessage(dateTimeNow, dateTimeEvent));
            }
            else
            {
                return string.Format("Fecha incorrecta para la linea con valor '{0}'.", line);
            }
        }
    }
}
