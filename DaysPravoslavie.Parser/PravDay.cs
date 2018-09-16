//
// Copyright (c) SoftTonna, 2018
//

using System;

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
        public const int OldStyleCorrection = -13;

        private DateTime _date;
        private DateTime _oldDate;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="date">Дата.</param>
        public PravDay(DateTime date)
        {
            _date = date;
            _oldDate = date.AddDays(OldStyleCorrection);

            HtmlPath =
                HttpPath +
                _oldDate.Year.ToString() +
                _oldDate.Month.ToString(IntFormat) +
                _oldDate.Day.ToString(IntFormat) + 
                DotHtml;
        }

        /// <summary>
        /// Дата по новому стилю.
        /// </summary>
        public DateTime Date
        {
            get { return _date; }
        }

        /// <summary>
        /// Дата по старому стилю.
        /// </summary>
        public DateTime OldStyleDate
        {
            get
            {
                return Date.AddDays(OldStyleCorrection);
            }
        }

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
