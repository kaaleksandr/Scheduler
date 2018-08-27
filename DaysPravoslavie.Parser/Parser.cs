using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaysPravoslavie
{
    public class PravDay
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">Дата.</param>
        public PravDay(DateTime date)
        {
            Date = date;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; private set; }
    }
}
