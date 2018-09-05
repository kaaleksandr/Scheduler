using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Scheduler
{
    public partial class Form1 : Form
    {
        private const int YearFoward = 1;

        private const string RegPath = "Software\\Pravdel";
        private const string RecentOutDir = "RecentOutDir";

        /// <summary>
        /// 
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        /// <summary>
        /// Инициализация.
        /// </summary>
        private void Init()
        {
            InitYearList();
            InitMonthList();

            //
            // Установить значения по умолчанию: год, след. месяц.
            //

            var now = DateTime.Now;
            if (now.Month != 12)
            {
                yearList.SelectedIndex = 0;
                monthList.SetItemChecked(now.Month, true);
            }
            else
            {
                yearList.SelectedIndex = 1;
                monthList.SetItemChecked(0, true);
            }
        }

        /// <summary>
        /// Инициализирует список годов.
        /// </summary>
        private void InitYearList()
        {
            yearList.SuspendLayout();

            try
            {
                var nowYear = DateTime.Now.Year;

                for(int i = nowYear; i <= nowYear + YearFoward; i += 1)
                {
                    var item = new MyListItem<int> { DisplayString = i.ToString(), Tag = i };
                    yearList.Items.Add(item);
                }
            }
            finally
            {
                yearList.ResumeLayout();
            }
        }

        /// <summary>
        /// Инициализирует список месяцев.
        /// </summary>
        private void InitMonthList()
        {
            monthList.SuspendLayout();

            try
            {
                var item = new MyListItem<int> { DisplayString = "Январь", Tag = 1 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Февраль", Tag = 2 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Март", Tag = 3 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Апрель", Tag = 4 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Май", Tag = 5 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Июнь", Tag = 6 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Июль", Tag = 7 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Август", Tag = 8 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Сентябрь", Tag = 9 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Октябрь", Tag = 10 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Ноябрь", Tag = 11 };
                monthList.Items.Add(item);

                item = new MyListItem<int> { DisplayString = "Декабрь", Tag = 12 };
                monthList.Items.Add(item);
            }
            finally
            {
                monthList.ResumeLayout();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private void DoCheck()
        {
            if (yearList.SelectedItem == null)
            {
                throw new Exception("Не выбран год.");
            }

            if (monthList.CheckedItems.Count == 0)
            {
                throw new Exception("Не выбрано ни одного месяца.");
            }
        }

        /// <summary>
        /// Возвращает значение ранее установленной выходной директории.
        /// </summary>
        /// <returns></returns>
        private string GetRecentOutDir()
        {
            var reg = Registry.CurrentUser.OpenSubKey(RegPath, false);

            try
            {
                if (reg != null)
                {
                    var val = reg.GetValue(RecentOutDir) as string;
                    return val ?? "";
                }
            }
            finally
            {
                if (reg != null)
                {
                    reg.Dispose();
                }
            }

            return "";
        }

        /// <summary>
        /// Сохраняет значение ранее установленной выходной директории.
        /// </summary>
        /// <param name="outDir"></param>
        private void SetRecentOutDir(string dir)
        {
            var reg = Registry.CurrentUser.OpenSubKey(RegPath, true);

            try
            {
                if (reg == null)
                {
                    reg = Registry.CurrentUser.CreateSubKey(RegPath);
                }

                if (reg != null)
                {
                    reg.SetValue(RecentOutDir, outDir.Text);
                }
            }
            finally
            {
                if (reg != null)
                {
                    reg.Dispose();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog d = new FolderBrowserDialog();
            if (d.ShowDialog() == DialogResult.OK)
            {
                outDir.Text = d.SelectedPath;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void createButton_Click(object sender, EventArgs e)
        {
            try
            {
                DoCheck();

                int year = (yearList.SelectedItem as MyListItem<int>).Tag;

                List<int> mons = new List<int>(monthList.CheckedItems.Count);
                foreach(var item in monthList.CheckedItems)
                {
                    mons.Add((item as MyListItem<int>).Tag);
                }

                UpdateLog("Подключение к http://days.pravoslavie.ru");

                RaspGenerator.Generate(outDir.Text, year, mons, UpdateProgress, UpdateLog);

                MessageBox.Show("Файлы расписаний созданы", "Создать файлы расписания", MessageBoxButtons.OK, MessageBoxIcon.Information);

                UpdateProgress(0);
                UpdateLog("");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Создать расписание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Обновляет прогресс.
        /// </summary>
        /// <param name="current"></param>
        private void UpdateProgress(int current)
        {
            progressBar.Value = current;
            Application.DoEvents();
        }

        /// <summary>
        /// Обновляет сообщение лога.
        /// </summary>
        /// <param name="message"></param>
        private void UpdateLog(string message)
        {
            logLabel.Text = message;
            Application.DoEvents();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            outDir.Text = GetRecentOutDir();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            SetRecentOutDir(outDir.Text);
        }
    }
}
