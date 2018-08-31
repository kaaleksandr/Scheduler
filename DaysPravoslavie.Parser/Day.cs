using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using HtmlAgilityPack;

namespace DaysPravoslavie
{
    /// <summary>
    /// 
    /// </summary>
    public class Day
    {
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="dateTime">Дата.</param>
        /// <param name="htmlDocument">HTML-документ.</param>
        public Day(DateTime dateTime, HtmlDocument htmlDocument)
        {
            Document = htmlDocument ?? throw new ArgumentNullException("htmlDocument");
            DateTime = dateTime;

            Init();
        }

        /// <summary>
        /// HTML-документ.
        /// </summary>
        public HtmlDocument Document { get; private set; }

        /// <summary>
        /// Дата.
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Номер Гласа.
        /// </summary>
        /// <remarks>Диапазон допустимых значений: 1-8.</remarks>
        public int Glas { get; private set; }

        /// <summary>
        /// Неделя.
        /// </summary>
        public string Week { get; private set; }

        /// <summary>
        /// Месяцеслов.
        /// </summary>
        public List<string> Months { get; private set; }

        /// <summary>
        /// Инициализация.
        /// </summary>
        private void Init()
        {
            var tuple = Helper.GetWeekAndGlas(Document);

            string[] parts = tuple.Item2.Split(new[] { ' ' }, 2);
            string glas = parts[1].Substring(0, 1);

            Glas = int.Parse(glas);
            Week = tuple.Item1;

            Months = Helper.GetCelebration(Document);
        }

        public override string ToString()
        {
            return DateTime.ToString() + ", Неделя: " + Week + ", Глас: " + Glas;
        }
    }
}
