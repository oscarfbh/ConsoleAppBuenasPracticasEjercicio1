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
            string path = "C:\\CursoBuenasPracticasBOT\\ConsoleAppBuenasPracticasEjercicio1ConSOLID";
            string fileName = "Eventos.txt";
            FileEventDateMessageCreator fileEventDateMessageCreator = new FileEventDateMessageCreator(clock, fileEventDateValidator);
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
