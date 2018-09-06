//
// Copyright (c) SoftTonna, 2018
//

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
        /// Великие праздники.
        /// </summary>
        public List<string> GreatCelebrations { get; private set; }

        /// <summary>
        /// Месяцеслов.
        /// </summary>
        public List<string> Month { get; private set; }

        /// <summary>
        /// Инициализация.
        /// </summary>
        private void Init()
        {
            var tuple = Helper.GetWeekAndGlas(Document);
            if (tuple != null)
            {
                string[] parts = tuple.Item2.Split(new[] { ' ' }, 2);
                string glas = "";
                if (parts.Length == 2)
                {
                    glas = parts[1].Substring(0, 1);
                }

                int gl = 0;
                if (int.TryParse(glas, out gl))
                {
                    Glas = gl;
                    Week = tuple.Item1;
                }
                else
                {
                    Glas = -1;
                    Week = tuple.Item1;
                    if (tuple.Item2 != "")
                    {
                        Week += " " + tuple.Item2;
                    }
                }
            }
            else
            {
                Glas = -1;
                Week = "";
            }

            GreatCelebrations = Helper.GetCelebration(Document);

            Month = Helper.GetMonth(Document);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetRowContent(out bool isCeleb)
        {
            string[] line = new string[Helper.ColumnCount];

            string tmp = DateTime.ToString("m");
            int spacePos = tmp.IndexOf(' ');
            tmp = tmp.Substring(0, spacePos + 4);

            // Дата.
            line[0] = DateTime.Day + " " + Helper.DateFormat.AbbreviatedMonthNames[DateTime.Month - 1];

            // День недели.
            line[1] = Helper.DateFormat.DayNames[(int)DateTime.DayOfWeek];

            // Месяцеслов.
            if (isCeleb = GreatCelebrations.Count > 0)
            {
                line[2] = string.Join(" ", GreatCelebrations);
            }
            else
            {
                line[2] = string.Join(" ", Month.Take(Helper.MaxCountOfMonth));
            }

            // Переходящие праздники.
            if (Glas != -1)
            {
                line[3] = Week + " Глас " + Glas + "-й.";
            }
            else
            {
                line[3] = Week;
            }

            // Богослужение.
            line[4] = "";

            return line;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return DateTime.ToString() + ", Неделя: " + Week + ", Глас: " + Glas;
        }
    }
}
