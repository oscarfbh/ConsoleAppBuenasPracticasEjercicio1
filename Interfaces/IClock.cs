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

        /// <summary>
        /// En base al tiempo inicial calcula el tiempo que ha pasado o el tiempo faltante del tiempo del evento.
        /// </summary>
        /// <param name="dateTimeInit">Tiempo inicial a considerar en la comparación.</param>
        /// <param name="dateTimeEvent">Tiempo del mensaje a calcular.</param>
        /// <returns>Cadena con el mensaje del tiempo que ha pasado entre los tiempos proporcionados.</returns>
        string GenerateTimeElapsedMessage(DateTime dateTimeInit, DateTime dateTimeEvent);
    }
}
