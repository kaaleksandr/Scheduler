using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scheduler
{
    public partial class Form1 : Form
    {
        private const int YearFoward = 1;

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
        }

        /// <summary>
        /// 
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

                yearList.SelectedIndex = 0;
            }
            finally
            {
                yearList.ResumeLayout();
            }
        }

        /// <summary>
        /// 
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

            if (monthList.SelectedItems.Count == 0)
            {
                throw new Exception("Не выбрано ни одного месяца.");
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
        private void yearList_SelectedValueChanged(object sender, EventArgs e)
        {
            
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

                List<int> mons = new List<int>(monthList.SelectedItems.Count);
                foreach(var item in monthList.SelectedItems)
                {
                    mons.Add((item as MyListItem<int>).Tag);
                }

                RaspGenerator.Generate(outDir.Text, year, mons);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Создать расписание", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
