//
// Copyright (c) SoftTonna, 2018
//

using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Drawing;

using HtmlAgilityPack;
using Xceed.Words.NET;
using GemBox.Spreadsheet;

namespace DaysPravoslavie
{
    public static class Helper
    {
        private const int MaxMonthDaysCount = 32;
        public const int ColumnCount = 5;
        public const int HeaderRowsCount = 3;

        /// <summary>
        /// Максимальное кол-во записей мясецеслова.
        /// </summary>
        public const int MaxCountOfMonth = 1;

        public static CultureInfo RussianCulture = CultureInfo.CreateSpecificCulture("ru-Ru");
        public static DateTimeFormatInfo DateFormat = RussianCulture.DateTimeFormat;

        /// <summary>
        /// Возвращает список дней месяца.
        /// </summary>
        public static List<PravDay> GetDays(int year, int month)
        {
            List<PravDay> days = new List<PravDay>(MaxMonthDaysCount);

            DateTime date = new DateTime(year, month, 1);
            while(date.Month == month)
            {
                days.Add(new PravDay(date));
                date = date.AddDays(1.0);
            }

            return days;
        }

        /// <summary>
        /// Возвращает HtmlDocument.
        /// </summary>
        public static HtmlDocument GetHtmlDocument(string httpPath)
        {
            HtmlWeb web = new HtmlWeb();
            return web.Load(httpPath);
        }

        /// <summary>
        /// Возвращает неделю и Глас.
        /// </summary>
        public static Tuple<string, string> GetWeekAndGlas(HtmlDocument htmlDocument)
        {
            HtmlNodeCollection spans = 
                htmlDocument.DocumentNode.SelectNodes("//html/body/table/tr[4]/td[2]/table[1]/tr/td[1]/span");

            if (spans != null)
            {
                if (spans.Count > 1)
                {
                    return new Tuple<string, string>(spans[0].InnerText, spans[1].InnerText);
                }

                if (spans.Count > 0)
                {
                    return new Tuple<string, string>(spans[0].InnerText, "");
                }
            }

            return null;
        }

        /// <summary>
        /// Возвращает праздник.
        /// </summary>
        public static List<string> GetCelebration(HtmlDocument htmlDocument)
        {
            if (htmlDocument == null)
            {
                throw new ArgumentNullException("htmlDocument");
            }

            List<string> celebrs = new List<string>();

            var tagP =
                htmlDocument.DocumentNode.SelectSingleNode("/html/body/table/tr[4]/td[2]/div[1]/p[@class='DP_TEXT DPN_-1']");
            if (tagP != null)
            {
                //
                // Получить список праздников.
                //

                HtmlNodeCollection spans = tagP.SelectNodes("./span");

                foreach (var sp in spans)
                {
                    celebrs.Add(sp.InnerText);
                }
            }

            return celebrs;
        }

