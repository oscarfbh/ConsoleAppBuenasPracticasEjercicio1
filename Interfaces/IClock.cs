using System;

namespace ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces
{
    public interface IClock
    {
        /// <summary>
        /// Obtiene la fecha y hora actuales.
        /// </summary>
        /// <returns>Fecha y hora actuales.</returns>
        DateTime GetTime();
    }
}
