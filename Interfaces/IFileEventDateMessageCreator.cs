using System.Collections.Generic;

namespace ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces
{
    public interface IFileEventDateMessageCreator
    {
        /// <summary>
        /// Genera los mensajes del archivo y directorio proporcionado.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        List<string> CreateEventMessages(string path, string fileName);
    }
}