        /// <summary>
        /// Возвращает месяцеслов.
        /// </summary>
        public static List<string> GetMonth(HtmlDocument htmlDocument)
        {
            List<string> month = new List<string>();

            HtmlNode node = null;
            int i = 1;
            const string prefix = "/html/body/table/tr[4]/td[2]/div[1]/p[@class='DP_TEXT DPN_";
            const string suffix = "']";

            for(;;)
            {
                node = htmlDocument.DocumentNode.SelectSingleNode(prefix + i.ToString() + suffix);
                i += 1;

                if (node == null)
                {
                    break;
                }

                month.Add(node.InnerText);
            }

            return month;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public static string GetCellName(int row, int column)
        {
            char columnChar= (char)('A' + column);
            return columnChar + (row + 1).ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="days"></param>
        /// <param name="progressCallback"></param>
        /// <returns></returns>
        public static void CreateDocument(string fname, bool createXlsxFile, List<Day> days, Action<int> progressCallback)
        {
            if (string.IsNullOrEmpty(fname))
            {
                throw new ArgumentNullException("fname");
            }

            var firstDay = days.First();
            string monthAndYearString = GetDateStringForDate(firstDay.DateTime);

            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
            ExcelFile excel = new ExcelFile();
            ExcelWorksheet ws = excel.Worksheets.Add("Расписание");

            using (DocX document = DocX.Create(fname))
            {
                var t = document.AddTable(HeaderRowsCount + days.Count, ColumnCount);
                t.Alignment = Alignment.center;
                t.AutoFit = AutoFit.Window;

                t.Rows[0].MergeCells(0, ColumnCount - 1);
                t.Rows[1].MergeCells(0, ColumnCount - 1);

                // excel
                var range = ws.Cells.GetSubrange("A1", GetCellName(0, ColumnCount - 1));
                range.Merged = true;

                range = ws.Cells.GetSubrange("A2", GetCellName(1, ColumnCount - 1));
                range.Merged = true;

                range = ws.Cells.GetSubrangeAbsolute(0, 0, HeaderRowsCount + days.Count - 1, ColumnCount - 1);
                range.Style.Borders.SetBorders(MultipleBorders.All, Color.Black, LineStyle.Thin);

                //

                //
                // Шапка таблицы.
                //

                var p = t.Rows[0].Cells[0].Paragraphs[0].Append("Расписание Богослужений");
                p.Alignment = Alignment.center;
                p.Bold();
                p.FontSize(20.0);
                p.Color(Color.White);

                t.Rows[0].Cells[0].FillColor = ColorTranslator.FromHtml("#2ec0fd");
                t.Rows[0].Cells[0].VerticalAlignment = VerticalAlignment.Center;

                // excel
                // Дата.
                ws.Columns["A"].Width = 10 * 256;
                // День недели.
                ws.Columns["B"].Width = 14 * 256;
                // Месяцеслов.
                ws.Columns["C"].Width = 24 * 256;
                // Переходящие праздники.
                ws.Columns["D"].Width = 20 * 256;
                // Богослужение.
                ws.Columns["E"].Width = 25 * 256;

                var tmpCell = ws.Cells["A1"];
                tmpCell.Style.Font.Size = 20 * 20;
                tmpCell.Style.Font.Color = Color.White;
                tmpCell.Style.Font.Weight = ExcelFont.MaxWeight;
                tmpCell.Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(0x2e, 0xc0, 0xfd));
                tmpCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                tmpCell.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
                tmpCell.Value = "Расписание Богослужений";
                //

                p = t.Rows[1].Cells[0].Paragraphs[0].Append(monthAndYearString);
                p.Alignment = Alignment.center;
                p.Bold();

                t.Rows[1].Cells[0].FillColor = ColorTranslator.FromHtml("#cccccc");
                t.Rows[1].Cells[0].VerticalAlignment = VerticalAlignment.Center;

                // excel
                tmpCell = ws.Cells["A2"];
                tmpCell.Style.FillPattern.SetSolid(SpreadsheetColor.FromArgb(0xcc, 0xcc, 0xcc));
                tmpCell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                tmpCell.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
                tmpCell.Value = monthAndYearString;
                //

                t.Rows[2].Cells[0].Paragraphs[0].Append("Дата").Alignment = Alignment.center;
                t.Rows[2].Cells[1].Paragraphs[0].Append("День недели").Alignment = Alignment.center;
                t.Rows[2].Cells[2].Paragraphs[0].Append("Месяцеслов").Alignment = Alignment.center;
                t.Rows[2].Cells[3].Paragraphs[0].Append("Переходящие праздники").Alignment = Alignment.center;
                t.Rows[2].Cells[4].Paragraphs[0].Append("Богослужение").Alignment = Alignment.center;

                // excel
                var a3 = ws.Cells["A3"];
                var b3 = ws.Cells["B3"];
                var c3 = ws.Cells["C3"];
                var d3 = ws.Cells["D3"];
                var e3 = ws.Cells["E3"];

                a3.Style.HorizontalAlignment =
                    b3.Style.HorizontalAlignment =
                    c3.Style.HorizontalAlignment =
                    d3.Style.HorizontalAlignment =
                    e3.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;

                a3.Style.VerticalAlignment =
                   b3.Style.VerticalAlignment =
                   c3.Style.VerticalAlignment =
                   d3.Style.VerticalAlignment =
                   e3.Style.VerticalAlignment = VerticalAlignmentStyle.Center;

                a3.Style.WrapText =
                   b3.Style.WrapText =
                   c3.Style.WrapText =
                   d3.Style.WrapText =
                   e3.Style.WrapText = true;

                a3.Value = "Дата";
                b3.Value = "День недели";
                c3.Value = "Месяцеслов";
                d3.Value = "Переходящие праздники";
                e3.Value = "Богослужение";
                //

                for (int i = 0; i < days.Count; i += 1)
                {
                    /** Праздник. */
                    bool isCeleb;
                    var line = days[i].GetRowContent(out isCeleb);

                    Color colorDay;
                    if (isCeleb || days[i].DateTime.DayOfWeek == DayOfWeek.Sunday)
                    {
                        colorDay = Color.Red;
                    }
                    else
                    {
                        colorDay = Color.Black;
                    }

                    //
                    // Добавить строки.
                    //

                    for (int j = 0; j < line.Length; j += 1)
                    {
                        var para = t.Rows[HeaderRowsCount + i].Cells[j].Paragraphs[0].Append(line[j]);
                        para.Alignment = Alignment.center;
                        para.Color(colorDay);
                        para.Culture(RussianCulture);
                        para.FontSize(10.0);

                        //excel
                        var cell = ws.Cells[GetCellName(HeaderRowsCount + i, j)];
                        cell.Style.HorizontalAlignment = HorizontalAlignmentStyle.Center;
                        cell.Style.VerticalAlignment = VerticalAlignmentStyle.Center;
                        cell.Style.Font.Color = colorDay;
                        cell.Style.Font.Size = 20 * 10;
                        cell.Style.WrapText = true;
                        cell.Value = line[j];
                        //

                        if (j == 2)
                        {
                            para.Bold();

                            //excel
                            cell.Style.Font.Weight = ExcelFont.MaxWeight;
                            //
                        }
                    }

                    progressCallback(GetProgress(i, days.Count));
                }

                //
                // Вставить таблицу и сохранить документ.
                //

                document.InsertTable(t);
                document.Save();

                excel.Save(Path.ChangeExtension(fname, ".xlsx"));
            }
        }

        /// <summary>
        /// Возвращает строку вида "декабрь 2018 года".
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private static string GetDateStringForDate(DateTime date)
        {
            return date.ToString("Y") + " года.";
        }

        /// <summary>
        /// Возвращает текущий прогресс.
        /// </summary>
        /// <param name="onePerc"></param>
        /// <param name="totalPerc"></param>
        /// <returns></returns>
        public static int GetProgress(int current, int total)
        {
            return (int)Math.Round((float)current / total * 100f);
        }
    }
}
