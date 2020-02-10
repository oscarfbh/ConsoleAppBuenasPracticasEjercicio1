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

        /// <summary>
        /// En base al tiempo inicial calcula el tiempo que ha pasado o el tiempo faltante del tiempo del evento.
        /// </summary>
        /// <param name="dateTimeInit">Tiempo inicial a considerar en la comparación.</param>
        /// <param name="dateTimeEvent">Tiempo del mensaje a calcular.</param>
        /// <returns>
        /// Cadena con el mensaje del tiempo que ha pasado entre los tiempos proporcionados.
        /// </returns>
        public string GenerateTimeElapsedMessage(DateTime dateTimeInit, DateTime dateTimeEvent)
        {
            string timeElapsed = "";
            TimeSpan timeSpan;
            //Por alguna razón al parsear una cadena que da la misma fecha considera que es diferente por eso se valida con las cadenas.
            if (DateTime.Compare(dateTimeInit, dateTimeEvent) > 0)
            {
                timeSpan = dateTimeInit - dateTimeEvent;
                timeElapsed = CalculateTimeMessage(timeSpan, "ocurrió hace ");
            }
            else if (DateTime.Compare(dateTimeInit, dateTimeEvent) < 0)
            {
                timeSpan = dateTimeEvent - dateTimeInit;
                if (timeSpan.Days.Equals(0) && timeSpan.Hours.Equals(0) && timeSpan.Minutes.Equals(0))
                {
                    timeElapsed = " inicia en este mismo momento.";
                }
                else
                {
                    timeElapsed = CalculateTimeMessage(timeSpan, "ocurrirá en ");
                }
            }
            else
            {
                timeElapsed = " inicia en este mismo momento.";
            }

            return timeElapsed;
        }

        private string CalculateTimeMessage(TimeSpan timeSpan, string word)
        {
            string message = string.Empty;
            double months = Math.Truncate(timeSpan.TotalDays / 30);
            double diference, totalHoursDays;

            if (timeSpan.TotalDays > 0 && timeSpan.TotalDays >= 30)
            {
                double totalDaysPerMonth = months * 30;
                double diferenceDays = timeSpan.TotalDays - totalDaysPerMonth;
                totalHoursDays = diferenceDays * 24;
                diference = timeSpan.TotalHours - totalHoursDays;
                if ((diferenceDays == 28 && diference >= 12))
                {
                    if (months == 0) { 
                        message = string.Format("{0} 1 mes.", word); 
                    }
                    else
                    {
                        message = string.Format("{0} {1} meses.", word, months + 1);
                    }
                }
                else if (months > 1)
                {
                    message = string.Format("{0} {1} meses.", word, months);
                }
                else
                {
                    message = string.Format("{0} 1 mes.", word);
                }
            }
            else if (timeSpan.TotalDays >= 29) {
                //checar si han pasado 12 hrs
                totalHoursDays = 29 * 24;
                diference = timeSpan.TotalHours - totalHoursDays;
                if (diference > 0 && diference >= 12)
                {
                    message = string.Format("{0} 1 mes.", word);
                }
                else
                {
                    message = string.Format("{0} {1} días.", word, timeSpan.Days);
                }

            }
            else if (timeSpan.Days >= 1)
            {
                totalHoursDays = timeSpan.Days * 24;
                diference = timeSpan.TotalHours - totalHoursDays;
                if (diference > 0 && diference >= 12)
                {
                    message = string.Format("{0} {1} días.", word, timeSpan.Days + 1);
                }
                else if (timeSpan.Days == 1) {
                    message = string.Format("{0} 1 día.", word);
                }
                else
                {
                    message = string.Format("{0} {1} días.", word, timeSpan.Days);
                }
            }
            else if (timeSpan.TotalHours >= 12)
            {
                message = string.Format("{0} 1 día.", word);
            }
            else if (timeSpan.TotalHours >= 1)
            {
                double hours = Math.Truncate(timeSpan.TotalHours);
                if (hours > 1)
                {
                    message = string.Format("{0} {1} horas.", word, hours);
                }
                else
                {
                    message = string.Format("{0} {1} hora.", word, hours);
                }
            }
            else
            {
                if (timeSpan.Minutes > 1)
                {
                    message = string.Format("{0} {1} minutos.", word, timeSpan.Minutes);
                }
                else
                {
                    message = string.Format("{0} {1} minuto.", word, timeSpan.Minutes);
                }
            }

            return message;
        }
    }
}
