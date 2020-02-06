namespace ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces
{
    public interface IFileEventDataReader
    {
        /// <summary>
        /// Obtiene la información del archivo en el path dado y devuelve un arreglo con los valores de las filas dentro del archivo.
        /// </summary>
        /// <param name="path">La ruta o directorio del archivo.</param>
        /// <param name="FileName">El nombre del archivo.</param>
        string[] GetFileDataRows(string path, string FileName);
    }
}
