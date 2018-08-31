using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DaysPravoslavie
{
    /// <summary>
    /// 
    /// </summary>
    public class PravDay
    {
        private const string IntFormat = "D2";
        public const string DotHtml = ".html";
        public const string HttpPath = "http://days.pravoslavie.ru/Days/";

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">Дата.</param>
        public PravDay(DateTime date)
        {
            Date = date;
            HtmlPath =
                HttpPath + 
                Date.Year.ToString() + 
                Date.Month.ToString(IntFormat) + 
                Date.Day.ToString(IntFormat) + 
                DotHtml;
        }

        /// <summary>
        /// 
        /// </summary>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Полный путь.
        /// </summary>
        public string HtmlPath
        {
            get;
            private set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return HtmlPath;
        }
    }
}
