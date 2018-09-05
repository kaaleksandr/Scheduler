using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;
using SautinSoft.Document;
using SautinSoft.Document.Tables;

namespace DaysPravoslavie
{
    public static class Helper
    {
        private const int MaxMonthDaysCount = 32;
        public const int ColumnCount = 5;

        public const int MaxCountOfMonth = 2;
        public static readonly Color TableBorderColor = Color.Black;

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
        /// Создает документ.
        /// </summary>
        /// <param name="daysFromMonth">Коллекция дней месяца.</param>
        /// <returns></returns>
        public static DocumentCore CreateDocument(List<Day> daysFromMonth, Action<int> progressCallback)
        {
            if (daysFromMonth == null)
            {
                throw new ArgumentNullException("daysFromMonth");
            }

            var firstDay = daysFromMonth.First();

            string monthAndYearString = GetDateStringForDate(firstDay.DateTime);

            DocumentCore dc = new DocumentCore();

            Section s = new Section(dc);
            dc.Sections.Add(s);

            Table table = new Table(dc);
            double width = LengthUnitConverter.Convert(18, LengthUnit.Centimeter, LengthUnit.Point);
            table.TableFormat.PreferredWidth = new TableWidth(width, TableWidthUnit.Point);
            table.TableFormat.Alignment = HorizontalAlignment.Center;
            table.TableFormat.Borders.Add(MultipleBorderTypes.All, BorderStyle.Single, TableBorderColor, 1.0);

            //
            // Заголовок "Расписание".
            //

            TableRow headerRow = new TableRow(dc);

            TableCell headerCell = new TableCell(dc);
            headerCell.CellFormat = new TableCellFormat()
            {
                BackgroundColor = new Color("#2ec0fd"),
                VerticalAlignment = VerticalAlignment.Center,
            };

            headerCell.ColumnSpan = ColumnCount;

            headerRow.Cells.Add(headerCell);

            Paragraph paraHeader = new Paragraph(dc);
            paraHeader.Content.Start.Insert("Расписание", new CharacterFormat
            {
                Bold = true,
                FontColor = Color.White,
                Size = 20.0,
                Language = RussianCulture,
            });

            paraHeader.ParagraphFormat = new ParagraphFormat { Alignment = HorizontalAlignment.Center };

            headerCell.Blocks.Add(paraHeader);

            table.Rows.Add(headerRow);

            //
            // Заголовок "Месяц и год".
            //

            TableRow myRow = new TableRow(dc);
            TableCell myCell = new TableCell(dc);
            myCell.CellFormat = new TableCellFormat()
            {
                BackgroundColor = new Color("#808080"),
                VerticalAlignment = VerticalAlignment.Center,
            };

            myCell.ColumnSpan = ColumnCount;

            myRow.Cells.Add(myCell);

            Paragraph myHeader = new Paragraph(dc);
            myHeader.Content.Start.Insert(monthAndYearString,
                new CharacterFormat
                {
                    Bold = true,
                    FontColor = Color.Black,
                    Size = 14.0,
                    Language = RussianCulture,
            });

            myHeader.ParagraphFormat = new ParagraphFormat { Alignment = HorizontalAlignment.Center };

            myCell.Blocks.Add(myHeader);

            table.Rows.Add(myRow);

            //
            // Шапка таблицы.
            // Дата, День недели, Месяцеслов, Переходящие праздники, Богослужение.
            //

            TableRow hat = new TableRow(dc);
            TableCell col1 = new TableCell(dc);
            TableCell col2 = new TableCell(dc);
            TableCell col3 = new TableCell(dc);
            TableCell col4 = new TableCell(dc);
            TableCell col5 = new TableCell(dc);

            var hatCharFormat = new CharacterFormat
            {
                Bold = true,
                FontColor = Color.Blue,
                Size = 14.0,
                Language = RussianCulture,
            };

            var hatParaFormat = new ParagraphFormat { Alignment = HorizontalAlignment.Center };
            var colFormat = new TableCellFormat { VerticalAlignment = VerticalAlignment.Center };

            col1.CellFormat = colFormat;
            col2.CellFormat = colFormat.Clone();
            col3.CellFormat = colFormat.Clone();
            col4.CellFormat = colFormat.Clone();
            col5.CellFormat = colFormat.Clone();

            hat.Cells.Add(col1);
            hat.Cells.Add(col2);
            hat.Cells.Add(col3);
            hat.Cells.Add(col4);
            hat.Cells.Add(col5);

            //
            // Дата.
            //

            Paragraph datePara = new Paragraph(dc);
            datePara.Content.Start.Insert("Дата", hatCharFormat);
            datePara.ParagraphFormat = hatParaFormat;

            //
            // День недели.
            //

            Paragraph weekOfDayPara = new Paragraph(dc);
            weekOfDayPara.Content.Start.Insert("День недели", hatCharFormat.Clone());
            weekOfDayPara.ParagraphFormat = hatParaFormat.Clone();

            //
            // Месяцеслов.
            //

            Paragraph monthPara = new Paragraph(dc);
            monthPara.Content.Start.Insert("Месяцеслов", hatCharFormat.Clone());
            monthPara.ParagraphFormat = hatParaFormat.Clone();

            //
            // Переходящие праздники.
            //

            Paragraph celebPara = new Paragraph(dc);
            celebPara.Content.Start.Insert("Переходящие праздники", hatCharFormat.Clone());
            celebPara.ParagraphFormat = hatParaFormat.Clone();

            //
            // Богослужение.
            //

            Paragraph servPara = new Paragraph(dc);
            servPara.Content.Start.Insert("Богослужение", hatCharFormat.Clone());
            servPara.ParagraphFormat = hatParaFormat.Clone();

            col1.Blocks.Add(datePara);
            col2.Blocks.Add(weekOfDayPara);
            col3.Blocks.Add(monthPara);
            col4.Blocks.Add(celebPara);
            col5.Blocks.Add(servPara);

            table.Rows.Add(hat);

            //
            // Заполнить таблицу.
            //

            for (int i = 0; i < daysFromMonth.Count; i += 1)
            {
                var line = daysFromMonth[i].GetRowContent();
                Color colorDay;
                if (daysFromMonth[i].DateTime.DayOfWeek != DayOfWeek.Sunday)
                {
                    colorDay = Color.Black;
                }
                else
                {
                    colorDay = Color.Red;
                }

                TableRow row = new TableRow(dc);
                for (int j = 0; j < line.Length; j += 1)
                {
                    TableCell cell = new TableCell(dc);
                    row.Cells.Add(cell);

                    Paragraph p = new Paragraph(dc);
                    p.ParagraphFormat.Alignment = HorizontalAlignment.Center;

                    p.Content.Start.Insert(
                        line[j],
                        new CharacterFormat
                        {
                            FontColor = colorDay,
                            Language = RussianCulture,
                        });

                    cell.Blocks.Add(p);
                }

                table.Rows.Add(row);

                progressCallback(GetProgress(i, daysFromMonth.Count));
            }

            s.Blocks.Add(table);

            return dc;
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
        /// 
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
