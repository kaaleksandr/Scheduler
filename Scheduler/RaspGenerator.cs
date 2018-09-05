using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DaysPravoslavie;

namespace Scheduler
{
    static class RaspGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outDir"></param>
        /// <param name="year"></param>
        /// <param name="months"></param>
        public static void Generate(string outDir, int year, List<int> months, Action<int> progressCallback, Action<string> logCallback)
        {
            if (progressCallback == null)
            {
                throw new ArgumentNullException("progressCallback");
            }

            string prefix = year.ToString();

            for(int i = 0; i < months.Count; i += 1)
            {
                string fname = prefix + "-" + Helper.DateFormat.MonthNames[months[i] - 1] + ".docx";
                string path = Path.Combine(outDir, fname);
                var pdays = Helper.GetDays(year, months[i]);
                List<Day> days = new List<Day>(pdays.Count);

                for (int k = 0; k < pdays.Count; k += 1)
                {
                    days.Add(new Day(pdays[k].Date, Helper.GetHtmlDocument(pdays[k].HtmlPath)));
                }

                logCallback("Создание документа: " + fname);

                var doc = Helper.CreateDocument(days, progressCallback);
                

                //
                // Удалить ранее созданный файл.
                //

                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                doc.Save(path);

                logCallback("Документ создан: " + fname);
            }
        }
    }
}
