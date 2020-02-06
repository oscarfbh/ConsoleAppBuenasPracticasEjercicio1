using ConsoleAppBuenasPracticasEjercicio1ConSOLID.Interfaces;
using System;

namespace ConsoleAppBuenasPracticasEjercicio1ConSOLID
{
    public class Clock : IClock
    {
        /// <summary>
        /// Obtiene la fecha y hora actuales.
        /// </summary>
        /// <returns>
        /// Fecha y hora actuales.
        /// </returns>
        public DateTime GetTime()
        {
            return DateTime.Now;
        }
    }
}
