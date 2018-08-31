using System;
using System.Collections.Generic;
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

            return new Tuple<string, string>(spans[0].InnerText, spans[1].InnerText);
        }

        /// <summary>
        /// Возвращает праздник.
        /// </summary>
        public static List<string> GetCelebration(HtmlDocument htmlDocument)
        {
            List<string> celebrs = null;

            var tagP =
                htmlDocument.DocumentNode.SelectSingleNode("/html/body/table/tr[4]/td[2]/div[1]/p[@class='DP_TEXT DPN_-1']");
            if (tagP != null)
            {
                //
                // Получить список праздников.
                //

                HtmlNodeCollection spans = tagP.SelectNodes("./span");
                celebrs = new List<string>(spans.Count);

                foreach (var sp in spans)
                {
                    celebrs.Add(sp.InnerText);
                }
            }

            return celebrs;
        }

        /// <summary>
        /// Создает документ.
        /// </summary>
        /// <param name="daysFromMonth">Коллекция дней месяца.</param>
        /// <returns></returns>
        public static DocumentCore CreateDocument(List<Day> daysFromMonth)
        {
            if (daysFromMonth == null)
            {
                throw new ArgumentNullException("daysFromMonth");
            }

            string documentPath = @"SimpleTable.pdf";

            // Let's create a new document.
            DocumentCore dc = new DocumentCore();

            // Add a new section.
            Section s = new Section(dc);
            dc.Sections.Add(s);

            // Let's create a plain table: 2x3, 100 mm of width.
            Table table = new Table(dc);
            double width = LengthUnitConverter.Convert(100, LengthUnit.Millimeter, LengthUnit.Point);
            table.TableFormat.PreferredWidth = new TableWidth(width, TableWidthUnit.Point);
            table.TableFormat.Alignment = HorizontalAlignment.Center;

            int counter = 0;

            // Add rows.
            int rows = 2;
            int columns = 3;
            for (int r = 0; r < rows; r++)
            {
                TableRow row = new TableRow(dc);

                // Add columns.
                for (int c = 0; c < columns; c++)
                {
                    TableCell cell = new TableCell(dc);

                    // Set cell formatting and width.
                    cell.CellFormat.Borders.SetBorders(MultipleBorderTypes.Outside, BorderStyle.Dotted, Color.Black, 1.0);

                    // Set the same width for each column.
                    cell.CellFormat.PreferredWidth = new TableWidth(width / columns, TableWidthUnit.Point);

                    if (counter % 2 == 1)
                        cell.CellFormat.BackgroundColor = new Color("#358CCB");

                    row.Cells.Add(cell);

                    // Let's add a paragraph with text into the each column.
                    Paragraph p = new Paragraph(dc);
                    p.ParagraphFormat.Alignment = HorizontalAlignment.Center;
                    p.ParagraphFormat.SpaceBefore = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);
                    p.ParagraphFormat.SpaceAfter = LengthUnitConverter.Convert(3, LengthUnit.Millimeter, LengthUnit.Point);

                    p.Content.Start.Insert(String.Format("{0}", (char)(counter + 'A')), new CharacterFormat() { FontName = "Arial", FontColor = new Color("#3399FF"), Size = 12.0 });
                    cell.Blocks.Add(p);
                    counter++;
                }
                table.Rows.Add(row);
            }

            // Add the table into the section.
            s.Blocks.Add(table);

            // Save our document into PDF format.
            dc.Save(documentPath, new PdfSaveOptions() { Compliance = PdfCompliance.PDF_A });

            // Open the result for demonstation purposes.
            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(documentPath) { UseShellExecute = true });
            /////////////////////////////////////////////////////////////////////

            var doc = new DocumentCore();



            return doc;
        }
    }
}
