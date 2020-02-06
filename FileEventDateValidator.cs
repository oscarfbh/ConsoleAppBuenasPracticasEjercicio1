using ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces;
using System.IO;

namespace ConsoleAppBuenasPracticasEjercicio1ConSOLID
{
    internal class FileEventDateValidator : IFileEventDateValidator
    {
        public string ValidateFileExist(string path, string fileName)
        {
            string errorMessage = string.Empty;
            string fullPath = ObtainFullPath(path);
            string filePath;
            if (string.IsNullOrWhiteSpace(fullPath))
            {
                errorMessage = "La ruta proporcionada del archivo no existe.";
            }
            else if (!Directory.Exists(fullPath))
            {
                errorMessage = string.Format("El directorio '{0}' no existe.", fullPath);
            }
            else if (string.IsNullOrWhiteSpace(fileName.Trim()))
            {
                errorMessage = "El nombre del archivo es incorrecto o vacío.";
            }
            else
            {
                filePath = string.Format("{0}\\{1}", fullPath, fileName.Trim());

                if (!File.Exists(filePath))
                {
                    errorMessage = string.Format("El archivo '{0}' no existe en la ruta '{1}'.", fileName, fullPath);
                }
            }

            return errorMessage;
        }

        private static string ObtainFullPath(string path)
        {
            string fullPath = path;
            if (!string.IsNullOrWhiteSpace(path))
            {
                fullPath = Path.GetFullPath(path);
            }

            return fullPath;
        }
    }
}