using ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces;

namespace ConsoleAppBuenasPracticasEjercicio1ConSOLID
{
    public class FileEventDataReader : IFileEventDataReader
    {
        /// <summary>
        /// Obtiene la información del archivo en el path dado y devuelve un arreglo con los valores de las filas dentro del archivo.
        /// </summary>
        /// <param name="path">La ruta o directorio del archivo.</param>
        /// <param name="FileName">El nombre del archivo.</param>
        /// <returns>Arreglo con los valores de las filas dentro del archivo.</returns>
        public string[] GetFileDataRows(string path, string FileName)
        {
            string dataFile = string.Format("{0}\\{1}", path, FileName);
            return System.IO.File.ReadAllLines(dataFile);
        }
    }
}
