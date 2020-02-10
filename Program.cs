using ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces;
using System;
using System.Collections.Generic;

namespace ConsoleAppBuenasPracticasEjercicio1ConSOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            IClock clock = new Clock();
            IFileEventDateValidator fileEventDateValidator = new FileEventDateValidator();
            IFileEventDataReader fileEventDataReader = new FileEventDataReader();
            string path = "C:\\CursoBuenasPracticasBOT\\ConsoleAppBuenasPracticasEjercicio1ConSOLID";
            string fileName = "Eventos.txt";
            IFileEventDateMessageCreator fileEventDateMessageCreator = new FileEventDateMessageCreator(clock, fileEventDateValidator, fileEventDataReader);
            try
            {
                List<string> eventMessages = fileEventDateMessageCreator.CreateEventMessages(path, fileName);
                foreach (string message in eventMessages)
                {
                    Console.WriteLine(message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
